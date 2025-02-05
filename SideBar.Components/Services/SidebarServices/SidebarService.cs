namespace SideBar.Components.Services.SidebarServices;

public class SidebarService : ISidebarService
{
    public bool IsToggled { get; private set; }

    public event Action OnChange = delegate { };

    public void Toggle()
    {
        IsToggled = !IsToggled;
        NotifyStateChanged();
    }

    public void SetState(bool isToggled)
    {
        IsToggled = isToggled;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}