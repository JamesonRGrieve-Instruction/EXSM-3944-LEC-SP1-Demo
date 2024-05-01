using DemoProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DemoMVCAuth.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Person> People { get; set; }
    public virtual DbSet<Job> Jobs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasData([
                new Job() {
                    ID = -1,
                    Name = "Bus Driver"
                }
            ]);
        });

        modelBuilder.Entity<Person>(entity => // A Person
        {
            entity.HasOne(child => child.Job) // Has One Job
                .WithMany(parent => parent.People) // With Many People
                .HasForeignKey(child => child.JobID)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName($"FK_{nameof(Person)}_{nameof(Job)}");

            entity.HasIndex(entity => entity.JobID)
                .HasDatabaseName($"FK_{nameof(Person)}_{nameof(Job)}");

            entity.HasData(
                [
                    new Person() {
                        ID = -1,
                        FirstName = "John",
                        LastName = "Doe",
                        JobID = -1
                    },
                    new Person() {
                        ID = -2,
                        FirstName = "Jane",
                        LastName = "Doe",
                        JobID = -1
                    },
                ]
            );
        });
    }
}
