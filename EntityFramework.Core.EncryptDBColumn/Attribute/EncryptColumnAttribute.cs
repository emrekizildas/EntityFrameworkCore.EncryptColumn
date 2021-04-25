using System;

namespace EntityFrameworkCore.EncryptColumn.Attribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class EncryptColumnAttribute : System.Attribute
    {
    }
}
