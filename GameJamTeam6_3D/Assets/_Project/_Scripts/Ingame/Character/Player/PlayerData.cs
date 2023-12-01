using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public CharacterAttributesValue speed;
    public CharacterAttributesValue def;
    public CharacterAttributesValue maxHp;
    public CharacterAttributesValue critChance;
    public CharacterAttributesValue critDamgPercent;

    private void Awake()
    {
        speed.Setup();
        def.Setup();
        maxHp.Setup();
        critChance.Setup();
        critDamgPercent.Setup();
    }

}

[Serializable]
public class CharacterAttributesValue
{
    public int value;

    int defaultValue;

    public void Setup()
    {
        defaultValue = value;
    }

    public void AddValue(int _value)
    {
        value += _value;
    }

    public void SetValue(int _value)
    {
        value = _value;
    }

}

