using AutoMapper;
using Azure.Messaging;
using Microsoft.AspNetCore.Mvc;
using SEP490_BackEnd.DTO.AccountProfile;
using SEP490_BackEnd.Models;
using SEP490_BackEnd.Repositories.Interface;
using SEP490_BackEnd.ResponseModel.AccountProfile;
using SEP490_BackEnd.Services.Interface;

namespace SEP490_BackEnd.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountServices(IAccountRepository accountRepository,
            IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetAccountByPersonId(int personId)
        {
            //Find user by personId
            Account? account = _accountRepository.GetAccountByPersonId(personId);

            //Check if account is exist
            if (account == null)
            {
                //return status code 500 if account is not exist
                return new ObjectResult("Không tìm thấy tài khoản này")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            //If account is exist, map account to AccountReponse
            AccountResponse accountResponse = _mapper.Map<AccountResponse>(account);

            //return status code 200 with accountResponse
            return new ObjectResult(accountResponse)
            {
                StatusCode = StatusCodes.Status200OK
            };
        }

        public async Task<IActionResult> UpdateProfile(AccountProfileDTO accountProfileDTO)
        {
            //Find user by personId
            Account? account = _accountRepository.GetAccountByPersonId(accountProfileDTO.PersonId);

            //Check if account is exist
            if (account == null)
            {
                //return status code 500 if account is not exist
                return new ObjectResult("Không tìm thấy tài khoản này")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            //Update account with new information
            account.Person.FullName = accountProfileDTO.FullName;
            account.Person.UserAvatar = accountProfileDTO.UserAvatar;
            account.Person.Phone = accountProfileDTO.Phone;
            account.Person.Gender = accountProfileDTO.Gender;
            account.Person.Address = accountProfileDTO.Address;
            account.Person.Dob = accountProfileDTO.Dob;

            account.Person.Tutor.Cmnd = accountProfileDTO.Cmnd;
            account.Person.Tutor.FrontCmnd = accountProfileDTO.FrontCmnd;
            account.Person.Tutor.BackCmnd = accountProfileDTO.BackCmnd;
            account.Person.Tutor.Cv = accountProfileDTO.Cv;
            account.Person.Tutor.EducationLevel = accountProfileDTO.EducationLevel;
            account.Person.Tutor.School = accountProfileDTO.School;
            account.Person.Tutor.GraduationYear = accountProfileDTO.GraduationYear;
            account.Person.Tutor.About = accountProfileDTO.About;

            //Update account to database
            bool isUpdated = _accountRepository.UpdateAccountProfile(account);

            //Check if account is updated
            if (!isUpdated)
            {
                //return status code 500 if account is not updated
                return new ObjectResult("Cập nhật thông tin thất bại")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            //return status code 200 if account is updated
            return new ObjectResult("Cập nhật thông tin thành công")
            {
                StatusCode = StatusCodes.Status200OK
            };

        }
    }
}
