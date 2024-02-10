using MediatR;

namespace PhysicalPersonsApp.Application.Features.ConnectedPersons.Queries.PersonConnectionsReport;

public class PersonConnectionsReportQuery : IRequest<IEnumerable<PersonConnectionReportVm>>
{
}
