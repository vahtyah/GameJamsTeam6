using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SerializedMonoBehaviour, IGameSignal
{
    public static Player instance;

    [SerializeField] Rigidbody rb;
    public Rigidbody GetRb() => rb;
    [SerializeField] IWeapon weapon;
    [SerializeField] PlayerMovement movement;
    [SerializeField] PlayerAnimControl anim;
    public PlayerAnimControl GetAnimControl() => anim;

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
        if (weapon.CanAttack()) weapon.DoAttack();
        
    }

    public void Prepare()
    {
        movement.Setup(10);
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