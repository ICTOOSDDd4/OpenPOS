using OpenPOS_APP.Exceptions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OpenPOS_APP.Exceptions
{
    public class RegisterException : IException
    {
        public List<string> Errors { get; }

        public RegisterException(string name, string email, string password, string repeatPassword) 
        { 
            CheckEmail(email);
            CheckPassword(password);
        }

        private void CheckEmail(string email)
        {
            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

            if (Regex.IsMatch(email, regex, RegexOptions.IgnoreCase))
            {
                Errors.Add("Voer een valide email in");
            }
        }

        private void CheckPassword(string password) 
        {
        
        }
    }
}
