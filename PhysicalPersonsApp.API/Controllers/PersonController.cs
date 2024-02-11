using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhysicalPersonsApp.API.Middlewares;
using PhysicalPersonsApp.Application.Features.Persons.Commands.Change;
using PhysicalPersonsApp.Application.Features.Persons.Commands.ChangeImage;
using PhysicalPersonsApp.Application.Features.Persons.Commands.Create;
using PhysicalPersonsApp.Application.Features.Persons.Commands.Delete;
using PhysicalPersonsApp.Application.Features.Persons.Queries.GetPersonDetails;
using PhysicalPersonsApp.Application.Features.Persons.Queries.GetPersonList;
using System.Net;

namespace PhysicalPersonsApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{

    private readonly IMediator _mediator;

    public PersonController(IMediator mediator)
    {
        _mediator = mediator;
    }


    /// <summary>
    /// Gets Person Info From DB
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<PersonVm>> Get([FromQuery] int id)
        => Ok(await _mediator.Send(new GetPersonDetailsQuery { Id = id }));


    /// <summary>
    /// Saves New Person In Database
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.Conflict)]
    public async Task<ActionResult<int>> Save([FromBody] CreatePersonCommand command)
        => Ok(await _mediator.Send(command));


    /// <summary>
    /// Updates Person Info In Database
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> Change([FromBody] ChangePersonCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }


    /// <summary>
    /// Adds Picture On Person
    /// </summary>
    /// <returns></returns>
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [HttpPost("Picture")]
    public async Task<ActionResult> AddPictureNew(int personId, IFormFile image)
    {
        await _mediator.Send(new AddPictureCommand { PersonId = personId, Image = image });
        return Ok();
    }



    /// <summary>
    /// Deletes Person From Database
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> Delete([FromQuery] int id)
    {
        await _mediator.Send(new DeletePersonCommand { Id = id });
        return Ok();
    }


    /// <summary>
    /// Fast Search Of Persons 
    /// </summary>
    /// <returns></returns>
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    [HttpGet("FastSearch")]
    public async Task<ActionResult<IEnumerable<PersonListVm>>> FastSearch([FromQuery] string firstName, string lastName, string personalN, int pageNum, int pageSize)
        => Ok(await _mediator.Send(new GetPersonsListQuery { FirstName = firstName, LastName = lastName, PersonalN = personalN, PageNum = pageNum, PageSize = pageSize }));


    /// <summary>
    /// Detailed Search Of Persons
    /// </summary>
    /// <returns></returns>
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    [HttpGet("DetailedSearch")]
    public async Task<ActionResult<IEnumerable<PersonListVm>>> DetailedSearch([FromQuery] GetPersonsListQuery query)
    => Ok(await _mediator.Send(query));

}
