using Market.Shared.Domain;

namespace Market.Identity.Api.Data;

public class CardInfo : BaseIdEntity
{
    
    public string UserId { get; set; }
    
    public virtual AuthUser User { get; set; }
    
    public string Name { get; set; }
    
    public string CardNumber { get; set; }
    
    public string CardHolderName { get; set; }
    
    public int ExpirationMonth { get; set; }
    
    public int ExpirationYear { get; set; }
    
    public int Cvv { get; set; }

    public static CardInfo Create(string name, string cardNumber, string cardHolderName, int expirationMonth, int expirationYear, int cvv)
    {
        return new CardInfo
        {
            Id = Guid.NewGuid(),
            Name = name,
            CardNumber = cardNumber,
            CardHolderName = cardHolderName,
            ExpirationMonth = expirationMonth,
            ExpirationYear = expirationYear,
            Cvv = cvv
        };
    }
}