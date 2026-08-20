using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// Simple UI that listens to GoalManager and shows a temporary message when a goal is completed.
/// Assign a panel GameObject (it will be enabled/disabled) and a TMP_Text for the message.
/// </summary>
public class GoalUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private float displaySeconds = 3f;

    private Coroutine hideCoroutine;

    private void Awake()
    {
        if (panel != null) panel.SetActive(false);
    }

    private void OnEnable()
    {
        if (GoalManager.Instance != null)
            GoalManager.Instance.OnGoalCompleted += HandleGoalCompleted;
    }

    private void OnDisable()
    {
        if (GoalManager.Instance != null)
            GoalManager.Instance.OnGoalCompleted -= HandleGoalCompleted;
    }

    private void HandleGoalCompleted(string id)
    {
        string msg = string.IsNullOrEmpty(id) ? "Goal completed" : $"Goal completed: {id}";
        ShowMessage(msg);
    }

    public void ShowMessage(string text)
    {
        if (panel == null || messageText == null) return;
        messageText.text = text;
        panel.SetActive(true);

        if (hideCoroutine != null) StopCoroutine(hideCoroutine);
        hideCoroutine = StartCoroutine(HideAfter());
    }

    private IEnumerator HideAfter()
    {
        yield return new WaitForSeconds(displaySeconds);
        if (panel != null) panel.SetActive(false);
        hideCoroutine = null;
    }
}
