namespace SideBar.Components.Components;

public partial class Sidebar : ComponentBase
{
    private MenuItem ActiveParent;
    [Parameter] public string BrandTitle { get; set; } = "My App";

    [Parameter] public List<MenuItem> MenuItems { get; set; } = [];

    private bool ShowModal = false;

    private string ModalTitle = "";

    private RenderFragment ModalContent = null;

    public void Dispose() { SidebarService.OnChange -= StateHasChanged; }

    protected override void OnInitialized() { SidebarService.OnChange += StateHasChanged; }

    private void ToggleSidebar() { SidebarService.Toggle(); }

    private void ToggleSubMenu(MenuItem item)
    {
        if (item.SubMenu.Any())
        {
            if (ActiveParent != null && !IsInHierarchy(ActiveParent, item))
            {
                CloseMenuBranch(ActiveParent);
            }

            item.IsActive = !item.IsActive;

            if (item.IsActive)
            {
                ActiveParent = FindTopLevelParent(item);
            }
            else if (ActiveParent == item)
            {
                ActiveParent = null;
            }
        }
        else
        {
            ShowModal = true;
            ModalTitle = item.Title;
            ModalContent = GetModalContent(item);
            CloseMenuBranch(ActiveParent); 
        }
    }

    private RenderFragment GetModalContent(MenuItem item)
    {
        return builder =>
        {
            builder.OpenComponent(0, typeof(Home)); // Example component
            builder.AddAttribute(1, "Title", item.Title);
            builder.CloseComponent();
        };
    }

    private void CloseModal()
    {
        ShowModal = false;
        ModalTitle = "";
        ModalContent = null;
    }

    private static bool IsInHierarchy(MenuItem parent, MenuItem target) { return parent == target || parent.SubMenu.Any(child => IsInHierarchy(child, target)); }

    private MenuItem FindTopLevelParent(MenuItem item)
    {
        var current = item;
        var topParent = item;

        while (true)
        {
            var parent = FindImmediateParent(current);
            if (parent == null)
            {
                break;
            }

            topParent = parent;
            current = parent;
        }

        return topParent;
    }

    private MenuItem FindImmediateParent(MenuItem item)
    {
        foreach (var menuItem in MenuItems)
        {
            if (menuItem.SubMenu.Contains(item))
            {
                return menuItem;
            }

            var parent = FindParentInSubmenu(menuItem, item);
            if (parent != null)
            {
                return parent;
            }
        }

        return null;
    }

    private static MenuItem FindParentInSubmenu(MenuItem parent, MenuItem target)
    {
        foreach (var child in parent.SubMenu)
        {
            if (child.SubMenu.Contains(target))
            {
                return child;
            }

            var result = FindParentInSubmenu(child, target);
            if (result != null)
            {
                return result;
            }
        }

        return null;
    }

    private static void CloseMenuBranch(MenuItem item)
    {
        if (item is null) { return; }
        item.IsActive = false;
        foreach (var child in item.SubMenu)
        {
            CloseMenuBranch(child);
        }
    }

    private RenderFragment RenderMenuItems(List<MenuItem> items) => builder =>
    {
        foreach (var item in items)
        {
            builder.OpenElement(0, "li");
            builder.AddAttribute(1, "class", $"nav-item {(item.IsActive ? "active" : "")} {(item.SubMenu.Any() ? "has-submenu" : "")}");

            // Menu item link
            builder.OpenElement(2, "a");
            builder.AddAttribute(3, "href", "javascript:void(0);"); // Prevent navigation
            builder.AddAttribute(4, "onclick", EventCallback.Factory.Create(this, () => ToggleSubMenu(item)));

            // Icon
            builder.OpenElement(5, "i");
            builder.AddAttribute(6, "class", $"fa {item.Icon}");
            builder.CloseElement();

            // Title
            builder.OpenElement(7, "span");
            builder.AddContent(8, item.Title);
            builder.CloseElement();

            // Arrow for submenus
            if (item.SubMenu.Any())
            {
                builder.OpenElement(9, "i");
                builder.AddAttribute(10, "class", "fa fa-chevron-right arrow");
                builder.CloseElement();
            }

            builder.CloseElement(); // Close <a>

            // Render submenu recursively
            if (item.SubMenu.Any())
            {
                builder.OpenElement(11, "ul");
                builder.AddAttribute(12, "class", "submenu");
                builder.AddAttribute(13, "style", $"display: {(item.IsActive ? "block" : "none")}");
                builder.AddContent(14, RenderMenuItems(item.SubMenu));
                builder.CloseElement(); // Close <ul>
            }

            builder.CloseElement(); // Close <li>
        }
    };
}