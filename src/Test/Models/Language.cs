using System.ComponentModel.DataAnnotations;

namespace Test.Models;

public class Language
{
    public int Id { get; set; }
    [Length(1, 100)]
    public string Name { get; set; }
    
    public ICollection<Record> Records { get; set; } = new List<Record>();

}