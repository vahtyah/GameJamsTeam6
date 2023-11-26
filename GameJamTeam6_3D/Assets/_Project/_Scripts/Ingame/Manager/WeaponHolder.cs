using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : SerializedMonoBehaviour
{
    [SerializeField] Dictionary<WeaponID, WeaponHolderData> weaponData;

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
        return weaponData[_id].posOnModelHand;
    }

}

public enum WeaponID
{
    AK74
}
