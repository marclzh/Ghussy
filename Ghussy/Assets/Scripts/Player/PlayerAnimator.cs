using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    //private bool isTransformed;
    private string currState;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //isTransformed = false;
        currState = "Default";
    }

    public void IsPlayerMoving(bool b)
    {
        animator.SetBool("isMoving", b); 
    }

    public void IsPlayerAttacking(bool b)
    {
        animator.SetBool("isFiring", b);
    }
    public void PlayerHit()
    {
        if (currState == "Default")
        {
            animator.Play("ghussy_hit");
        } 
        else if (currState == "SkeletonTransformation")
        {
            animator.Play("skelly_hit");
        } 
        else
        {

        }
        
    }
    public void BaseTransform()
    {
        animator.SetBool("isTransforming", true);
    }

    public void PlayerTransform()
    {
        animator.SetTrigger(currState);
    }

    public void IsPlayerDead(bool b)
    {
        animator.SetBool("isDead", b);
    }

    public void IsTransformationDead()
    {
        animator.SetTrigger("isTransformDead");
        currState = "Default";  
        //isTransformed = false;
    }

    public void UpdatePossessionState(BasePossessionState nextState)
    {
        currState = nextState.ToString();
        //isTransformed = true;
        PlayerTransform();
    }
}
