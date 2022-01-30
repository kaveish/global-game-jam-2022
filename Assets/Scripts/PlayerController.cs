using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    public bool alive;
    Rigidbody2D rb;
    Vector2 movement;

    void Start()
    {
        alive = true;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    void OnMove(InputValue movementValue)
    {
        movement = movementValue.Get<Vector2>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //Checks if other gameobject has a Tag of Player
        if (other.gameObject.tag == "Enemy")
        {
            //Sets player status to dead
            alive = false;

            //Pauses gameplay
            Time.timeScale = 0;
        }

    }
}
