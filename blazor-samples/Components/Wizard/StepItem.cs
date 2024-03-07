namespace blazor_samples.Components.Wizard;

public class StepItem
{
    static int indexStore;   
    public string Name { get; set; }
    public int Index { get; private set; }
    public string Slug => Name?.Replace(" ", "-").ToLower().Trim();
    public string Icon { get; set; }
    public bool Active { get; set; }
    public bool Enabled { get; set; }
    public bool Completed { get; private set; }

    public string GetClass() => $" {(Active ? "active" : "")} {(Completed ? "completed" : "")} ";

    public StepItem()
    {
        Index = indexStore;
        PublisherHandler.Publisher.StepItemChangedEvent += ChangedEventHandler;
        PublisherHandler.Publisher.EnableAllStepsEvent += EnableStepEventHandler;
        indexStore++;
    }

    void ChangedEventHandler(Object sender, StepItem e)
    {
        if (e.Index != Index)
            this.Active = false;

        if (e.Index > Index)
            this.Completed = true;
    }

    void EnableStepEventHandler(Object sender, bool e)
    {
        Enabled = true;
    }

    ~StepItem(){
        PublisherHandler.Publisher.StepItemChangedEvent -= ChangedEventHandler;
        PublisherHandler.Publisher.EnableAllStepsEvent -= EnableStepEventHandler;
        indexStore--;
    }
}

public class StepItemEventPublisher
{
    public event EventHandler<StepItem> StepItemChangedEvent;
    public event EventHandler<bool> EnableAllStepsEvent;

    public void EnableAllSteps() => EnableAllStepsEvent(this, true);
    public void OnChanged(StepItem changedItem) => StepItemChangedEvent(this, changedItem);
}

public static class PublisherHandler
{
    public static readonly StepItemEventPublisher Publisher = new();

    public static void OnChanged(StepItem e) => Publisher.OnChanged(e);

    public static void EnableAllSteps() => Publisher.EnableAllSteps();
}
