# EntityFrameworkCore.EncryptColumn
Hello, you can store your data in encrypted form in your database with this package. 

## How to use?
Install "Entity Framework Core.Encrypt Column" package to your project. 

Specify your encryption key in the constructor method of your DbContext class and create a instance from the encryption provider. 

```csharp
private readonly IEncryptionProvider _provider;
public ExampleDbContext()
{
    Initialize.EncryptionKey = "example_encrypt_key";
    this._provider = new GenerateEncryptionProvider();
}
```
Then specify that you will use an encryption provider in the "OnModelCreating" method. 

```csharp
modelBuilder.UseEncryption(this._provider);
```

That's it! Now you can encrypt the parameters in the class you want by adding the "EncryptColumn" attribute. 

```csharp
public class User
{
    public Guid ID { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    [EncryptColumn]
    public string EmailAddress { get; set; }
    [EncryptColumn]
    public string IdentityNumber { get; set; }
}
```

You can take a look at the [example project here](https://github.com/emrekizildas/EntityFrameworkCore.EncryptColumn.Example). 

## Contact
You can send an e-mail to kizildas@icloud.com for your problems related to the project. You can also create an issue in repository. 
