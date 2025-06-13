using Test.Dto;

namespace Test.Services;

public interface IRecordService
{
    Task<RecordDto> CreateRecord(CreateRecordRequestDto requestDto);

    Task<IEnumerable<RecordDto>> GetRecords(DateTime? created, int? languageId, int? taskId);
}