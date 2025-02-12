#region

using Microsoft.AspNetCore.Components.Rendering;

#endregion

namespace SideBar.Components.Components;

public partial class Sidebar : ComponentBase
{
    // Sidebar brand title.
    [Parameter] public string BrandTitle { get; set; } = "My App";

    // You can pass in your own list of menu items; if empty, the service's list is used.
    [Parameter] public List<MenuItem> MenuItems { get; set; } = new();

    public void Dispose()
    {
        SidebarService.OnChange -= StateHasChanged;
        ModalService.OnChange -= StateHasChanged;
    }

    protected override void OnInitialized()
    {
        SidebarService.OnChange += StateHasChanged;
        ModalService.OnChange += StateHasChanged;
    }

    private void ToggleSidebar() { SidebarService.Toggle(); }

    /// <summary>
    ///     Opens the modal to display submenu items for the clicked menu item.
    /// </summary>
    private void OpenModal(MenuItem item)
    {
        if (!item.SubMenu.Any())
        {
            return;
        }

        ModalService.ShowModal(item.Title, ModalContent);
        return;

        void ModalContent(RenderTreeBuilder builder)
        {
            var seq = 0;
            builder.OpenComponent<SubMenuComponent>(seq++);
            builder.AddAttribute(seq, "Items", item.SubMenu);
            builder.CloseComponent();
        }
    }

    /// <summary>
    ///     Closes the modal.
    /// </summary>
    private void CloseModal() { ModalService.HideModal(); }
}