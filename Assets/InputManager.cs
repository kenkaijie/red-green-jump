using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;


public class KeyPressedEvent: UnityEvent<KeyCode>
{

}

public class InputManager: MonoBehaviour
{
    public KeyPressedEvent OnKeyPressed = new KeyPressedEvent();

    public float GetVerticalAxis()
    {
        return Input.GetAxis("Vertical");
    }

    private void InvokeKeyPress(KeyCode key)
    {
        Debug.Log("Pressed Key: " + key);
        OnKeyPressed.Invoke(key);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InvokeKeyPress(KeyCode.Escape);
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            InvokeKeyPress(KeyCode.T);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            InvokeKeyPress(KeyCode.UpArrow);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            InvokeKeyPress(KeyCode.DownArrow);
        }
    }

}

