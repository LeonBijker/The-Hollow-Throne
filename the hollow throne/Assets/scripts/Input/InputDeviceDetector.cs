using System;
using UnityEngine;

public enum InputDeviceType { Unknown, KeyboardMouse, Controller }

/// <summary>
/// Detects whether the player is using keyboard/mouse or a controller by observing recent input.
/// Subscribe to OnInputDeviceChanged or read CurrentDevice.
/// Place on a GameObject in your scene (e.g. GameManager) and set DontDestroyOnLoad if needed.
/// </summary>
public class InputDeviceDetector : MonoBehaviour
{
    public static InputDeviceDetector Instance { get; private set; }

    public InputDeviceType CurrentDevice { get; private set; } = InputDeviceType.KeyboardMouse;
    public event Action<InputDeviceType> OnInputDeviceChanged;

    [Header("Debug")]
    [SerializeField] private bool debugLogs = true;
    [Tooltip("Minimum seconds between automatic device switches to avoid noise")]
    [SerializeField] private float switchCooldown = 0.5f;
    private float lastSwitchTime = -10f;

    [Tooltip("Minimum axis magnitude to consider a controller axis active")]
    [SerializeField] private float axisThreshold = 0.5f;

    // common keyboard keys to monitor (small set for performance)
    private readonly KeyCode[] keyboardKeys = new KeyCode[]
    {
        KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D,
        KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.Space, KeyCode.Return, KeyCode.Escape,
        KeyCode.LeftShift, KeyCode.RightShift, KeyCode.LeftControl, KeyCode.RightControl
    };

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Update()
    {
        // check controller buttons
        for (int i = 0; i <= 19; i++)
        {
            if (Input.GetKeyDown((KeyCode)((int)KeyCode.JoystickButton0 + i)))
            {
                if (debugLogs) Debug.Log($"InputDeviceDetector: joystick button {i} pressed");
                SetDevice(InputDeviceType.Controller);
                return;
            }
        }

        // check controller axes (Horizontal/Vertical commonly mapped)
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(h) >= axisThreshold || Mathf.Abs(v) >= axisThreshold)
        {
            if (debugLogs) Debug.Log($"InputDeviceDetector: axis detected h={h:F2} v={v:F2}");
            // if no keyboard key pressed this frame, assume controller
            if (!AnyKeyboardOrMousePressed())
            {
                SetDevice(InputDeviceType.Controller);
                return;
            }
        }

        // check keyboard keys or mouse
        if (AnyKeyboardOrMousePressed())
        {
            SetDevice(InputDeviceType.KeyboardMouse);
            return;
        }
    }

    private bool AnyKeyboardOrMousePressed()
    {
        // catch any key or mouse activity (covers held keys as well)
        if (Input.anyKey)
        {
            if (debugLogs) Debug.Log("InputDeviceDetector: anyKey detected");
            return true;
        }

        foreach (KeyCode k in keyboardKeys)
        {
            if (Input.GetKey(k) || Input.GetKeyDown(k))
            {
                if (debugLogs) Debug.Log($"InputDeviceDetector: key detected {k}");
                return true;
            }
        }

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) return true;

        // detect mouse movement as keyboard/mouse input
        float mx = Input.GetAxisRaw("Mouse X");
        float my = Input.GetAxisRaw("Mouse Y");
        if (Mathf.Abs(mx) > 0.001f || Mathf.Abs(my) > 0.001f)
        {
            if (debugLogs) Debug.Log($"InputDeviceDetector: mouse move detected mx={mx:F2} my={my:F2}");
            return true;
        }

        return false;
    }

    private void SetDevice(InputDeviceType device)
    {
        if (CurrentDevice == device) return;
        // enforce cooldown to avoid flip-flopping from noise
        // allow immediate switch back to keyboard for responsiveness
        if (device != InputDeviceType.KeyboardMouse && Time.time - lastSwitchTime < switchCooldown)
        {
            if (debugLogs) Debug.Log($"InputDeviceDetector: switch to {device} ignored due to cooldown");
            return;
        }

        InputDeviceType prev = CurrentDevice;
        CurrentDevice = device;
        lastSwitchTime = Time.time;
        if (debugLogs) Debug.Log($"InputDeviceDetector: device changed {prev} -> {device}");
        OnInputDeviceChanged?.Invoke(device);
    }
}
