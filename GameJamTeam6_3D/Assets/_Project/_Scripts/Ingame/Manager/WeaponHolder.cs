using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : SerializedMonoBehaviour
{
    public static WeaponHolder instance;

    [SerializeField] Dictionary<WeaponID, WeaponHolderData> weaponData;

    private void Awake()
    {
        instance = this;
    }

    public WeaponHolderData GetWeapon(WeaponID _id)
    {
        return weaponData[_id];
    }

    public IWeapon GetIWeapon(WeaponID _id)
    {
        return weaponData[_id].weapon;
    }

    public Vector3 GetWeaponLocalPositionOnHand(WeaponID _id)
    {
        return weaponData[_id].modelOnHandPos;
    }

}

public enum WeaponID
{
    AK74 = 0, ScarL = 1, Bazooka = 2
}
