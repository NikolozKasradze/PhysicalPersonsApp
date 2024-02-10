namespace PhysicalPersonsApp.Application.Exceptions;

public class ValidationException : ApplicationException
{
    public IEnumerable<string> ValidationErrors { get; set; }
    public ValidationException(IEnumerable<string> failures)
    {
        ValidationErrors = failures.ToList();
    }
}
