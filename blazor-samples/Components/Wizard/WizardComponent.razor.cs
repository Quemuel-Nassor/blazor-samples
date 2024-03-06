using Microsoft.AspNetCore.Components;
namespace blazor_samples.Components.Wizard;

public partial class WizardComponent : ComponentBase
{
    StepItem ActiveStep { get; set; }

    [Parameter]
    public List<StepItem> Steps { get; set; } = new();

    public void Next()
    {
        if (ActiveStep != null)
        {
            if (ActiveStep.Index < Steps.Count - 1)
            {
                ActiveStep = Steps[ActiveStep.Index + 1];
                ActiveStep.Active = true;
                ChangeStep(ActiveStep);
            }
        }
    }

    public void Previous()
    {
        if (ActiveStep != null)
        {
            if (ActiveStep.Index > 0)
            {
                ActiveStep = Steps[ActiveStep.Index - 1];
                ActiveStep.Active = true;
                ChangeStep(ActiveStep);
            }
        }
    }

    void ChangeStep(StepItem step)
    {
        step.Active = true;
        step.Enabled = true;
        ActiveStep = step;
        
        PublisherHandler.OnChanged(step);
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            ActiveStep = Steps.FirstOrDefault();
            if (ActiveStep != null)
            {
                ActiveStep.Active = true;
                ActiveStep.Enabled = true;
                PublisherHandler.OnChanged(ActiveStep);
            }
        }
    }
}