using Market.Shared.Domain;
using Market.Shared.Infrastructure.Common.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Market.Identity.Api.Data;

public class AuthUser : IdentityUser
{
    private List<CardInfo> _cardInfos = new();

    public string ProfileImageName { get; set; } = string.Empty;
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public IReadOnlyCollection<CardInfo> Cards => _cardInfos;
    
    public void AddCard(CardInfo cardInfo)
    {
        _cardInfos.Add(cardInfo);
    }
    
    public void RemoveCard(Guid cardId)
    {

        var card = _cardInfos.FirstOrDefault(c => c.Id == cardId);
        if (card.IsNull())
        {
            throw new MarketException("Information about card not found");
        }
        
        _cardInfos.Remove(card);
    }
}


