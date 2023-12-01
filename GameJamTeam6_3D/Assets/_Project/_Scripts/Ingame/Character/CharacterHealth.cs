using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth 
{
    int curHealth;
    int maxHealth;
    public Action<int> onCurHealthChange;
    public Action onDead;



    public void Setup(int _maxHealth, int _startHealth = -1)
    {
        maxHealth = _maxHealth;
        if (_startHealth == -1) curHealth = maxHealth;
        else curHealth = _startHealth;
    }

    public void AddHealth(int _input)
    {
        curHealth += _input;
        if (curHealth > maxHealth) curHealth = maxHealth;
        onCurHealthChange?.Invoke(curHealth);
        if (IsDead()) onDead?.Invoke();
    }
    
    public float GetHealthAmountNormalized()
    {
        return (float)curHealth / maxHealth;
    }

    public void AddSignalHealthChange(Action<int> _call)
    {
        onCurHealthChange = _call;
    }

    public void AddSignalOnDead(Action _call)
    {
        onDead = _call;
    }

    bool IsDead()
    {
        return curHealth <= 0;
    }
}
