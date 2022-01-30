using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWandering : MonoBehaviour
{
    public float speed = 1f; 
    public float movementStopTime = 4f;
    public float collisionCooldownTime = 2f;

    Rigidbody2D rb;
    Animator animator;
    Vector2 direction;
    Vector3 scale;
    float startMovingTime;
    float collisionCooldownEnds;
    bool isWalking;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        scale = transform.localScale;
        startMovingTime = Time.time;
        StartWalking();
    }

    // Update is called once per frame
    void Update()
    {
    }

    int Chase()
    {
        if(tag != "Enemy")
            return 0;
        GameObject player = GameObject.Find("Player");
        Vector3 dist = player.transform.position - transform.position;
        Vector2 direction = dist;
        if(dist.magnitude < 3)
        {
            rb.MovePosition(rb.position + direction * speed * 1.0f * Time.fixedDeltaTime);
            return 1;
        }
        else
            return 0;
    }

    void FixedUpdate()
    {
        int chasing = Chase();
        if(chasing == 0)
            Wander();
    }

    void Wander()
    {
        if (!isWalking && Time.time > startMovingTime)
            StartWalking();

        if (isWalking)
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        HandleCollision();
    }

    void OnCollisionStay2D(Collision2D other)
    {
        HandleCollision();
    }

    void HandleCollision()
    {
        // if walking and outside of cooldown, stop
        if (isWalking && Time.time > collisionCooldownEnds)
        {
            if (gameObject.name == "KittyWandering")
                Debug.Log("Collision! Stop walking");
            StopWalking();
        }
        // if walking and within cooldown, start walking in a new direction
        else if (isWalking)
        {
            if (gameObject.name == "KittyWandering")
                Debug.Log("Collision! Start walking");
            StartWalking();
        }
    }

    void FaceDirection(Vector2 direction)
    {
        if (!animator)
            return;

        Debug.Log("Face direction " + (direction.x < 0));

        if (direction.x < 0)
            transform.localScale = new Vector3(scale.x, scale.y, scale.z);
        else
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
    }

    void StartWalking()
    {
        isWalking = true;
        collisionCooldownEnds = Time.time + collisionCooldownTime;
        direction = Random.insideUnitCircle.normalized;
        FaceDirection(direction);
        if (animator)
            animator.SetBool("Walking", true);
    }

    void StopWalking()
    {
        isWalking = false;
        startMovingTime = Time.time + movementStopTime;
        if (gameObject.name == "KittyWandering")
            Debug.Log("Start walking in " + (startMovingTime - Time.time));
        if (animator)
            animator.SetBool("Walking", false);
    }
}
