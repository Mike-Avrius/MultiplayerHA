using UnityEngine;
using UnityEngine.UI;
public class EnterRoomButton : MonoBehaviour
{
    [SerializeField] private Button connectSessionButton;
    public void JoinExistenceRoom( SessionManager manager, string roomName)
    {
        connectSessionButton.interactable = false;
        manager.StartElseSession(roomName);
    }
}
