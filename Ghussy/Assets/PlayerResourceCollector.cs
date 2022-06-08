using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerResourceCollector : MonoBehaviour
{ 
    private int ectoplasmCount = 0;

    [SerializeField] private TextMeshProUGUI EctoplasmText;

   // [SerializeField] private AudioSource collectionSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ectoplasm"))
        {
            //collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            EctoplasmObject ectoplasmObject = (EctoplasmObject) collision.gameObject.GetComponent<Item>().item;
            ectoplasmObject.SetAmount(Random.Range(1, 10));
            ectoplasmCount += ectoplasmObject.GetAmount();
            EctoplasmText.text = ectoplasmCount.ToString();
        }
    }

}
