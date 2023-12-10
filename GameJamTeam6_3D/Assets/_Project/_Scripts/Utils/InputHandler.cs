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

    public bool IsNormalAttackHoldDown()
    {
        return Input.GetMouseButton(0);
        //return Input.GetMouseButtonDown(0);
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
        return Input.GetKey(KeyCode.A);
    }
    bool PressRight()
    {
        return Input.GetKey(KeyCode.D);
    }
    bool PressForward()
    {
        return Input.GetKey(KeyCode.W);
    }

    bool PressFallBack()
    {
        return Input.GetKey(KeyCode.S);
    }

    public bool PressInventory()
    {
        return Input.GetKeyDown(KeyCode.B);
    }
    
    public bool PressPause()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }
    
    public bool PressRightClick()
    {
        return Input.GetMouseButtonDown(1);
    }



}




