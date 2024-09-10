namespace Market.EventBus;

public abstract class BaseIntegrationEvent
{
    public Guid Id { get; private set; }

    public string CorrelationId { get; private set; }

    public DateTime CreationDate { get; private set; }

    protected BaseIntegrationEvent()
    {
        Id = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
    }

    protected BaseIntegrationEvent(string correlationId) : this()
    {
        CorrelationId = correlationId;
    }

    public void SetCorrelationId(string correlationId)
    {
        CorrelationId = correlationId;
    }

}
