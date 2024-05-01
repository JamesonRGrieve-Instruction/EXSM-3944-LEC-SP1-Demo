using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Models;

[Table("person")]
public class Person
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int ID { get; set; }
    [Column("first_name", TypeName = "varchar(30)")]
    public string FirstName { get; set; } = "";
    [Column("last_name", TypeName = "varchar(30)")]
    public string LastName { get; set; } = "";

    [Column("job_id")]
    public int JobID { get; set; }

    [ForeignKey(nameof(JobID))]
    [InverseProperty(nameof(Models.Job.People))]
    public virtual Job Job { get; set; }
}