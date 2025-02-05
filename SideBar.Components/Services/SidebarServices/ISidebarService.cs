namespace SideBar.Components.Services.SidebarServices;

public interface ISidebarService
{
    bool IsToggled { get; }
    event Action OnChange;
    void Toggle();
    void SetState(bool isToggled);
}