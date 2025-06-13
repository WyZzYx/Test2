using System.Runtime.InteropServices.JavaScript;

namespace Test.Dto;

public class RecordDto
{
    public int Id { get; set; }
    public LanguageDto Language { get; set; }
    public TaskDto Task { get; set; }
    public StudentDto Student { get; set; }
    public JSType.BigInt ExecutionTime { get; set; }
    public string Created { get; set; }
}