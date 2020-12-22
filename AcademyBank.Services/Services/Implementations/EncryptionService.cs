using AcademyBank.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AcademyBank.BLL.Services.Implementations
{
    public class EncryptionService : IEncryptionService
    {
        public AesManaged aes { get; set; }
        public EncryptionService()
        {
            aes = new AesManaged();
        }

        public string Encrypt(string planeText)
        {
            byte[] keyBytes = Encoding.ASCII.GetBytes(Constants.Key);
            byte[] IVBytes = Encoding.ASCII.GetBytes(Constants.IV);
            var getEncrypt = aes.CreateEncryptor(keyBytes, IVBytes);
            MemoryStream memory = new MemoryStream();
            CryptoStream cryptstream = new CryptoStream(memory, getEncrypt, CryptoStreamMode.Write);
            using (StreamWriter sw = new StreamWriter(cryptstream))
                sw.Write(planeText);
            byte[] encrypted = memory.ToArray();
            string cyphre = ByteArrayToString(encrypted);
            return cyphre;
        }
        public string Decrypt(string cyphre)
        {
            byte[] byteArr = StringToByteArr(cyphre);
            byte[] keyBytes = Encoding.ASCII.GetBytes(Constants.Key);
            byte[] IVBytes = Encoding.ASCII.GetBytes(Constants.IV);
            ICryptoTransform decryptor = aes.CreateDecryptor(keyBytes, IVBytes);
            MemoryStream ms2 = new MemoryStream(byteArr);
            CryptoStream cs2 = new CryptoStream(ms2, decryptor, CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cs2);
            var decrypted = reader.ReadToEnd();
            return decrypted;
        }

        private string ByteArrayToString(byte[] arr)
        {
            string str = "";
            for (int i = 0; i < arr.Length; i++)
            {
                if (i != arr.Length - 1) str += arr[i] + ",";
                else str += arr[i];
            }
            return str;
        }
        private byte[] StringToByteArr(string cyphre)
        {
            string[] arr = cyphre.Split(',');
            byte[] byteArr = new byte[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                byte b;
                bool readCorr = Byte.TryParse(arr[i], out b);
                byteArr[i] = b;
            }
            return byteArr;
        }
    }
}
