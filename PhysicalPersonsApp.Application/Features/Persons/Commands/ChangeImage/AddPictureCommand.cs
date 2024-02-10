using MediatR;
using Microsoft.AspNetCore.Http;

namespace PhysicalPersonsApp.Application.Features.Persons.Commands.ChangeImage;

public class AddPictureCommand : IRequest
{
    public int PersonId { get; set; }
    public IFormFile Image { get; set; }

}
