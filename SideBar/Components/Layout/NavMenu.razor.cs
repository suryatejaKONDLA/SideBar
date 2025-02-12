namespace SideBar.Components.Layout;

public partial class NavMenu
{
    private List<MenuItem> MenuItems =
    [
        new() { Title = "Home", Icon = "fa-home", Link = "#" },
        new()
        {
            Title = "Plugins",
            Icon = "fa-plug",
            Link = "#",
            SubMenu =
            [
                new MenuItem { Title = "Plugin Manager", Link = "#" },
                new MenuItem { Title = "Add New", Link = "#" }
            ]
        },

        new()
        {
            Title = "Users",
            Icon = "fa-user",
            Link = "#",
            SubMenu =
            [
                new MenuItem { Title = "All Users", Link = "#" },
                new MenuItem { Title = "Add New", Link = "#" }
            ]
        }
    ];
}