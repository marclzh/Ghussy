using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * The general weapon class.
 */
public class Weapon : MonoBehaviour
{
    // Components necessary for the weapon to fire.
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate;
    [SerializeField] float baseFireRate;

    // Getter for the firepoint of the weapon.
    public Transform GetFirePoint()
    {
        return firePoint;
    }

    // Getter for the base fire rate of the weapon. (Mainly for laser)
    public float GetBaseFireRate()
    {
        return baseFireRate;
    }
}

