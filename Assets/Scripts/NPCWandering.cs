using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWandering : MonoBehaviour
{
    public float speed = 1f; 
    public float movementStopTime = 1f;
    public float collisionTimeout = 0.1f;

    Rigidbody2D rb;
    Vector2 direction;
    float startMovingTime;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = Random.insideUnitCircle.normalized;
        startMovingTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        Wander();
    }

    void Wander()
    {
        if (Time.time < startMovingTime)
            return;
        
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        startMovingTime = Time.time + 1f;
        direction = Random.insideUnitCircle.normalized;
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (Time.time > startMovingTime + collisionTimeout)
            direction = Random.insideUnitCircle.normalized;
    }

    void OnTriggerEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<PlayerController>().alive = false;
            Time.timeScale = 0;
        }
    }
}
