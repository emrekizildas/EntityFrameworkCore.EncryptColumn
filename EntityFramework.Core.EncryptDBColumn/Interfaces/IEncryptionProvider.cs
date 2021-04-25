using System;
namespace EntityFrameworkCore.EncryptColumn.Interfaces
{
    public interface IEncryptionProvider
    {
        string Encrypt(string dataToEncrypt);
        string Decrypt(string dataToDecrypt);
    }
}
