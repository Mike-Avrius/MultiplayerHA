using Fusion;
using TMPro;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    [SerializeField] private NetworkRunner networkRunner;
    [SerializeField] private TMP_InputField roomNameInput;

    public void StartSession()
    {
        if (roomNameInput.text == "") return;
        
        networkRunner.StartGame( new StartGameArgs()
        {
            GameMode  = GameMode.Shared,
            SessionName = roomNameInput.text,
            OnGameStarted = OnGameStarted,
            CustomLobbyName = networkRunner.LobbyInfo.Name
        });
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
    

    private void OnGameStarted(NetworkRunner networkRunner)
    {
        Debug.Log($"You joined room {networkRunner.SessionInfo.Name}");
    }
    
}
