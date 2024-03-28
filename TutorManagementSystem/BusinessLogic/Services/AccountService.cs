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

        public AccountService(ClassNTutorContext context, IConfiguration config,
            IEmailService emailService, IS3StorageService s3storageService)
        {
            _context = context;
            _config = config;
            _emailService = emailService;
            _s3storageService = s3storageService;
        }

        public async Task<Account> AddAccount(RegisterDto entity)
        {
            try
            {
                var lastPersonID = await _context.Accounts.OrderBy(x => x.PersonId).LastOrDefaultAsync().ConfigureAwait(false);

                var newAccount = new Account
                {
                    PersonId = lastPersonID!.PersonId + 1,
                    Email = entity.Email!,
                    Password = PasswordHashUtility.HashPassword(entity.Password!),
                    Status = "Active",
                    RoleId = entity.RoleId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                await _context.Accounts.AddAsync(newAccount).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);

                var findAccount = await _context.Accounts.Where(x => x.Email.Equals(newAccount.Email))
                    .FirstOrDefaultAsync().ConfigureAwait(false);

                return findAccount;
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
                var newAccount = await AddAccount(entity).ConfigureAwait(false);

                if (newAccount == null)
                {
                    return null;
                }

                if (!FileHelper.IsImage(entity.Avatar.FileName))
                {
                    return null;
                }

                var avatar = await _s3storageService.UploadFileToS3(entity.Avatar!).ConfigureAwait(false);

                var newPerson = new Person
                {
                    PersonId = newAccount.PersonId,
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

        public async Task<string> GetEmail(string token)
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
                var email = jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

                return email;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> Login(LoginDto entity)
        {
            try
            {
                var account = await _context.Accounts.Include(x => x.Role).Where(x => x.Email.Trim().ToLower()
                .Equals(entity.Email!.Trim().ToLower())).FirstOrDefaultAsync();

                if (account == null ||
                    !PasswordHashUtility.VerifyPassword(entity.Password!, account.Password)) return null;

                if (account.Status.ToLower().Equals("Active".ToLower())) return null;

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                 new Claim(ClaimTypes.Email, account.Email),
                 new Claim(ClaimTypes.Role, account.Role.RoleName),
                };

                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
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
                var newPerson = await AddPerson(entity).ConfigureAwait(false);

                if (newPerson == null) return false;

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
                var newPerson = await AddPerson(entity).ConfigureAwait(false);

                if (newPerson == null) return false;

                var newStaff = new Staff
                {
                    PersonId = newPerson.PersonId,
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
                var newPerson = await AddPerson(entity).ConfigureAwait(false);

                if (newPerson == null) return false;

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
                    PersonId = newPerson.PersonId,
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
