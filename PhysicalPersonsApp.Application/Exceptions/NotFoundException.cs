namespace PhysicalPersonsApp.Application.Exceptions;

public class NotFoundException : ApplicationException
{
    public NotFoundException(string name, string key) : base($"{name} ({key}) is not found")
    {

    }
}
