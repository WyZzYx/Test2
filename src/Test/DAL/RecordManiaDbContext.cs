using Microsoft.EntityFrameworkCore;
using Test.Models;

namespace Test.Data;

public class RecordManiaDbContext : DbContext
{
    public RecordManiaDbContext(DbContextOptions<RecordManiaDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<Language> Language { get; set; } = null!;
    public DbSet<TaskEntity> Task { get; set; } = null!;
    public DbSet<Student> Student { get; set; } = null!;
    public DbSet<Record> Record { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Record>(entity =>
        {
            entity.HasOne(r => r.Language)
                .WithMany(l => l.Records)
                .HasForeignKey(r => r.LanguageId);


            entity.HasOne(r => r.Task)
                .WithMany(t => t.Records)
                .HasForeignKey(r => r.TaskId);

            entity.HasOne(r => r.Student)
                .WithMany(s => s.Records)
                .HasForeignKey(r => r.StudentId);
        });
        
        modelBuilder.Entity<Language>().HasData(new List<Language>()
        {
            new (){Id = 1, Name = "English"}
        });
        
        modelBuilder.Entity<Student>().HasData(new List<Student>()
        {
            new (){Id = 1, Name = "Vitalii", LastName = "Koltok", Email = "Winner@gmail.com"}
        });
        
        modelBuilder.Entity<Record>().HasData(new List<Record>()
        {
            new (){Id = 1, LanguageId = 1, StudentId = 1, TaskId = 1, Created = DateTime.UtcNow}
        });
        
        modelBuilder.Entity<TaskEntity>().HasData(new List<TaskEntity>()
        {
            new (){Id = 1, Name = "Task1", Description = "Desc1"}
        });
        
    }
}