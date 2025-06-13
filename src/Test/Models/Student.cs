using System.ComponentModel.DataAnnotations;

namespace Test.Models;

public class Student
{
    public int Id { get; set; }
    [Length(1, 100)]
    public string Name { get; set; }
    [Length(1, 100)]
    public string LastName { get; set; }
    [Length(1, 250)]
    public string Email { get; set; }
    
    public ICollection<Record> Records { get; set; } = new List<Record>();

    
}