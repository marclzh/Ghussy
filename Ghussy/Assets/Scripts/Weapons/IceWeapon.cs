using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWeapon : Weapon
{
    private readonly float laserFireRate = 0.00001f;
    public Ability ability;
    [SerializeField] private float defDistanceRay = 25;
    public LineRenderer lineRenderer;

    // Laser logic goes here :)
    public void ShootLaser()
    {
        fireRate = laserFireRate;
        if (Physics2D.Raycast(transform.position, transform.right))
        {
            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, transform.right);
            Debug.Log(hit.collider.gameObject.name);
            Draw2DRay(firePoint.position, hit.point);
        }
        else
        {
            Draw2DRay(firePoint.position, firePoint.transform.right * defDistanceRay);
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

}
