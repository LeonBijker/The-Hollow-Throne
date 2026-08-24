using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    private RoomEnemyTracker roomEnemyTracker;

    [SerializeField] private bool canProceed;

    private void Awake()
    {
        roomEnemyTracker = GetComponentInParent<RoomEnemyTracker>();
    }

    private void OnEnable()
    {
        if (roomEnemyTracker != null)
        {
            roomEnemyTracker.OnRoomCleared += Proceed;
            Debug.Log("Door is waiting for the room to be cleared.");
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
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interact(collision.gameObject);
    }
}