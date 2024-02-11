using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhysicalPersonsApp.API.Middlewares;
using PhysicalPersonsApp.Application.Features.ConnectedPersons.Commands;
using PhysicalPersonsApp.Application.Features.ConnectedPersons.Commands.Delete;
using PhysicalPersonsApp.Application.Features.ConnectedPersons.Queries.PersonConnectionsReport;
using System.Net;

namespace PhysicalPersonsApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonConnectionController : ControllerBase
{

    private readonly IMediator _mediator;

    public PersonConnectionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Adds connected person 
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.Conflict)]
    public async Task<ActionResult<int>> Add(CreatePersonConnectionCommand command)
        => Ok(await _mediator.Send(command));

    /// <summary>
    /// Deletes connected person
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        await _mediator.Send(new DeletePersonConnectionCommand { Id = id });
        return Ok();
    }

    /// <summary>
    /// Report of connected persons 
    /// </summary>
    /// <returns></returns>
    [HttpGet("Report")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<IEnumerable<PersonConnectionReportVm>>> ConnectedPersonsReport()
        => Ok(await _mediator.Send(new PersonConnectionsReportQuery()));

}
