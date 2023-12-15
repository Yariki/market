namespace Orders.Domain.Entities;

public class SampleItem : BaseAuditableEntity<int>
{
    public string? Title { get; set; }

    public string? Note { get; set; }

    public PriorityLevel Priority { get; set; }

    public DateTime? Reminder { get; set; }

    private bool _done;
    public bool Done
    {
        get => _done;
        set
        {
            if (value && !_done)
            {
                AddDomainEvent(new SampleItemCompletedEvent(this));
            }

            _done = value;
        }
    }
}
