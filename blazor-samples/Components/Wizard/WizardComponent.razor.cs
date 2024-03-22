using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
namespace blazor_samples.Components.Wizard;

public partial class WizardComponent : ComponentBase, IAsyncDisposable
{
    [Inject]
    IJSRuntime JsRuntime { get; set; }
    IJSObjectReference jsModule;

    [Parameter]
    public StepItem ActiveStep { get; set; }

    [Parameter]
    public bool DisableEnhancedNavigation { get; set; }

    [Parameter]
    public EventCallback<StepItem> ActiveStepChanged { get; set; }

    [Parameter]
    public List<StepItem> Steps { get; set; } = new();

    public async Task EnableAllSteps()
    {
        PublisherHandler.EnableAllSteps();
        await ActiveStepChanged.InvokeAsync(ActiveStep);
    }

    public async Task Next()
    {
        if (ActiveStep != null)
        {
            if (ActiveStep.Index < Steps.Count - 1)
                await ChangeStep(Steps[ActiveStep.Index + 1]);
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

    public async Task ChangeStep(StepItem step)
    {
        step.Active = true;
        step.Enabled = !DisableEnhancedNavigation;
        ActiveStep = step;

        PublisherHandler.OnChanged(step);
        await ActiveStepChanged.InvokeAsync(ActiveStep);
        if (ActiveStep.Slug != null)
            await jsModule.InvokeVoidAsync("scrollToActiveStep", ActiveStep.Slug);
    }

    protected override async void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            jsModule = await JsRuntime.InvokeAsync<IJSObjectReference>("import", $"./Components/Wizard/WizardComponent.razor.js");
            ActiveStep = Steps.FirstOrDefault(x => x.Active) ?? Steps.FirstOrDefault();

            await jsModule.InvokeVoidAsync("setStepIndicatorWidth");

            if (ActiveStep != null)
                await ChangeStep(ActiveStep);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (jsModule is not null)
            await jsModule.DisposeAsync();
    }
}