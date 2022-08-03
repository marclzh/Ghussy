using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Animator transition = GetComponent<Animator>(); 
        transition.Play("Crossfade_End");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
