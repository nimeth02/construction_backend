using LevoHubBackend.Application.DTOs.Templates;
using MediatR;

namespace LevoHubBackend.Application.Features.Templates.Queries.GetTemplateById;

public class GetTemplateByIdQuery : IRequest<TemplateDto>
{
    public int Id { get; set; }
}
