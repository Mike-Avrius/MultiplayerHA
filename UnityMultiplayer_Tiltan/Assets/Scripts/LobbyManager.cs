using System;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using TMPro;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour, INetworkRunnerCallbacks
{
    
    [SerializeField] NetworkRunner networkRunner;
    [SerializeField] private SessionManager _sessionManager;
    [SerializeField] private UI_ButtonManager _uiButtonManager;
    
    [Header("Lobby")]
    [SerializeField] private GameObject lobbyPanel;
    [SerializeField] private TMP_InputField lobbyNameInput;
    [SerializeField] private TextMeshProUGUI lobbyName;
    
    [Header("Session")]
    [SerializeField] private GameObject sessionPanel;
    [SerializeField] private Transform contentParent;
    [SerializeField] private EnterRoomButton roomButtonPrefab;
    private List<EnterRoomButton> sessionButtonList = new List<EnterRoomButton>();
    
    [Header("Room")]
    [SerializeField] private GameObject roomPanel;
    [SerializeField] private TextMeshProUGUI numberOfPlayers;
   
    
    private const string PRO_LOBBY_NAME = "ProLobby";
    private const string NOOB_LOBBY_NAME = "NoobLobby";
    

    private void Awake()
    {
        networkRunner.AddCallbacks(this);
    }
    
    public async void JoinLobby()
    {
        if (lobbyNameInput.text == "") return;
        
        _uiButtonManager.ButtonInteractionChanger(UiState.LobbySelection);
        
        StartGameResult result = await networkRunner.JoinSessionLobby(SessionLobby.Custom, lobbyNameInput.text);

        if (result.Ok)
        {
            Debug.Log($"Player joined {lobbyNameInput.text}");
            lobbyPanel.SetActive(false);
            sessionPanel.SetActive(true);
            lobbyName.text = networkRunner.LobbyInfo.Name;
        }
    }
    public async void JoinProLobby()
    {
        _uiButtonManager.ButtonInteractionChanger(UiState.LobbySelection);
        
        StartGameResult result = await networkRunner.JoinSessionLobby(SessionLobby.Custom, PRO_LOBBY_NAME);

        if (result.Ok)
        {
            Debug.Log($"Player joined {PRO_LOBBY_NAME}");
            lobbyPanel.SetActive(false);
            sessionPanel.SetActive(true);
            lobbyName.text = networkRunner.LobbyInfo.Name;
        }
    }
    public async void JoinNoobLobby()
    {
        _uiButtonManager.ButtonInteractionChanger(UiState.LobbySelection);
        
        StartGameResult result = await networkRunner.JoinSessionLobby(SessionLobby.Custom, NOOB_LOBBY_NAME);

        if (result.Ok)
        {
            Debug.Log($"Player joined {NOOB_LOBBY_NAME}");
            lobbyPanel.SetActive(false);
            sessionPanel.SetActive(true);
            lobbyName.text = networkRunner.LobbyInfo.Name;
        }
    }
    

    public async void LeaveLobby()
    {
        _uiButtonManager.ButtonInteractionChanger(UiState.InRoom);
        if (networkRunner.IsRunning)
        {
            await networkRunner.Shutdown();
        }
        
        sessionPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        
        foreach (var b in sessionButtonList)
        {
            Destroy(b.gameObject); // комната удаляется если в ней 0 игроков
        }
        
        sessionButtonList.Clear();
        
        foreach (var session in sessionList)
        {
            EnterRoomButton buttonObj = Instantiate(roomButtonPrefab, contentParent);
            sessionButtonList.Add(buttonObj);

            string sessionName = session.Name;

            buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = sessionName;
            buttonObj.GetComponent<Button>().onClick.AddListener((() => buttonObj.JoinExistenceRoom(_sessionManager, sessionName)));
        }
    }
    
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        bool isLocalPLayer = networkRunner.LocalPlayer == player;
        
        Debug.Log($"Player {player.PlayerId} joined, local player: {isLocalPLayer}");
        
        RefreshUI_Room();
    }
    
    
    void RefreshUI_Room()
    {
#if LOBBY_MANAGER_UI
        if(networkRunner.IsRunning && !networkRunner.IsShutdown)
        {
            sessionPanel.SetActive(false);
            roomPanel.SetActive(true);
            string playerNum = networkRunner.SessionInfo?.PlayerCount.ToString();
            numberOfPlayers.text = $"Available players: {playerNum} / {_sessionManager.MaxPlCount}";
        }
#endif   
    }
    

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
     //   throw new NotImplementedException();
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
      //  throw new NotImplementedException();
    }

    

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        RefreshUI_Room();
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
       // throw new NotImplementedException();
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
       // throw new NotImplementedException();
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
//throw new NotImplementedException();
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
       // throw new NotImplementedException();
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
      //  throw new NotImplementedException();
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
       // throw new NotImplementedException();
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
      //  throw new NotImplementedException();
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
//        throw new NotImplementedException();
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
       // throw new NotImplementedException();
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        //throw new NotImplementedException();
    }
    
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
       // throw new NotImplementedException();
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
       // throw new NotImplementedException();
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
       // throw new NotImplementedException();
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
       // throw new NotImplementedException();
    }
}
