using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Player : SerializedMonoBehaviour, IGameSignal
{
    public static Player instance;

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
    CharacterHealth characterHealth = new CharacterHealth();
    public CharacterHealth GetCharacterHealth() => characterHealth;
    public GameObject inventoryCam;

    bool isLive = false;

    private void Awake()
    {
        instance = this;
        characterHealth.onDead = OnDie;
    }

    private void Start()
    {
       
    }

    void Update()
    {
        if (isLive == false) return;
        movement.Iterate();
        if (weapon.CanAttack()) weapon.Shoot();
    }

    void OnDie()
    {
        StartCoroutine(IEDying());
    }

    IEnumerator IEDying()
    {
        anim.PlayAnim(PlayerAnimState.Die);
        isLive = false;
        GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(anim.GetCurrentAnimLength());
        IngameManager.instance.Lose();
    }

    public void AddHealth(int _input)
    {
        _input -= playerData.def.value;
        characterHealth.AddHealth(_input);
    }

    public float GetHealthAmountNormalized()
    {
        return (float)characterHealth.CurHealth / (float)playerData.maxHp.value;
    }

    public void SetNewWeapon(IWeapon _weapon)
    {
        weapon = _weapon;
    }

    public void Prepare()
    {
        movement.Setup(); ;
        weapon.Setup();
        characterHealth.Setup(100);
        anim.PlayAnim(PlayerAnimState.NormalMovement);
    }

    public void StartGame()
    {
        isLive = true;
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
