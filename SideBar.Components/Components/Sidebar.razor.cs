namespace SideBar.Components.Components;

public partial class Sidebar : ComponentBase
{
    [Parameter] public string BrandTitle { get; set; } = "My App";

    [Parameter] public List<MenuItem> MenuItems { get; set; } = [];

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

    private void CloseModal() { ModalService.HideModal(); }
}