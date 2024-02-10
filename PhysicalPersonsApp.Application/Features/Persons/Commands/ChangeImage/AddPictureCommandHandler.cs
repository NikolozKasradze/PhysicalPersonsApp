using MediatR;
using PhysicalPersonsApp.Application.Contracts.Persistance;
using PhysicalPersonsApp.Domain;

namespace PhysicalPersonsApp.Application.Features.Persons.Commands.ChangeImage;

public class AddPictureCommandHandler : IRequestHandler<AddPictureCommand>
{
    private readonly IRepository<Person> _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddPictureCommandHandler(IRepository<Person> personRepository, IUnitOfWork unitOfWork)
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;

    }

    public async Task Handle(AddPictureCommand request, CancellationToken cancellationToken)
    {
        if (request.Image.Length > 0)
        {
            var person = await _personRepository.GetOneByIdAsync(request.PersonId);

            string path = $"Files/Images/Person/{request.PersonId}/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = Path.GetFileName(request.Image.FileName);
            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                request.Image.CopyTo(stream);
            }

            person.Image = Path.Combine(path, fileName);

            await _unitOfWork.Persons.UpdateAsync(person);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
