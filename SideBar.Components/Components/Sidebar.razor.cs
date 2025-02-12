namespace SideBar.Components.Components;

public partial class Sidebar : ComponentBase
{
    [Parameter] public string BrandTitle { get; set; } = "My App";

    [Parameter] public List<MenuItem> MenuItems { get; set; } = [];

    private bool ShowModal = false;
    private string ModalTitle = "";
    private RenderFragment ModalContent = null;

    protected override void OnInitialized()
    {
        SidebarService.OnChange += StateHasChanged;
    }

    public void Dispose()
    {
        SidebarService.OnChange -= StateHasChanged;
    }

    private void ToggleSidebar()
    {
        SidebarService.Toggle();
    }

    private void OpenModal(MenuItem item)
    {
        if (!item.SubMenu.Any())
        {
            return;
        }

        ShowModal = true;
        ModalTitle = item.Title;
        ModalContent = builder =>
        {
            var seq = 0;
            builder.OpenComponent<SubMenuComponent>(seq++);
            builder.AddAttribute(seq, "Items", item.SubMenu);
            builder.CloseComponent();
        };
    }

    private void CloseModal()
    {
        ShowModal = false;
        ModalTitle = "";
        ModalContent = null;
    }
}