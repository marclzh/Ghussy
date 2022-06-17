using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public GameObject weaponPrefab;
    public GameObject bulletPrefab;
}
