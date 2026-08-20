using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

// Simple UI helper to show player mechanics and goal
public class PlayerInfoUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text mechanicsText;
    [SerializeField] private TMP_Text goalText;

    [Header("Defaults")]
    [SerializeField] private string defaultTitle = "How to Play";
    [TextArea(3,6)] [SerializeField] private string defaultMechanics = "Move with AD. Jump with Space. Dash with W.";
    [TextArea(2,4)] [SerializeField] private string defaultGoal = "Reach the throne and defeat the final boss.";

    [SerializeField] private bool pauseWhenOpen = true;
    [Header("Startup")]
    [Tooltip("Show the player info automatically when the scene starts")]
    [SerializeField] private bool showOnStart = true;
    [Tooltip("Event invoked when the player closes the info and the game should begin")]


    private float previousTimeScale = 1f;

    void Awake()
    {
        if (panel != null) panel.SetActive(false);
    }

    void Start()
    {
        if (showOnStart)
            ShowDefault();
    }

    void Update()
    {
        // When the info panel is visible, only allow closing / starting the game with Space
        if (panel != null && panel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartGame();
            }
        }
    }

    public void ShowDefault()
    {
        ShowInfo(defaultMechanics, defaultGoal, defaultTitle);
    }

    public void ShowInfo(string mechanics, string goal, string title = null)
    {
        if (panel == null) return;

        titleText.text = string.IsNullOrEmpty(title) ? defaultTitle : title;
        mechanicsText.text = mechanics;
        goalText.text = goal;

        panel.SetActive(true);
        if (pauseWhenOpen)
        {
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0f;
        }
    }

    public void Hide()
    {
        if (panel == null) return;
        panel.SetActive(false);
        if (pauseWhenOpen) Time.timeScale = previousTimeScale;
    }

    // Call to close the info and signal that the game should start
    public void StartGame()
    {
        Hide();
        // if a GameManager exists, use it to start or resume the game depending on the current scene
        if (GameManager.Instance != null)
        {
            UnityEngine.SceneManagement.Scene active = SceneManager.GetActiveScene();
            // gameplay scene is build index 1 in this project; if we're already in gameplay, ResumeGame, otherwise StartGame
            if (active.buildIndex == 1)
                GameManager.Instance.ResumeGame();
            else
                GameManager.Instance.StartGame();
        }
    }

    public void Toggle()
    {
        if (panel == null) return;
        if (panel.activeSelf) Hide(); else ShowDefault();
    }
}
