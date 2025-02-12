namespace SideBar.Components.Services.ModalServices;

public interface IModalService
{
    bool IsModalVisible { get; }
    string ModalTitle { get; }
    RenderFragment ModalContent { get; }
    event Action OnChange;

    void ShowModal(string title, RenderFragment content);
    void HideModal();
    void ToggleModal(string title, RenderFragment content);
}