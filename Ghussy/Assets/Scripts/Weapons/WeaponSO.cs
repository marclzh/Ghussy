using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
public class WeaponSO : ScriptableObject
{
    public string weaponName;
    public GameObject weaponPrefab;
    public GameObject bulletPrefab;
}
