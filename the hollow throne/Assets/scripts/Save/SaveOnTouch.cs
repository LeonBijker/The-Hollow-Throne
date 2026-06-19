using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Trigger a save when the player GameObject touches this collider.
/// Uses the GameObject name "Player" (no tags).
/// Works with 2D triggers and 3D triggers/collisions. Configure collider as Trigger for expected behaviour.
/// </summary>
public class SaveOnTouch : MonoBehaviour
{
    [Tooltip("When true the save trigger only works once and then disables itself.")]
    [SerializeField] private bool saveOnce = true;

    [Tooltip("Optional: log a message when saved")]
    [SerializeField] private bool debugLog = true;

    [Tooltip("When true the object will be destroyed after saving once")]
    [SerializeField] private bool destroyAfterSave = false;

    private bool hasSaved = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        TrySaveFrom(other.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TrySaveFrom(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        TrySaveFrom(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        TrySaveFrom(collision.gameObject);
    }

    private void TrySaveFrom(GameObject other)
    {
        if (hasSaved && saveOnce) return;
        if (other == null) return;

        // match by name "Player"
        if (other.name != "Player") return;

        var playerPos = other.transform.position;
        var data = new SaveData
        {
            sceneBuildIndex = SceneManager.GetActiveScene().buildIndex,
            playerX = playerPos.x,
            playerY = playerPos.y
        };

        // detect whether player currently has DoubleJump component and save that state
        var hasDoubleJump = other.GetComponent<DoubleJump>() != null;
        data.doubleJumpUnlocked = hasDoubleJump;

        SaveSystem.Save(data);
        hasSaved = true;

        if (debugLog) Debug.Log($"SaveOnTouch: saved game at scene {data.sceneBuildIndex} pos=({data.playerX:F2},{data.playerY:F2})");

        if (destroyAfterSave)
            Destroy(gameObject);
        else if (saveOnce)
            enabled = false;
    }
}
