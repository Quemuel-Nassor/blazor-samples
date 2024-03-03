using Microsoft.AspNetCore.Components;
namespace blazor_samples.Components.Wizard;

public partial class WizardComponent : ComponentBase
{
    [Parameter]
    public List<StepItem> Steps { get; set; } = new();

    void ChangeStep(StepItem step)
    {
        int i = Steps.IndexOf(step);

        if (i > 0)
            Steps[i - 1].Completed = true;

        for (int j = 0; j < Steps.Count; j++)
            Steps[j].Active = false;

        step.Active = true;
        step.Enabled = true;
    }    

    protected override void OnInitialized()
    {
        var step = Steps.FirstOrDefault();
        if(step != null) step.Active = true;
    }
}