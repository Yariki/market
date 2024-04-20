using AutoMapper;
using Basket.Application.Basket.Models;
using Basket.Application.Common.Exceptions;
using Basket.Application.Common.Interfaces;
using FluentValidation;
using MediatR;

namespace Basket.Application.Basket.Queries;


public class GetBasketQuery : IRequest<BasketDto>
{
    public string CustomerId { get; set; }
}

public class GetBasketQueryHandler : IRequestHandler<GetBasketQuery, BasketDto>
{

    private readonly IBasketRepository _basketRepository;
    private readonly IMapper _mapper;

    public GetBasketQueryHandler(IBasketRepository basketRepository, IMapper mapper)
    {
        _basketRepository = basketRepository;
        _mapper = mapper;
    }

    public async Task<BasketDto> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        var basket = await _basketRepository.GetBasketAsync(request.CustomerId);

        if(basket == null)
        {
            throw new NotFoundException(nameof(request.CustomerId));
        }

        return _mapper.Map<BasketDto>(basket);
    }
}

public class GetBasketQueryValidator : AbstractValidator<GetBasketQuery>
{
    public GetBasketQueryValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
    }
}

