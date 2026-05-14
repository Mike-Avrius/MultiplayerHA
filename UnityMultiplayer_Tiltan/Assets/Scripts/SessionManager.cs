using Fusion;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    [SerializeField] private NetworkRunner networkRunner;


    public void StartSession()
    {
        networkRunner.StartGame( new StartGameArgs()
        {
            GameMode  = GameMode.Shared,
            SessionName = "AlwaysPlayOnline",
            OnGameStarted = OnGameStarted
        });
    }

    private void OnGameStarted(NetworkRunner networkRunner)
    {
        Debug.Log("You are online");
    }
    
}
