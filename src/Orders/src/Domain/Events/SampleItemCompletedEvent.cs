namespace Orders.Domain.Events;

public class SampleItemCompletedEvent : BaseEvent
{
    public SampleItemCompletedEvent(SampleItem item)
    {
        Item = item;
    }

    public SampleItem Item { get; }
}
