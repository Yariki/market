using Basket.Application.Common.Interfaces;
using Basket.Domain.Entities;
using MediatR;

namespace Basket.Application.SampleItems.Commands.CreateSampleItem;

public record CreateSampleItemCommand : IRequest<int>
{
    public string? Title { get; init; }
}

public class CreateTodoItemCommandHandler : IRequestHandler<CreateSampleItemCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateSampleItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new SampleItem
        {
            Title = request.Title,
            Done = false
        };

        _context.SampleItems.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
