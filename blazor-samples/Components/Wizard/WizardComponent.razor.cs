using Microsoft.AspNetCore.Components;
namespace blazor_samples.Components.Wizard;

public partial class WizardComponent : ComponentBase
{
    StepItem ActiveStep { get; set; }

    [Parameter]
    public List<StepItem> Steps { get; set; } = new();

    [Parameter]
    public EventCallback<List<StepItem>> StepsChanged { get; set; }

    public async Task EnableAllSteps()
    {
        PublisherHandler.EnableAllSteps();
        await StepsChanged.InvokeAsync(Steps);
    }

    public async Task Next()
    {
        if (ActiveStep != null)
        {
            if (ActiveStep.Index < Steps.Count - 1)
                await ChangeStep( Steps[ActiveStep.Index + 1]);
        }
    }

    public async Task Previous()
    {
        if (ActiveStep != null)
        {
            if (ActiveStep.Index > 0)
                await ChangeStep(Steps[ActiveStep.Index - 1]);
        }
    }

    async Task ChangeStep(StepItem step)
    {
        step.Active = true;
        step.Enabled = true;
        ActiveStep = step;
        
        PublisherHandler.OnChanged(step);
        await StepsChanged.InvokeAsync(Steps);
    }

    protected override async void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            ActiveStep = Steps.FirstOrDefault();
            if (ActiveStep != null)
                await ChangeStep(ActiveStep);
        }
    }
}