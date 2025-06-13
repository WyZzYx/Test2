using Microsoft.EntityFrameworkCore;
using Test.DAL;
using Test.Dto;
using Test.Models;

namespace Test.Services;

public class RecordService : IRecordService
{

    private readonly RecordManiaDbContext _context;

    public RecordService(RecordManiaDbContext context)
    {
        _context = context;
    }

    public async Task<RecordDto> CreateRecord(CreateRecordRequestDto requestDto)
    {
        var language = await _context.Language.FindAsync(requestDto.LanguageId);
        if (language == null)
            throw new Exception("Language not found");

        var student = await _context.Student.FindAsync(requestDto.StudentId);
        if (student == null)
            throw new Exception($"Student ID {requestDto.StudentId} not found.");

        TaskEntity taskEntity;
        var existingTask = await _context.Task.FindAsync(requestDto.TaskId);
        if (existingTask != null)
        {
            taskEntity = existingTask;
        }
        else
        {
            if (string.IsNullOrWhiteSpace(requestDto.TaskName) ||
                string.IsNullOrWhiteSpace(requestDto.TaskDescription))
            {
                throw new Exception("Task not found. Provide Name and Description to create it");
            }

            taskEntity = new TaskEntity
            {
                Name = requestDto.TaskName,
                Description = requestDto.TaskDescription
            };
            _context.Task.Add(taskEntity);
            await _context.SaveChangesAsync();
        }

        var record = new Record
        {
            LanguageId = language.Id,
            StudentId = student.Id,
            TaskId = taskEntity.Id,
            ExecutionTime = requestDto.ExecutionTime,
            Created = requestDto.Created ?? DateTime.UtcNow
        };

        _context.Record.Add(record);
        await _context.SaveChangesAsync();

        var dto = new RecordDto
        {
            Id = record.Id,
            Language = new LanguageDto { Id = language.Id, Name = language.Name },
            Task = new TaskDto { Id = taskEntity.Id, Name = taskEntity.Name, Description = taskEntity.Description },
            Student = new StudentDto
                { Id = student.Id, FirstName = student.Name, LastName = student.LastName, Email = student.Email },
            ExecutionTime = record.ExecutionTime,
            Created = record.Created.ToString("MM/dd/yyyy HH:mm:ss")
        };

        return dto;
    }

    public async Task<IEnumerable<RecordDto>> GetRecords(
        DateTime? created,
        int? languageId,
        int? taskId)
    {
        var query = _context.Record
            .Include(r => r.Language)
            .Include(r => r.Task)
            .Include(r => r.Student)
            .AsQueryable();

        if (created.HasValue)
            query = query.Where(r => r.Created.Date == created.Value.Date);

        if (languageId.HasValue)
            query = query.Where(r => r.LanguageId == languageId.Value);

        if (taskId.HasValue)
            query = query.Where(r => r.TaskId == taskId.Value);

        var records = await query
            .OrderByDescending(r => r.Created)
            .ThenBy(r => r.Student.LastName)
            .ToListAsync();

        var result = records.Select(r => new RecordDto
        {
            Id = r.Id,
            Language = new LanguageDto { Id = r.Language.Id, Name = r.Language.Name },
            Task = new TaskDto { Id = r.Task.Id, Name = r.Task.Name, Description = r.Task.Description },
            Student = new StudentDto
            {
                Id = r.Student.Id, FirstName = r.Student.Name, LastName = r.Student.LastName, Email = r.Student.Email
            },
            ExecutionTime = r.ExecutionTime,
            Created = r.Created.ToString("MM/dd/yyyy HH:mm:ss")
        }).ToList();

        return result;
    }
}