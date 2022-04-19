using CM.Library.DataModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Library.Events.Person
{
    public class RegisterPersonCommand : IRequest<string>
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string JwtKey { get; set; }

        public string DefaultProfilePictureFileName { get; set; }

        public string DefaultProfilePictureFileExtension { get; set; }

        public string DefaultProfilePictureFilePath { get; set; }


        public RegisterPersonCommand(
            string userName,
            string password,
            string confirmPassword,
            string issuer, string audience,
            string jwtKey,
            string defaultProfilePictureFileName,
            string defaultProfilePictureFileExtension,
            string defaultProfilePictureFilePath)
        {
            UserName = userName;
            Password = password;
            ConfirmPassword = confirmPassword;
            Issuer = issuer;
            Audience = audience;
            JwtKey = jwtKey;
            DefaultProfilePictureFileName = defaultProfilePictureFileName;
            DefaultProfilePictureFileExtension = defaultProfilePictureFileExtension;
            DefaultProfilePictureFilePath = defaultProfilePictureFilePath;
        }

        public RegisterPersonCommand DeepCopy()
        {
            return (RegisterPersonCommand)this.MemberwiseClone();
        }
    }
}
