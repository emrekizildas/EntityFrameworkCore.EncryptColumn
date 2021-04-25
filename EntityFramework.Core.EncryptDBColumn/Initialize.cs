using System;

namespace EntityFrameworkCore.EncryptColumn
{
    public static class Initialize
    {
        public static string EncryptionKey = string.Empty;

        public static void Key(string key) => EncryptionKey = key;
    }
}
