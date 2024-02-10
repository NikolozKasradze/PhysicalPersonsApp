namespace PhysicalPersonsApp.Application.Exceptions;

public class DuplicateException : ApplicationException
{
    public DuplicateException(string message) : base(message)
    {

    }
}
