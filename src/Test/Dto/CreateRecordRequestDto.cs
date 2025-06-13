using System.Runtime.InteropServices.JavaScript;

namespace Test.Dto;

public class CreateRecordRequestDto
{
    public int LanguageId { get; set; }
    public int TaskId { get; set; }
    public string? TaskName { get; set; }
    public string? TaskDescription { get; set; }
    public int StudentId { get; set; }
    public JSType.BigInt ExecutionTime { get; set; }
    public DateTime? Created { get; set; }
}