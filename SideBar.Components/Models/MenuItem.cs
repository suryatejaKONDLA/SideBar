namespace SideBar.Components.Models;

public class MenuItem
{
    public string Title { get; init; }
    public string Icon { get; init; }
    public string Link { get; set; }
    public List<MenuItem> SubMenu { get; init; } = [];
    public bool IsActive { get; set; }
}