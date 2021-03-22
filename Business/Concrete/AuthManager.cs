using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        IUserService _userService;
        ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user,claims.Data);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            
            var result = BusinessRules.Run(VerifyPassword(userForLoginDto));
            if (result==null)
            {
                var user = _userService.GetByMail(userForLoginDto.Email);
                return new SuccessDataResult<User>(user.Data, Messages.SuccessfullLogin);
            }
            return new ErrorDataResult<User>(result.Message);
        }
       
        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            { 
                Email=userForRegisterDto.Email,
                FirstName=userForRegisterDto.FirstName,
                LastName=userForRegisterDto.LastName,
                PasswordHash=passwordHash,
                PasswordSalt=passwordSalt,
                Status=true
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserAdded);
        }

        public IResult UserExist(string email)
        {
           
            if (!CheckIfUserExist(email).Success)
            {
                return new SuccessResult();
                
            }
            return new ErrorResult(Messages.UserAlreadyExist);


        }
        private IResult CheckIfUserExist(string email)
        {
            var userToCheck = _userService.GetByMail(email);
            if (userToCheck.Data == null)
            {
                return new ErrorResult(Messages.UserNotFound);
               
            }
            return new SuccessResult();

        }
        private IResult VerifyPassword(UserForLoginDto userForLoginDto )
        {
            
            if (CheckIfUserExist(userForLoginDto.Email).Success)
            {
                var checkUser = _userService.GetByMail(userForLoginDto.Email);
                var result= HashingHelper.VerifyPasswordHash(userForLoginDto.Password, checkUser.Data.PasswordHash, checkUser.Data.PasswordSalt);
                if (result)
                {
                    return new SuccessResult();
                }
            }
            return new ErrorResult(Messages.PasswordError);
        }
      
    }
}
