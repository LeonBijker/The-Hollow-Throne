using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Applies pending SaveSystem.PendingLoad when the saved scene finishes loading.
/// Restores player position and abilities (e.g., double jump).
/// Attach to a persistent manager (e.g., the GameManager object) or any object in the scene.
/// </summary>
public class SaveApplier : MonoBehaviour
{
    private void Awake()
    {
        // ensure this listener persists across scenes if placed on a persistent object
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SaveData pending = SaveSystem.PendingLoad;
        if (pending == null) return;

        if (scene.buildIndex != pending.sceneBuildIndex) return;

        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            Vector3 pos = player.transform.position;
            player.transform.position = new Vector3(pending.playerX, pending.playerY, pos.z);
            Debug.Log($"SaveApplier: applied saved player position ({pending.playerX},{pending.playerY})");

            PlayerController pc = player.GetComponent<PlayerController>();
            if (pc != null)
            {
                if (pending.doubleJumpUnlocked)
                {
                    pc.SwapJumping<DoubleJump>();
                    Debug.Log("SaveApplier: restored DoubleJump ability");
                }
                else
                {
                    pc.SwapJumping<CharacterJump>();
                    Debug.Log("SaveApplier: ensured CharacterJump (no double jump)");
                }
            }
        }
        else
        {
            Debug.LogWarning("SaveApplier: Player object not found when applying save");
        }

        // restore completed goals into GoalManager without triggering events
        if (pending.completedGoals != null && pending.completedGoals.Length > 0 && GoalManager.Instance != null)
        {
            GoalManager.Instance.RestoreCompletedGoals(pending.completedGoals);
            Debug.Log($"SaveApplier: restored {pending.completedGoals.Length} completed goals");
        }

        // clear pending and ensure gameplay time/state via GameManager if present
        SaveSystem.PendingLoad = null;
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResumeGame();
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
