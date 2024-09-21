namespace Market.Shared.Domain;

public interface IEntity<TId>
{
    public TId Id { get; set; }
}