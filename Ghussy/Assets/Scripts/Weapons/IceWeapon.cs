using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWeapon : Weapon
{
    private readonly float laserFireRate = 0.005f;
    private readonly float laserDamage = 4f;
    public Ability ability;
    [SerializeField] private float defDistanceRay = 10;
    public LineRenderer lineRenderer;
    public GameObject startVFX;
    public GameObject endVFX;

    private List<ParticleSystem> particles;

    // Audio
    private bool audioPlaying;

    // Laser logic goes here :)

    private void Start()
    {
        particles = new List<ParticleSystem>(); 
        FillLists();
    }

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
        startVFX.transform.position = startPos;
        lineRenderer.SetPosition(1, endPos);
        endVFX.transform.position = endPos;
    }

    void FillLists()
    {
        for (int i = 0; i < startVFX.transform.childCount; i++)
        {
            var ps = startVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (ps != null)
                particles.Add(ps);
        }

        for (int i = 0; i < endVFX.transform.childCount; i++)
        {
            var ps = endVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (ps != null)
                particles.Add(ps);
        }
    }

    public void EnableLaser()
    {
        // Audio
        if (!audioPlaying) { AudioManager.Instance.Play("Laser"); audioPlaying = true; }

        // Line Renderer
        lineRenderer.enabled = true;
        
        // Particles
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Play();
        }
    }

    public void DisableLaser() 
    {
        // Audio
        AudioManager.Instance.Stop("Laser");
        audioPlaying = false;

        // Line Renderer
        lineRenderer.enabled = false;
       
        // Particles
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Stop();
        }
    }


}
