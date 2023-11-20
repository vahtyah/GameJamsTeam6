using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorDebug 
{
    public static void DebugRed(object _content)
    {
        Debug.Log($"<color=red>{_content}</color>");
    }
    public static void DebugGreen(object _content)
    {
        Debug.Log($"<color=green>{_content}</color>");
    }
    public static void DebugYellow(object _content)
    {
        Debug.Log($"<color=yellow>{_content}</color>");
    }
    public static void DebugOrange(object _content)
    {
        Debug.Log($"<color=orange>{_content}</color>");
    }
}
