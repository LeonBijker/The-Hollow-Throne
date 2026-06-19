using UnityEngine;

/// <summary>
/// Place on a trigger collider to mark a goal complete when the Player touches it.
/// Matches player by GameObject.name == "Player" (no tags).
/// </summary>
public class GoalTrigger : MonoBehaviour
{
    [Tooltip("Optional goal identifier")]
    [SerializeField] private string goalId = "";

    [Tooltip("Optional descriptive text logged when the goal completes")]
    [SerializeField] private string description = "";

    [Tooltip("If true, this trigger will only fire once and then disable itself")]
    [SerializeField] private bool oneTime = true;

    [Tooltip("If true, call GameManager.EndGame() after completing the goal")]
    [SerializeField] private bool endGameOnComplete = false;

    [Tooltip("If >=0, load this scene index after goal completes")]
    [SerializeField] private int nextSceneIndex = -1;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        TryTrigger(other.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryTrigger(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        TryTrigger(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        TryTrigger(collision.gameObject);
    }

    private void TryTrigger(GameObject other)
    {
        if (triggered && oneTime) return;
        if (other == null) return;
        if (other.name != "Player") return;

        // If this goal has an id and it's already completed (persisted), do not trigger again
        if (!string.IsNullOrEmpty(goalId) && GoalManager.Instance != null)
        {
            if (GoalManager.Instance.IsGoalCompleted(goalId))
            {
                // ensure we mark as triggered locally to prevent further checks
                triggered = true;
                if (oneTime) enabled = false;
                return;
            }
        }

        if (GoalManager.Instance != null)
        {
            GoalManager.Instance.CompleteGoal(goalId, description, endGameOnComplete, nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("GoalTrigger: No GoalManager found in scene. Goal will not be fully handled.");
        }

        triggered = true;
        if (oneTime) enabled = false;
    }
}
