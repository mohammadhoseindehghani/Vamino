using FluentValidation;
using MediatR;
using Vamino.Application.Contracts._Common;

namespace Vamino.Application.Behavior;

public sealed class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators
) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);

        var validationFailures = new List<FluentValidation.Results.ValidationFailure>();

        var validationTasks = validators.Select(v => v.ValidateAsync(context, cancellationToken));
        var results = await Task.WhenAll(validationTasks);

        validationFailures.AddRange(results.SelectMany(r => r.Errors));

        if (validationFailures.Count == 0)
            return await next();

        var errorMessages = validationFailures
            .Select(f => f.ErrorMessage)
            .Where(m => !string.IsNullOrWhiteSpace(m))
            .Distinct()
            .ToArray();

        throw new AppException("خطاهای اعتبارسنجی رخ داده است.", 400, errorMessages);
    }
}