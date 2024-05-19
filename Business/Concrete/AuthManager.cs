using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        
        //kullananıcı giriş yaptığında token üretmek için kullanılacak 
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            //boş gönderiliyor CreatePasswordHash metoduna parametre olarak gönderilerek CreatePasswordHash metodu bu byte dizilerine ilgili değerleri oluşturacak çünkü diziler referans tiplidir 
            byte[] passwordHash, passwordSalt;
            //gönderilen parametrelere göre password oluşturan metot
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            //kullanıcı oluşturma
            var user = new User
            {
                //Dto bilgisine göre kullanıcı oluşturuldu 
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                
                //CreatePasswordHash metodunun out keyword'ü ile işaretlene parametreler kullanılarak user nesnesinin password bilgileri oluşturuldu 
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                
                //kullanıcı aktif mi?
                Status = true
            };
            //kullanıcı veri tabanına eklendi 
            _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            //kullanıcının girdiği email bilgisine göre  veri tabanından kullanıcı bilgileri alındı 
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            
            //kullanıcı var mı kontrol edildi 
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            //kullanıcın girdiği şifre ile db'deki şifre karşılaştırıldı 
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            //giriş başarılı
            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            //kullanıcının rolleri alıundı 
            var claims = _userService.GetClaims(user);
            
            //kullanıcıya ve kullanıcının rollerine gore token üretildi 
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }
    }
}
