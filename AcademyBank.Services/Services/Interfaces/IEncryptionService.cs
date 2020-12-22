using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.BLL.Services.Interfaces
{
    public interface IEncryptionService
    {
        public string Encrypt(string planeText);
        public string Decrypt(string cyphre);
    }
}
