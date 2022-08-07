using UnityEngine;

public class IceWeapon : Weapon
{
    private readonly float laserFireRate = 0.005f;
    private readonly float laserDamage = 2f;
    public Ability ability;
    [SerializeField] private float defDistanceRay = 10;
    public LineRenderer lineRenderer;


    // Laser logic goes here :)
    public void ShootLaser()
    {
        fireRate = laserFireRate;
        FindObjectOfType<AudioManager>().Play("WeaponFire1");
        if (Physics2D.Raycast(transform.position, transform.right))
        {
            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, transform.right);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Enemy")
                {
                    hit.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(laserDamage);
                }

                if (hit.collider.gameObject.tag == "Boss")
                {
                    hit.collider.gameObject.GetComponent<BossHealth>().TakeDamage(laserDamage);
                }

                Draw2DRay(firePoint.position, hit.point);
            }
            else
            {
                Draw2DRay(firePoint.position, firePoint.transform.right * defDistanceRay);
            }
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
