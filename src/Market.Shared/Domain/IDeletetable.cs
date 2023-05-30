namespace Market.Shared.Domain;

public interface IDeletetable
{
    public bool IsDeleted { get; set; }
}