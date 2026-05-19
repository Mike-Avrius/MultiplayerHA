using Fusion;
using TMPro;
using UnityEngine;


public class SessionManager : MonoBehaviour
{
    [SerializeField] private NetworkRunner networkRunner;
    [SerializeField] private TMP_InputField roomNameInput;
    [SerializeField] private TextMeshProUGUI sessionNameField;

    [SerializeField] private UI_ButtonManager _uiButtonManager;

   
    
    private int maxPlayerCount = 2;
    public int MaxPlCount => maxPlayerCount;
    
    public void StartSession()
    {
        if (roomNameInput.text == "") return;

        _uiButtonManager.ButtonInteractionChanger(UiState.InLobby);
        
        networkRunner.StartGame( new StartGameArgs()
        {
            GameMode  = GameMode.Shared,
            SessionName = roomNameInput.text,
            OnGameStarted = OnGameStarted,
            CustomLobbyName = networkRunner.LobbyInfo.Name,
            PlayerCount = maxPlayerCount
        });
    }

    public void SetMaxRoomPLayer(float value)
    {
        maxPlayerCount = (int)value;
    }
    
    public void StartElseSession(string sessionName)
    {
        networkRunner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = sessionName,
            OnGameStarted = OnGameStarted,
            CustomLobbyName =  networkRunner.LobbyInfo.Name
        });
    }

    public void LeaveRoom()
    {
        networkRunner.Shutdown();
    }
    

    private void OnGameStarted(NetworkRunner networkRunner)
    {
        string sn = networkRunner.SessionInfo.Name;
        sessionNameField.text = sn;
    }
    
}
