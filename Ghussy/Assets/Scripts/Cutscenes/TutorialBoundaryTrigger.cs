using UnityEngine;

/**
 * Class to control a trigger in the tutorial.
 */
public class TutorialBoundaryTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }

        return;
    }
}
