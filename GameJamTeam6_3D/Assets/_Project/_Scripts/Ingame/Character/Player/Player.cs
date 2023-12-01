using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SerializedMonoBehaviour, IGameSignal
{
    public static Player instance;

    public Action<int> onHealthChange;

    [SerializeField] Rigidbody rb;
    [SerializeField] PlayerData playerData;
    public PlayerData GetPlayerData() { return playerData; }
    public Rigidbody GetRb() => rb;
    [SerializeField] IWeapon weapon;
    public IWeapon GetWeapon() => weapon;
    [SerializeField] PlayerMovement movement;
    [SerializeField] PlayerAnimControl anim;
    public PlayerAnimControl GetAnimControl() => anim;
    [SerializeField] Transform model;
    public Transform GetModel() => model;
    [SerializeField] Transform modelRightHand;
    public Transform GetModelRightHand() => modelRightHand;

    int hp = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        anim.PlayAnim(PlayerAnimState.NormalMovement);
    }

    void Update()
    {
        movement.Iterate();
            if (weapon.CanAttack()) weapon.Shoot();
    }

    public void AddHealth(int _input)
    {
        _input -= playerData.def.value;
        hp += _input;
        if (hp > playerData.maxHp.value)
        {
            hp = playerData.maxHp.value;
        }
        onHealthChange?.Invoke(hp);
    }
    
    public float GetHealthAmountNormalized()
    {
        return (float)hp / (float)playerData.maxHp.value;
    }

    public void SetNewWeapon(IWeapon _weapon)
    {
        weapon = _weapon;
    }

    public void Prepare()
    {
        movement.Setup(); ;
        weapon.Setup();
    }

    public void StartGame()
    {
        
    }

    public void Pause()
    {
        
    }

    public void Resume()
    {
        
    }

    public void Win()
    {
        
    }

    public void Lose()
    {
       
    }
}
