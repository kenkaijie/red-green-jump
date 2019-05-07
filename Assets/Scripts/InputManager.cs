using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;


public enum KeyAction
{
    Jump,
    ColorSwitch,
    Pause
}

public class KeyPressedEvent: UnityEvent<KeyAction>
{

}

public class InputManager: MonoBehaviour
{
    public KeyCode JumpButton;
    public KeyCode ColorSwitchButton;
    public KeyCode PauseButton;

    public KeyPressedEvent OnKeyPressed = new KeyPressedEvent();

    public float GetVerticalAxis()
    {
        return Input.GetAxis("Vertical");
    }

    private void InvokeKeyPress(KeyAction action)
    {
        Debug.Log("Pressed Action: " + action);
        OnKeyPressed.Invoke(action);
    }

    private void Update()
    {
        if (Input.GetKeyDown(JumpButton))
        {
            InvokeKeyPress(KeyAction.Jump);
        }
        else if (Input.GetKeyDown(ColorSwitchButton))
        {
            InvokeKeyPress(KeyAction.ColorSwitch);
        }
        else if (Input.GetKeyDown(PauseButton))
        {
            InvokeKeyPress(KeyAction.Pause);
        }
    }

}

