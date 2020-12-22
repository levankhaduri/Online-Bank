using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.BLL.Services
{
    public class Constants
    {
        public const string Key = "1458752398565478";
        public const string IV = "1597531562547853";
        public const string PhoneNumberPattern = @"^([+]995)[\s-]?[5]\d{1,3}[\s-]?\d{1,3}[\s-]?\d{1,3}[\s-]?\d{1,3}$";
        public const string AdminRole = "Admin";
        public const string OnlyAdmin = "OnlyAdmin";
        public const string OnlyUser = "OnlyUser";
        public const string User = "User";

        //MailSender Constants
        public const string mailHost = "smtp.gmail.com";
        public const string webAddress = "academybanktbc@gmail.com";
        public const string password = "Testtest123@";
    }
}
