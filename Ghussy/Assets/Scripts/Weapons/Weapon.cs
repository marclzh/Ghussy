using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate;
    [SerializeField] float baseFireRate;

    public Transform GetFirePoint()
    {
        return firePoint;
    }

    public float GetBaseFireRate()
    {
        return baseFireRate;
    }
}

