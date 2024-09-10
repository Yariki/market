namespace Market.EventBus;

public abstract class BaseIntegrationEvent
{
    public Guid Id { get; private set; }

    public Guid CorrelationId { get; private set; }

    public DateTime CreationDate { get; private set; }

    public BaseIntegrationEvent()
    {
        Id = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
    }

    public BaseIntegrationEvent(Guid correlationId) : this()
    {
        CorrelationId = correlationId;
    }

    public void SetCorrelationId(Guid correlationId)
    {
        CorrelationId = correlationId;
    }

}
