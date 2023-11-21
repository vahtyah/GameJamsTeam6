using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static InputHandler instance;

    private void Awake()
    {
        instance = this;

    }

    public float HorizontalMovement()
    {
        float value = 0f;
        if (PressLeft()) value = -1;
        else if (PressRight()) value = 1;
        else value = 0;
        return value;
    }

    public float VerticalMovement()
    {
        float value = 0f;
        if (PressFallBack()) value = -1;
        else if (PressForward()) value = 1;
        else value = 0;
        return value;
    }

    bool PressLeft()
    {
        return Input.GetKeyDown(KeyCode.A);
    }
    bool PressRight()
    {
        return Input.GetKeyDown(KeyCode.D);
    }
    bool PressForward()
    {
        return Input.GetKeyDown(KeyCode.W);
    }

    bool PressFallBack()
    {
        return Input.GetKey(KeyCode.S);
    }





}
