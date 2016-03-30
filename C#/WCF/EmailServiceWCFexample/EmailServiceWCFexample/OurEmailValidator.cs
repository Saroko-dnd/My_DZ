using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmailServiceWCFexample
{
    public class OurEmailValidator : IEmailValidator
    {
        public bool ValidateAddress(string EmailToValidate)
        {
            Regex CheckAddressRegex = new Regex(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@(([0-9a-zA-Z])+([-\w]*[0-9a-zA-Z])*\.)+[a-zA-Z]{2,9})$");

            return CheckAddressRegex.IsMatch(EmailToValidate);
        }
    }
}
