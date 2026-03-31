using LevoHubBackend.Application.DTOs.Templates;
using MediatR;
using System.Collections.Generic;

namespace LevoHubBackend.Application.Features.Templates.Queries.GetTemplates;

public class GetTemplatesQuery : IRequest<List<TemplateDto>>
{
}
