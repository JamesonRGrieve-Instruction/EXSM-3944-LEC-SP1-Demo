using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DemoProject.Models;

[Table("job")]
public class Job
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int ID { get; set; }
    [Column("name")]
    public string Name { get; set; } = "";

    [InverseProperty(nameof(Person.Job))]
    [ValidateNever]
    public virtual ICollection<Person> People { get; set; }
}