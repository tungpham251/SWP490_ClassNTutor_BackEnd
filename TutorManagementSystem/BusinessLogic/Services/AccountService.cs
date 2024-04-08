using AutoMapper;
using BusinessLogic.Helpers;
using BusinessLogic.Services.Interfaces;
using DataAccess.Dtos;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly ClassNTutorContext _context;
        private readonly IConfiguration _config;
        private readonly IEmailService _emailService;
        private readonly IS3StorageService _s3storageService;
        private readonly IMapper _mapper;

        public AccountService(ClassNTutorContext context, IConfiguration config,
            IEmailService emailService, IS3StorageService s3storageService, IMapper mapper)
        {
            _context = context;
            _config = config;
            _emailService = emailService;
            _s3storageService = s3storageService;
            _mapper = mapper;
        }

        public async Task<Account> AddAccount(RegisterDto entity)
        {
            try
            {
                var newPerson = await AddPerson(entity).ConfigureAwait(false);

                if (newPerson == null) return null;

                var newAccount = new Account
                {
                    PersonId = newPerson.PersonId,
                    Email = entity.Email!,
                    Password = PasswordHashUtility.HashPassword(entity.Password!),
                    Status = "Active",
                    RoleId = entity.RoleId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                await _context.Accounts.AddAsync(newAccount).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);

                return newAccount;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Person> AddPerson(RegisterDto entity)
        {
            try
            {
                if (!FileHelper.IsImage(entity.Avatar.FileName))
                {
                    return null;
                }

                var avatar = await _s3storageService.UploadFileToS3(entity.Avatar!).ConfigureAwait(false);

                var lastPerson = await _context.People.OrderBy(x => x.PersonId).LastOrDefaultAsync().ConfigureAwait(false);

                var newPerson = new Person
                {
                    PersonId = lastPerson!.PersonId + 1,
                    UserAvatar = avatar,
                    Address = entity.Address!,
                    Dob = entity.Dob,
                    Gender = entity.Gender!,
                    FullName = entity.FullName!,
                    Phone = entity.Phone!,
                };

                await _context.People.AddAsync(newPerson).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);

                return newPerson;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> ChangePassword(ChangePasswordDto entity)
        {
            try
            {
                var account = await _context.Accounts.Include(x => x.Role).Where(x => x.Email.Trim().ToLower()
               .Equals(entity.Email!.Trim().ToLower())).FirstOrDefaultAsync();

                if (account == null) return false;

                var check = PasswordHashUtility.VerifyPassword(entity.OldPassword!, account.Password);

                if (!check || entity.NewPassword!.Equals(entity.OldPassword)
                    || !entity.RePassword!.Equals(entity.NewPassword)) return false;

                account.Password = PasswordHashUtility.HashPassword(entity.NewPassword);

                _context.Accounts.Update(account);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<PersonDto>> GetAllParents()
        {
            var list = await _context.People.Include(x => x.Account)
                .Where(x => x.Account!.RoleId == 2
                && x.Account.Status.Trim().ToLower()
                .Equals("Active".Trim().ToLower()))
                .ToListAsync().ConfigureAwait(false);
            return _mapper.Map<IEnumerable<PersonDto>>(list);
        }

        public async Task<IEnumerable<PersonDto>> GetAllStudents()
        {
            var list = await _context.People.Include(x => x.StudentStudentNavigation)
               .Where(x => x.StudentStudentNavigation!.Status!.Trim().ToLower()
               .Equals("Created".Trim().ToLower()))
               .ToListAsync().ConfigureAwait(false);
            return _mapper.Map<IEnumerable<PersonDto>>(list);
        }

        public async Task<IEnumerable<PersonDto>> GetAllTutors()
        {
            var list = await _context.People.Include(x => x.Account)
                .Where(x => x.Account!.RoleId == 1
                && x.Account.Status.Trim().ToLower()
                .Equals("Active".Trim().ToLower()))
                .ToListAsync().ConfigureAwait(false);
            return _mapper.Map<IEnumerable<PersonDto>>(list);
        }

        public async Task<string> GetAccountId(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidIssuer = _config["Jwt:Issuer"],
                    IssuerSigningKey = securityKey
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var id = jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

                return id;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<LoginResponseDto> Login(LoginDto entity)
        {
            try
            {
                var account = await _context.Accounts.Include(x => x.Role).Where(x => x.Email.Trim().ToLower()
                .Equals(entity.Email!.Trim().ToLower())).FirstOrDefaultAsync();

                if (account == null ||
                    !PasswordHashUtility.VerifyPassword(entity.Password!, account.Password)) return null;

                if (!account.Status.ToLower().Equals("Active".ToLower())) return null;

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, account.PersonId.ToString()),
                    new Claim(ClaimTypes.Email, account.Email),
                    new Claim(ClaimTypes.Role, account.Role.RoleName.ToUpper()),
                };

                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMonths(2),
                    signingCredentials: credentials);

                var tokenHandle = new JwtSecurityTokenHandler().WriteToken(token);

                return new LoginResponseDto(tokenHandle, account.PersonId, account.Role.RoleName);

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> RegisterParent(RegisterDto entity)
        {
            try
            {
                var newAccount = await AddAccount(entity).ConfigureAwait(false);

                if (newAccount == null) return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> RegisterStaff(RegisterStaffDto entity)
        {
            try
            {
                var newAccount = await AddAccount(entity).ConfigureAwait(false);

                if (newAccount == null) return false;

                var newStaff = new Staff
                {
                    PersonId = newAccount.PersonId,
                    StaffType = entity.StaffType!,
                };

                await _context.Staffs.AddAsync(newStaff).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> RegisterTutor(RegisterTutorDto entity)
        {
            try
            {
                var newAccount = await AddAccount(entity).ConfigureAwait(false);

                if (newAccount == null) return false;

                if (!FileHelper.IsPDF(entity.Cv.FileName)
                    || !FileHelper.IsImage(entity.BackCmnd.FileName)
                     || !FileHelper.IsImage(entity.FrontCmnd.FileName)
                    )
                {
                    return false;
                }

                var front = await _s3storageService.UploadFileToS3(entity.FrontCmnd!).ConfigureAwait(false);
                var back = await _s3storageService.UploadFileToS3(entity.BackCmnd!).ConfigureAwait(false);
                var cv = await _s3storageService.UploadFileToS3(entity.Cv!).ConfigureAwait(false);

                var newTutor = new Tutor
                {
                    PersonId = newAccount.PersonId,
                    Cmnd = entity.Cmnd,
                    BackCmnd = back,
                    FrontCmnd = front,
                    Cv = cv,
                    About = entity.About,
                    EducationLevel = entity.EducationLevel!,
                    GraduationYear = entity.GraduationYear!,
                    School = entity.School!,
                };

                await _context.Tutors.AddAsync(newTutor).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ResetPassword(string email)
        {
            try
            {
                var account = await _context.Accounts.Where(x => x.Email.Trim().ToLower()
                .Equals(email.Trim().ToLower())).FirstOrDefaultAsync();

                if (account == null) return false;

                var newPassword = PasswordGenerator.GenerateRandomPassword(8);

                var hashedPassword = PasswordHashUtility.HashPassword(newPassword);

                account.Password = hashedPassword;
                _context.Accounts.Update(account);
                await _context.SaveChangesAsync();

                await _emailService.SendEmail(account.Email, "Reset Password !!!", $"Your new password is: {newPassword}");

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
