using LevoHubBackend.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.Stages.Commands.UpdateStage;

public class UpdateStageCommandHandler : IRequestHandler<UpdateStageCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public UpdateStageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateStageCommand request, CancellationToken cancellationToken)
    {
        var stage = await _context.Stages.FindAsync(new object[] { request.Id }, cancellationToken);

        if (stage == null)
        {
            return false;
        }

        if (request.Name != null)
        {
            stage.Name = request.Name;
        }

        if (request.Description != null)
        {
            stage.Description = request.Description;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
