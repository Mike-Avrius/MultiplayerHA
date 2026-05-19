using UnityEngine;

public class EnterRoomButton : MonoBehaviour
{
    
    public void JoinExistenceRoom( SessionManager manager, string roomName)
    {
        manager.StartElseSession(roomName);
    }
}
