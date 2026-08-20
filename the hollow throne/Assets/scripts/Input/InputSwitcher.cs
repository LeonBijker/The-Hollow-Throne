using System.Linq;
using UnityEngine;

/// <summary>
/// Detects input device via InputDeviceDetector and swaps player input components accordingly.
/// It will add the requested input component if missing and remove the other.
/// Place on a manager object in the scene. Assign the PlayerController optionally.
/// </summary>
public class InputSwitcher : MonoBehaviour
{
    [Tooltip("Optional: set to an existing PlayerController. If null, will FindObjectOfType<PlayerController>()")]
    [SerializeField] private PlayerController playerController;

    [Tooltip("Log device switches to console")]
    [SerializeField] private bool debugLogs = true;

    private void Start()
    {
        if (playerController == null)
            playerController = FindFirstObjectByType<PlayerController>();
        if (InputDeviceDetector.Instance != null)
        {
            InputDeviceDetector.Instance.OnInputDeviceChanged += OnDeviceChanged;
            // apply initial device
            ApplyDevice(InputDeviceDetector.Instance.CurrentDevice);
        }
        else
        {
            // no detector present: ensure keyboard is the default
            ApplyDevice(InputDeviceType.KeyboardMouse);
        }
    }

    private void OnDestroy()
    {
        if (InputDeviceDetector.Instance != null)
            InputDeviceDetector.Instance.OnInputDeviceChanged -= OnDeviceChanged;
    }

    private void OnDeviceChanged(InputDeviceType device)
    {
        ApplyDevice(device);
    }

    private void ApplyDevice(InputDeviceType device)
    {
        if (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
            if (playerController == null) return;
        }

        GameObject go = playerController.gameObject;

        if (device == InputDeviceType.Controller)
        {
            // ensure ControllerInput exists
            ControllerInput ctrl = go.GetComponent<ControllerInput>();
            if (ctrl == null)
            {
                go.AddComponent<ControllerInput>();
                if (debugLogs) Debug.Log("InputSwitcher: added ControllerInput");
            }

            // remove KeyboardInput components
            KeyboardInput[] keys = go.GetComponents<KeyboardInput>();
            foreach (KeyboardInput k in keys)
            {
                Destroy(k);
                if (debugLogs) Debug.Log("InputSwitcher: removed KeyboardInput");
            }
        }
        else // KeyboardMouse or Unknown -> ensure keyboard input
        {
            KeyboardInput key = go.GetComponent<KeyboardInput>();
            if (key == null)
            {
                go.AddComponent<KeyboardInput>();
                if (debugLogs) Debug.Log("InputSwitcher: added KeyboardInput");
            }

            // remove ControllerInput components
            ControllerInput[] ctrls = go.GetComponents<ControllerInput>();
            foreach (ControllerInput c in ctrls)
            {
                Destroy(c);
                if (debugLogs) Debug.Log("InputSwitcher: removed ControllerInput");
            }
        }
    }
}
