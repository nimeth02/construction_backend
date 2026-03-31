using MediatR;
using System.Collections.Generic;

namespace LevoHubBackend.Application.Features.Templates.Commands.CreateTemplate;

public class CreateTemplateCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<int> DepartmentIds { get; set; } = new();
}
