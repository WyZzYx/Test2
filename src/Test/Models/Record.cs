using System.Runtime.InteropServices.JavaScript;

namespace Test.Models;

public class Record
{
    public int Id { get; set; }
    public int LanguageId { get; set; }
    public Language Language { get; set; }

    public int TaskId { get; set; }
    public TaskEntity Task { get; set; }
    
    public int StudentId { get; set; }
    public Student Student { get; set; }
    
    public int ExecutionTime { get; set; }
    public DateTime Created { get; set; }

}