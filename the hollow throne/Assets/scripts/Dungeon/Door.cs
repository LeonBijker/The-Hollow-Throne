using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private RoomEnemyTracker roomEnemyTracker;

    private bool canProceed;

    private void Awake()
    {
        roomEnemyTracker = GetComponentInParent<RoomEnemyTracker>();
    }

    private void OnEnable()
    {
        if (roomEnemyTracker != null)
        {
            roomEnemyTracker.OnRoomCleared += Proceed;
        }
    }

    private void OnDisable()
    {
        if (roomEnemyTracker != null)
        {
            roomEnemyTracker.OnRoomCleared -= Proceed;
        }
    }

    private void Proceed()
    {
        canProceed = true;

        Debug.Log("Room cleared. Door can now be used.");
    }

    public void Interact(GameObject player)
    {
        if (!canProceed)
        {
            Debug.Log("The room is not cleared yet.");
            return;
        }

        Debug.Log("Player is proceeding to the next room.");

        // TODO: Move player to the next room
    }
}