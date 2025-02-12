namespace SideBar.Components.Services.ModalServices;

public class ModalService : IModalService
{
    public bool IsModalVisible { get; private set; }
    public string ModalTitle { get; private set; } = string.Empty;
    public RenderFragment ModalContent { get; private set; }

    public event Action OnChange = delegate { };

    public void ShowModal(string title, RenderFragment content)
    {
        IsModalVisible = true;
        ModalTitle = title;
        ModalContent = content;
        NotifyStateChanged();
    }

    public void HideModal()
    {
        IsModalVisible = false;
        ModalTitle = string.Empty;
        ModalContent = null;
        NotifyStateChanged();
    }

    public void ToggleModal(string title, RenderFragment content)
    {
        if (IsModalVisible)
        {
            HideModal();
        }
        else
        {
            ShowModal(title, content);
        }
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}