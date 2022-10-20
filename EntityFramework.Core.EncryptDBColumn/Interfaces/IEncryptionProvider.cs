using System;
namespace EntityFrameworkCore.EncryptColumn.Interfaces
{
    public interface IEncryptionProvider
    {
        string Encrypt(string dataToEncrypt);
        string Decrypt(string dataToDecrypt);

        string EncryptInt(int dataToEncrypt);
        int DecryptInt(string dataToDecrypt);
    }
}
