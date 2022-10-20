using EntityFrameworkCore.EncryptColumn.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EntityFrameworkCore.EncryptColumn.Converter
{
    internal sealed class EncryptionConverter : ValueConverter<string, string>
    {
        public EncryptionConverter(IEncryptionProvider encryptionProvider, ConverterMappingHints mappingHints = null) : base (x => encryptionProvider.Encrypt(x), x => encryptionProvider.Decrypt(x), mappingHints)
        {
        }
    }

    internal sealed class IntEncryptionConverter : ValueConverter<int, string>
    {
        public IntEncryptionConverter(IEncryptionProvider encryptionProvider, ConverterMappingHints mappingHints = null) : base(x => encryptionProvider.EncryptInt(x), x => encryptionProvider.DecryptInt(x), mappingHints)
        {
        }
    }
}
