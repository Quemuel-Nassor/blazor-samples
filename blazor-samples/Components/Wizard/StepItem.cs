namespace blazor_samples.Components.Wizard;

public class StepItem
{
    public string Name { get; set; }
    public string Slug => Name.Replace(" ", "-");
    public string Icon { get; set; }
    public bool Active { get; set; }
    public bool Enabled { get; set; } = true;
    public bool Completed { get; set; }
    
    public string GetClass()
    {
        return $" {(Active ? "active" : "")} {(Completed ? "completed" : "")} ";
    }
}