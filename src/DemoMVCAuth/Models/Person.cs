using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DemoProject.Models;

public class PhoneNumberFormat : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is string)
        {
            string[] phoneNumberParts = ((string)value).Split('-');
            return phoneNumberParts[0].Length == 3 && phoneNumberParts[1].Length == 3 && phoneNumberParts[2].Length == 4;
        }
        else
        {
            return false;
        }
    }
}
[Table("person")]
public class Person
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int ID { get; set; }
    [Required]
    [MaxLength(30)]
    [Column("first_name", TypeName = "varchar(30)")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = "";
    [Required]
    [MaxLength(30)]
    [Column("last_name", TypeName = "varchar(30)")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = "";
    [Required]
    [StringLength(12, MinimumLength = 12)]
    [PhoneNumberFormat(ErrorMessage = "Phone number must be in the format XXX-XXX-XXXX.")]
    [Column("phone_number", TypeName = "char(12)")]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; } = "";


    [Column("job_id")]
    [Display(Name = "Job")]
    public int JobID { get; set; }

    [ForeignKey(nameof(JobID))]
    [InverseProperty(nameof(Models.Job.People))]
    [ValidateNever]
    public virtual Job Job { get; set; }

    [Column("user_id")]
    public string? UserID { get; set; }

    [ForeignKey(nameof(UserID))]
    [ValidateNever]
    public virtual IdentityUser User { get; set; }
}
