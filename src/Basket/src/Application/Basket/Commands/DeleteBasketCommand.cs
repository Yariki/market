using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basket.Application.Common.Interfaces;
using FluentValidation;
using MediatR;

namespace Basket.Application.Basket.Commands;
public class DeleteBasketCommand : IRequest<Unit>
{
    public string CustomerId { get;set;}
}

public class DeleteBasketCommandHandler : IRequestHandler<DeleteBasketCommand, Unit>
{
    private readonly IBasketRepository _basketRepository;

    public DeleteBasketCommandHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    public async Task<Unit> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        await _basketRepository.DeleteBasketAsync(request.CustomerId);

        return Unit.Value;
    }
}

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
    }
}
