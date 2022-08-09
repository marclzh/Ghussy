/**
 * Class to control the electric ghost enemy.
 */

public class ElectricGhost : Enemy
{
    public void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }
}
