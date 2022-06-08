using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    private Transform target;
    //public GameObject targetTracker;
    Vector3 velocity = Vector3.zero;
    public float minModifier;
    public float maxModifier;
    bool isFollowing = false;

    void Start()
    {
        GameObject targetTracker = GameObject.FindGameObjectWithTag("GhussyTarget");
        target = targetTracker.transform;
    }

    public void StartFollowing()
    {
        isFollowing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            //transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, Time.deltaTime * Random.Range(minModifier, maxModifier));
           transform.position = Vector3.Lerp(this.transform.position, target.position, Time.deltaTime * Random.Range(minModifier, maxModifier));
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            Destroy(gameObject);
        }
    }
}
