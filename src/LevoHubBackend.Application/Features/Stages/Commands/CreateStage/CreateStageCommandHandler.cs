using LevoHubBackend.Application.Interfaces;
using LevoHubBackend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.Stages.Commands.CreateStage;

public class CreateStageCommandHandler : IRequestHandler<CreateStageCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateStageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateStageCommand request, CancellationToken cancellationToken)
    {
        if (await _context.Stages.AnyAsync(s => s.Name == request.Name, cancellationToken))
        {
            throw new ArgumentException("A stage with the same name already exists.");
        }

        var stage = new Stage
        {
            Name = request.Name,
            Description = request.Description,
            DepartmentId = request.DepartmentId
        };

        _context.Stages.Add(stage);
        await _context.SaveChangesAsync(cancellationToken);

        return stage.Id;
    }
}
