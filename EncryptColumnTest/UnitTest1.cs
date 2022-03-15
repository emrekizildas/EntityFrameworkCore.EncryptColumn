using EncryptColumnTest.Context;
using EncryptColumnTest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace EncryptColumnTest;
[TestClass]
public class UnitTest1
	{
	private static ExampleDbContext testContext=new();
	private User testUser = new User
			{
			Firstname = "Emre",
			Lastname = "Kizildas",
			EmailAddress = "kizildas@icloud.com",
			IdentityNumber = "12345678901"
			};

	public UnitTest1()
		{
		//testContext=new();
		}

	//[ClassInitialize()]
	//public static void Initialize()
	//	{
	//	testContext.Users.RemoveRange(testContext.Users); // clear the database.
	//	}

	[TestMethod]
	public void AddRecord()
		{
		try
			{
			testContext.Users.Add(testUser);
			testContext.SaveChanges();
			}
		catch(Exception Ex)
			{
			Assert.Fail($"Exception thrown on {nameof(AddRecord)}: {Ex.Message}");
			}
		}

	[TestMethod]
	public void ReadRecordByName()
		{
		try
			{
			var user = testContext.Users.FirstOrDefault(u => u.Firstname == testUser.Firstname);
			Assert.IsNotNull(user);
			Assert.IsNotNull(user.Firstname);
			Assert.AreEqual(testUser.Firstname, user.Firstname);
			Assert.AreEqual(testUser.IdentityNumber, user.IdentityNumber);
			Assert.AreEqual(testUser.EmailAddress, user.EmailAddress);
			}
		catch(Exception Ex)
			{
			Assert.Fail($"Exception thrown on {nameof(ReadRecordByName)}: {Ex.Message}");
			}
		}

	[TestMethod]
	public void ReadRecordByIdentityNumber()
		{
		try
			{
			var user = testContext.Users.FirstOrDefault(u => u.IdentityNumber == testUser.IdentityNumber);
			Assert.IsNotNull(user);
			Assert.IsNotNull(user.Firstname);
			Assert.AreEqual("Emre", user.Firstname);
			Assert.AreEqual("12345678901", user.IdentityNumber);
			Assert.AreEqual("kizildas@icloud.com", user.EmailAddress);
			}
		catch(Exception Ex)
			{
			Assert.Fail($"Exception thrown on {nameof(ReadRecordByName)}: {Ex.Message}");
			}
		}

	[TestMethod]
	public void AddAnotherRecord()
		{
		try
			{
			User newUser = new User { EmailAddress="jdubois31@outlook.com", Firstname="John", Lastname="Dubois", IdentityNumber="299792458" };
			testContext.Users.Add(newUser);
			testContext.SaveChanges();
			}
		catch (Exception Ex)
			{
			Assert.Fail($"Exception thrown on {nameof(AddAnotherRecord)}: {Ex.Message}");
			}
		}
	}