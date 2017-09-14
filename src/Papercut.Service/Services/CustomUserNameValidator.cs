using System;
using System.IdentityModel.Selectors;
using System.ServiceModel;

namespace Papercut.Service.Services
{
    public class CustomUserNameValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            Console.WriteLine($"Authenticating {userName}:{password}");
            if (userName == "test" && password == "test")
            {
                return;
            }

            throw new FaultException("Invalid username and/or password");
        }
    }
}