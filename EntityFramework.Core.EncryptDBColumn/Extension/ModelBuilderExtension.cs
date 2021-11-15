using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Converter;
using EntityFrameworkCore.EncryptColumn.Attribute;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;

namespace EntityFrameworkCore.EncryptColumn.Extension
{
    public static class ModelBuilderExtension
    {
        public static void UseEncryption(this ModelBuilder modelBuilder, IEncryptionProvider encryptionProvider)
        {
            if (modelBuilder is null)
                throw new ArgumentNullException(nameof(modelBuilder), "There is not ModelBuilder object.");
            if (encryptionProvider is null)
                throw new ArgumentNullException(nameof(encryptionProvider), "You should create encryption provider.");

            var encryptionConverter = new EncryptionConverter(encryptionProvider);
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (IMutableProperty property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(string) && !IsDiscriminator(property))
                    {
                        object[] attributes = property.PropertyInfo.GetCustomAttributes(typeof(EncryptColumnAttribute), false);
                        if (attributes.Any())
                            property.SetValueConverter(encryptionConverter);
                    }
                }
            }

        }

        private static bool IsDiscriminator(IMutableProperty property) => property.Name == "Discriminator" || property.PropertyInfo == null;
    }
}
