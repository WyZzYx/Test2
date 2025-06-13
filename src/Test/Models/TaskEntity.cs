using System.ComponentModel.DataAnnotations;

namespace Test.Models;

public class TaskEntity
{
    [Key]
    public int Id { get; set; }
    [Length(1, 100)]
    public string Name { get; set; }
    [Length(1, 2000)]
    public string Description { get; set; }
    
    public ICollection<Record> Records { get; set; } = new List<Record>();

}