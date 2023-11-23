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
    public Rigidbody GetRb() => rb;
    [SerializeField] IWeapon weapon;
    [SerializeField] PlayerMovement movement;
    [SerializeField] PlayerAnimControl anim;
    [SerializeField] Transform model;
    public Transform GetModel() => model;
    public PlayerAnimControl GetAnimControl() => anim;

    int hp = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.Iterate();
        if (InputHandler.instance.IsNormalAttackHoldDown())
            if (weapon.CanAttack()) weapon.Shoot();
        
        
        anim.PlayAnim(PlayerAnimState.NormalMovement);
    }

    public void AddHealth(int _input)
    {
        hp += _input;
        onHealthChange?.Invoke(hp);
    }

    public void Prepare()
    {
        movement.Setup(200);
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
