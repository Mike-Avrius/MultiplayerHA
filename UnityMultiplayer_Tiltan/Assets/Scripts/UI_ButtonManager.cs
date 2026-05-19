using UnityEngine;
using UnityEngine.UI;

public enum UiState
{
    LobbySelection,
    InLobby,
    InRoom
}

public class UI_ButtonManager : MonoBehaviour
{
    
    [Header("Lobby")]
    [SerializeField] private Button proLobbyButton;
    [SerializeField] private Button noobLobbyButton;
    [SerializeField] private Button createLobbyButton;
    
    [SerializeField] private Button closeLobbyPanelButton;
    
    [Header("Session")]
    [SerializeField] private Button startOwnSessionButton;
    [SerializeField] private Button leaveLobbyButton;
    
    [Header("Room")]
    [SerializeField] private Button leaveRoomButton;

    
    
    public void ButtonInteractionChanger(UiState state)
    {
        proLobbyButton.interactable = state != UiState.LobbySelection;
        noobLobbyButton.interactable = state != UiState.LobbySelection;
        createLobbyButton.interactable = state != UiState.LobbySelection;
        closeLobbyPanelButton.interactable = state != UiState.LobbySelection;

        startOwnSessionButton.interactable = state != UiState.InLobby;
        leaveLobbyButton.interactable = state != UiState.InLobby;

        leaveRoomButton.interactable = state != UiState.InRoom;
    }
}
