using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth 
{
    [SerializeField] int curHealth;
    [SerializeField] int maxHealth;
    public int CurHealth => curHealth;
    public int MaxHealth => maxHealth;
    public Action<int> onCurHealthChange;
    public Action onDead;



    public void Setup(int _maxHealth, float _startHealthPercent = 1)
    {
        maxHealth = _maxHealth;
        curHealth = (int)((float)maxHealth * _startHealthPercent);
        onCurHealthChange?.Invoke(curHealth);
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
