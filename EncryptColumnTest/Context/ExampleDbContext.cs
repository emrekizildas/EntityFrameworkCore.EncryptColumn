using EncryptColumnTest.Models;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using EntityFrameworkCore.EncryptColumn;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkCore.EncryptColumn.Extension;

namespace EncryptColumnTest.Context;

public class ExampleDbContext:DbContext
  {
  private readonly IEncryptionProvider _provider;
  public ExampleDbContext()
    {
    Initialize.EncryptionKey = "example_encrypt_key_must be 32 b";
    this._provider = new GenerateEncryptionProvider();
    }

  public DbSet<User> Users { get; set; }
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EncryptColumnTest;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

  protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    modelBuilder.UseEncryption(this._provider);
    //modelBuilder.HasDefaultSchema("dbo");
    //modelBuilder.Entity<User>(entity =>
    //{
    //  entity.ToTable("Users");
    //  entity.HasKey(u => u.ID).HasName("PK_User_Id");
    //  entity.Property(p => p.ID).HasColumnType("GUID").IsRequired().ValueGeneratedOnAdd();
    //});
    base.OnModelCreating(modelBuilder);
    }
  }

