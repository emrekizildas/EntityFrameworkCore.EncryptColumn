using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using EntityFrameworkCore.EncryptColumn.Interfaces;

namespace EntityFrameworkCore.EncryptColumn.Util;

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
            
        var iv = new byte[16];
        byte[] array;

        using (var aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (var streamWriter = new StreamWriter((Stream)cryptoStream))
                    {
                        streamWriter.Write(dataToEncrypt);
                    }
                    array = memoryStream.ToArray();
                }
            }
        }
        var result = Convert.ToBase64String(array);
        return result;
    }

    public string Decrypt(string dataToDecrypt)
    {
        if (string.IsNullOrEmpty(key))
            throw new ArgumentNullException("EncryptionKey", "Please initialize your encryption key.");

        if (string.IsNullOrEmpty(dataToDecrypt))
            return string.Empty;
            
        var iv = new byte[16];

        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.IV = iv;
        var decrypter = aes.CreateDecryptor(aes.Key, aes.IV);

        var buffer = Convert.FromBase64String(dataToDecrypt);
        using var memoryStream = new MemoryStream(buffer);
        using var cryptoStream = new CryptoStream((Stream)memoryStream, decrypter, CryptoStreamMode.Read);
        using var streamReader = new StreamReader((Stream)cryptoStream);
        return streamReader.ReadToEnd();
    }
}
