using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Exposes a public SaveNow() method intended for Unity UI Buttons or
/// other systems to trigger a save on demand.
/// Finds the GameObject named "Player" and saves position, scene and state.
/// </summary>
public class SaveButton : MonoBehaviour
{
    [Tooltip("Optional: log a message when saved")]
    [SerializeField] private bool debugLog = true;

    /// <summary>
    /// Call this from a UI Button OnClick() to save the current player state.
    /// </summary>
    public void SaveNow()
    {
        var player = GameObject.Find("Player");
        if (player == null)
        {
            if (debugLog) Debug.LogWarning("SaveButton: Player GameObject not found.");
            return;
        }

        SaveFrom(player);
    }

    /// <summary>
    /// Save using the provided player GameObject. Useful for programmatic calls.
    /// </summary>
    public void SaveFrom(GameObject player)
    {
        if (player == null)
        {
            if (debugLog) Debug.LogWarning("SaveButton: player is null.");
            return;
        }

        var playerPos = player.transform.position;
        var data = new SaveData
        {
            sceneBuildIndex = SceneManager.GetActiveScene().buildIndex,
            playerX = playerPos.x,
            playerY = playerPos.y
        };

        // preserve existing completed goals if present
        var existing = SaveSystem.Load();
        if (existing != null && existing.completedGoals != null)
            data.completedGoals = existing.completedGoals;

        // detect whether player currently has DoubleJump component and save that state
        data.doubleJumpUnlocked = player.GetComponent<DoubleJump>() != null;

        SaveSystem.Save(data);

        if (debugLog) Debug.Log($"SaveButton: saved game at scene {data.sceneBuildIndex} pos=({data.playerX:F2},{data.playerY:F2})");
    }
}
