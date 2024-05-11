using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DemoProject.Models;

[Table("industry")]
public class Industry
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int ID { get; set; }
    [Column("name")]
    public string Name { get; set; } = "";

    [InverseProperty(nameof(Job.Industry))]
    [ValidateNever]
    public virtual ICollection<Job> Jobs { get; set; }
}