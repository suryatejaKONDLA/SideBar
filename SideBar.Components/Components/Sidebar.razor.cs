namespace SideBar.Components.Components;

public partial class Sidebar : ComponentBase
{
    [Parameter] public string BrandTitle { get; set; } = "Logo";

    [Parameter] public List<MenuItem> MenuItems { get; set; } = [];

    private bool IsToggled = false;

    private void ToggleSidebar()
    {
        IsToggled = !IsToggled;
    }

    private void ToggleSubMenu(MenuItem item)
    {
        item.IsActive = !item.IsActive;

        foreach (var menuItem in MenuItems.Where(menuItem => menuItem != item))
        {
            menuItem.IsActive = false;
        }
    }
}