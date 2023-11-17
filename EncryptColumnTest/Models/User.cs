using EntityFrameworkCore.EncryptColumn.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptColumnTest.Models;

public class User
  {
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public Guid? ID { get; set; }
  public string? Firstname { get; set; }
  public string? Lastname { get; set; }
  [EncryptColumn]
  public string? EmailAddress { get; set; }
  [EncryptColumn]
  public string? IdentityNumber { get; set; }
  }
