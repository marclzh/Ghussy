using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] VoidEvent OnBossTrigger;
    [SerializeField] Transform BossBlock;
    [SerializeField] Transform BossOverlay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnBossTrigger.Raise();
            AudioManager.Instance.Play("Metal");
            AudioManager.Instance.Stop("Theme");
            AudioManager.Instance.Play("Boss1");

            BossBlock.gameObject.SetActive(true);
            BossOverlay.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
