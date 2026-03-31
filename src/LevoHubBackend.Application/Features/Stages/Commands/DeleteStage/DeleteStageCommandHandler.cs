using LevoHubBackend.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LevoHubBackend.Application.Features.Stages.Commands.DeleteStage;

public class DeleteStageCommandHandler : IRequestHandler<DeleteStageCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeleteStageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteStageCommand request, CancellationToken cancellationToken)
    {
        var stage = await _context.Stages.FindAsync(new object[] { request.Id }, cancellationToken);

        if (stage == null)
        {
            return false;
        }

        _context.Stages.Remove(stage);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
