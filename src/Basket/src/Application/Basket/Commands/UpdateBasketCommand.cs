using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using Basket.Application.Basket.Models;
using Basket.Application.Common.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Application.Basket.Commands;
public class UpdateBasketCommand : IRequest<BasketDto>
{
    public Guid UserId { get; set; }

    public BasketDto Basket { get; set; }

}

public class  UpdateBasketCommandHandler : IRequestHandler<UpdateBasketCommand, BasketDto>
{
    private readonly IBasketRepository _basketRepository;
    private readonly IMapper _mapper;

    public UpdateBasketCommandHandler(IBasketRepository basketRepository, IMapper mapper)
    {
        _basketRepository = basketRepository;
        _mapper = mapper;
    }

    public async Task<BasketDto> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
    {
        var basket = await _basketRepository.UpdateBasketAsync(_mapper.Map<Domain.Entities.Basket>(request.Basket));
        return _mapper.Map<BasketDto>(basket);
    }
}

public class UpdateBasketCommandValidator : AbstractValidator<UpdateBasketCommand>
{
    public UpdateBasketCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Basket).NotNull();
    }
}

