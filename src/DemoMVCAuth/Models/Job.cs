using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public virtual ICollection<Person> People { get; set; }
}