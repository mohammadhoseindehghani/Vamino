namespace Vamino.Application.Contracts._Common;

public sealed class OperationResultDto
{
    public bool Succeeded { get; init; }
    public string[] Errors { get; init; } = [];
    public static OperationResultDto Success() => new()
        {
            Succeeded = true
        };
    public static OperationResultDto Failure(params string[] errors) => new()
        {
            Succeeded = false,
            Errors = errors
        };
}