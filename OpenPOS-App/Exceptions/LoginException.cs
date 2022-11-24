using OpenPOS_APP.Exceptions.Interfaces;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OpenPOS_APP.Exceptions
{
    public class LoginException : IException

    {
        public List<string> Errors { get; }

        public LoginException (string email, string password)
        {
            Errors = new List<string>();

            

            var user = UserService.Authenticate(email, password);
            if (user != null)
            {
                ApplicationSettings.LoggedinUser = user;
            } else
            {
                Errors.Add("Ongeldige gebruikersnaam en wachtwoord combinatie");
            }
        }
    }
}
