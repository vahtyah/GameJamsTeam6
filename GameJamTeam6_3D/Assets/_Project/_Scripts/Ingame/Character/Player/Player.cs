using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IGameSignal
{
    public static Player instance;

    [SerializeField] Rigidbody rb;
    public Rigidbody GetRb() => rb;
    [SerializeField] IWeapon weapon;
    [SerializeField] PlayerMovement movement;

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
        if (weapon.CanAttack()) weapon.DoAttack();
        movement.Iterate();
        
    }

    public void Prepare()
    {
        movement.Setup(10);
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
