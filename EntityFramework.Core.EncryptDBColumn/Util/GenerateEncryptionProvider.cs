﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using EntityFrameworkCore.EncryptColumn.Interfaces;

namespace EntityFrameworkCore.EncryptColumn.Util
{
    public class GenerateEncryptionProvider : IEncryptionProvider
    {
        private readonly string key;
        public GenerateEncryptionProvider(string key)
        {
            this.key = key;
        }

        public string Encrypt(string dataToEncrypt)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("EncryptionKey", "Please initialize your encryption key.");

            if (string.IsNullOrEmpty(dataToEncrypt))
                return string.Empty;
                
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(dataToEncrypt);
                        }
                        array = memoryStream.ToArray();
                    }
                }
            }
            string result = Convert.ToBase64String(array);
            return result;
        }

        public string Decrypt(string dataToDecrypt)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("EncryptionKey", "Please initialize your encryption key.");

            if (string.IsNullOrEmpty(dataToDecrypt))
                return string.Empty;
                
            byte[] iv = new byte[16];

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                var buffer = Convert.FromBase64String(dataToDecrypt);
                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        public string EncryptInt(int dataToEncrypt)
        {
            return Encrypt(dataToEncrypt.ToString());
        }

        public int DecryptInt(string dataToDecrypt)
        {
            return int.Parse(Decrypt(dataToDecrypt));
        }
    }
}
