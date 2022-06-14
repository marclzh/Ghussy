using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeLevelLoader : MonoBehaviour
{
    public GameObject levelLoader;

    // Start is called before the first frame update
    void Awake()
    {
        levelLoader.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
