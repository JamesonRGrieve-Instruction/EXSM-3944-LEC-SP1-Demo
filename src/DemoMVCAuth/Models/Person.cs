using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DemoProject.Models;

[Table("person")]
public class Person
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int ID { get; set; }
    [Column("first_name", TypeName = "varchar(30)")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = "";
    [Column("last_name", TypeName = "varchar(30)")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = "";

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