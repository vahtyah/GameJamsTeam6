using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponHolderData", menuName = "WeaponHolderData")]
public class WeaponHolderData : SerializedScriptableObject
{
    public IWeapon weapon;
    public Vector3 posOnModelHand;
}
