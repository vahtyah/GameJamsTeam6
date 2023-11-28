using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{

    [Button(ButtonHeight = 50)]
    public void ChangeGun(WeaponID _id)
    {
        GameObject newObj = Instantiate(WeaponHolder.instance.GetIWeapon(_id).GetGameObject(), Player.instance.GetModelRightHand());
        IWeapon weapon = newObj.GetComponent<IWeapon>();
        weapon.Setup();
        if (Player.instance.GetWeapon() != null)
        {
            Destroy(Player.instance.GetWeapon().GetGameObject());
        }
        Player.instance.SetNewWeapon(weapon);
    }







}
