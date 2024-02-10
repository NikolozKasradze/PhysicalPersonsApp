using FluentValidation;
using MediatR;

namespace PhysicalPersonsApp.Application;

public class Validator<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
       where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public Validator(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationTasks = _validators.Select(v => v.ValidateAsync(context, cancellationToken));
            var validationResults = await Task.WhenAll(validationTasks);

            var failures = validationResults
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .Select(x => x.ErrorMessage);

            if (failures.Any())
            {
                throw new Exceptions.ValidationException(failures);
            }
        }
        return await next();
    }

}
