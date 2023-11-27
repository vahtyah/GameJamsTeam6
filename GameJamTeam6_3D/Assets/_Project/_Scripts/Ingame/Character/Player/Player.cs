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
        
    }

    void Update()
    {
        movement.Iterate();
            if (weapon.CanAttack()) weapon.Shoot();
        
        
        anim.PlayAnim(PlayerAnimState.NormalMovement);
    }

    public void AddHealth(int _input)
    {
        hp += _input;
        onHealthChange?.Invoke(hp);
    }

    public void SetNewWeapon(IWeapon _weapon)
    {
        weapon = _weapon;
    }

    public void Prepare()
    {
        movement.Setup(250);
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
