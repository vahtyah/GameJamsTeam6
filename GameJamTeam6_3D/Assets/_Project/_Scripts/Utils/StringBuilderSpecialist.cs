using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
public static class StringBuilderSpecialist
{
    static StringBuilder stringbuilder = new StringBuilder();

    public static void Clear() => stringbuilder.Clear();
    public static void Set(string _arg) {
        Clear();
        stringbuilder.Append(_arg);
    }
    public static void Set(int _arg){
        Clear();
        stringbuilder.Append(_arg);
    }
    public static string GetString() => stringbuilder.ToString();

    public static string SetAndGet(string _arg)
    {
        Clear();
        stringbuilder.Append(_arg);
        return GetString();
    }
}
