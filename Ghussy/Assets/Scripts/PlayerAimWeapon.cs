using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimWeapon : MonoBehaviour
{
    public Transform player;
    public float offSetDistance;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidBody;
    Vector2 mousePos;
    Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = player.GetComponent<Animator>();
    }

    private void Update()
    {
        CheckCurrentMousePos();
    }

    void CheckCurrentMousePos()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()); 
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        HandleAiming();
        HandleShooting();
    }

    void HandleAiming()
    {
        // Rotates weapon
        Vector3 directionPlayerIsLooking = mousePos - rigidBody.position;
        // directionPlayerIsLooking.z = 0;
        float angle = Mathf.Atan2(directionPlayerIsLooking.y, directionPlayerIsLooking.x) * Mathf.Rad2Deg;
        rigidBody.rotation = angle;
        rigidBody.position = player.position + (offSetDistance * directionPlayerIsLooking.normalized);

        if (directionPlayerIsLooking.x < 0)
        {
            spriteRenderer.flipY = true;
        }
        else if (directionPlayerIsLooking.x > 0)
        {
            spriteRenderer.flipY = false;
        }
    }

    void HandleShooting()
    {

    }
}
