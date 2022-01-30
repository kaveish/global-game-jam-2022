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
    GameObject front;
    GameObject back;
    GameObject side;

    void Start()
    {
        alive = true;
        rb = GetComponent<Rigidbody2D>();
        front = gameObject.transform.Find("Girl Front").gameObject;
        back = gameObject.transform.Find("Girl Back").gameObject;
        side = gameObject.transform.Find("Girl Side").gameObject;
        FaceDirection(Vector2.up);
        StopWalking();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    void OnMove(InputValue movementValue)
    {
        movement = movementValue.Get<Vector2>();

        if(movement.magnitude > 0.1f)
            StartWalking();
        else
            StopWalking();

        if (movement.y > 0f)
            FaceDirection(Vector2.up);
        else if (movement.y < 0f)
            FaceDirection(Vector2.down);
        else if (movement.x > 0f) 
            FaceDirection(Vector2.left);
    }

    void FaceDirection(Vector2 direction)
    {
        if (direction == Vector2.up)
        {
            front.SetActive(false);
            back.SetActive(true);
            side.SetActive(false);
        } else if (direction == Vector2.down) {
            front.SetActive(true);
            back.SetActive(false);
            side.SetActive(false);
        } else {
            front.SetActive(false);
            back.SetActive(false);
            side.SetActive(true);
        }
    }

    void StartWalking()
    {
        front.GetComponent<Animator>().SetBool("Walking", true);
        back.GetComponent<Animator>().SetBool("Walking", true);
        side.GetComponent<Animator>().SetBool("Walking", true);
    }

    void StopWalking()
    {
        front.GetComponent<Animator>().SetBool("Walking", false);
        back.GetComponent<Animator>().SetBool("Walking", false);
        side.GetComponent<Animator>().SetBool("Walking", false);
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
