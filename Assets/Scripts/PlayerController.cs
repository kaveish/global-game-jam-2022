using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    public bool alive;
    public GameObject deadPlayerPrefab;
    public float health = 100;
    Rigidbody2D rb;
    Vector2 movement;
    GameObject front;
    GameObject back;
    GameObject side;
    Vector3 scale;
    public GameObject bgMusic;
    AudioSource bgMusicSrc;
    static string[] hintTexts = {
        "Player 2 can hear the difference between friendly and deadly NPCs. Work together!",
        "Your health decreases as you walk around the life-sapping maze. Hug friendly NPCs to regain it.",
        "Git Gud",
        "Enemies kill you immediately on contact. Plan your moves with your partner."
    };
    void Start()
    {
        alive = true;
        rb = GetComponent<Rigidbody2D>();
        scale = transform.localScale;
        front = gameObject.transform.Find("Girl Front").gameObject;
        back = gameObject.transform.Find("Girl Back").gameObject;
        side = gameObject.transform.Find("Girl Side").gameObject;
        FaceDirection(Vector2.up);
        StopWalking();
    }

    void FixedUpdate()
    {

        health = health - Time.fixedDeltaTime * 1.5f;
        //Debug.Log(health);
        if (health < 0)
            die();
        Text textbox = GetComponentInChildren<Text>();
        textbox.text = "Health: " + Mathf.RoundToInt(health).ToString() + "/100";
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        speed = 3.0f;

    }
    public void OnSprintPress()
    {
        speed = 9.0f;
    }

    void OnMove(InputValue movementValue)
    {
        movement = movementValue.Get<Vector2>();

        if (movement.magnitude > 0.1f)
            StartWalking();
        else
            StopWalking();

        if (movement.y > 0f)
        {
            transform.localScale = new Vector3(scale.x, scale.y, scale.z);
            FaceDirection(Vector2.up);
        }
        else if (movement.y < 0f)
        {
            transform.localScale = new Vector3(scale.x, scale.y, scale.z);
            FaceDirection(Vector2.down);
        }
        else if (movement.x > 0f)
        {
            transform.localScale = new Vector3(scale.x, scale.y, scale.z);
            FaceDirection(Vector2.left);
        }
        else if (movement.x < 0f)
        {
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
            FaceDirection(Vector2.left);
        }
    }

  
    void FaceDirection(Vector2 direction)
    {
        if (direction == Vector2.up)
        {
            front.SetActive(false);
            back.SetActive(true);
            side.SetActive(false);
        }
        else if (direction == Vector2.down)
        {
            front.SetActive(true);
            back.SetActive(false);
            side.SetActive(false);
        }
        else
        {
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

    void die()
    {
        health = 0.0f;

        //Sets player status to dead
        alive = false;

        Instantiate(deadPlayerPrefab, transform);
        GetComponent<PlayerInput>().enabled = false;
        front.SetActive(false);
        back.SetActive(false);
        side.SetActive(false);

        //Pauses gameplay
        Time.timeScale = 0;

        bgMusic = GameObject.Find("bgmvol1");
        bgMusicSrc = bgMusic.GetComponent<AudioSource>();
        bgMusicSrc.Pause();

        Text hintText = GameObject.Find("DeathScreen").transform.Find("Canvas").transform.Find("Hint").GetComponent<Text>();
        int rand = Random.Range(0, hintTexts.Length);
        hintText.text = "Hint:\n" + hintTexts[rand];
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //Checks if other gameobject has a Tag of Player
        if (other.gameObject.tag == "Enemy")
        {
            die();
        }
        else if (other.gameObject.tag == "Friend")
        {
            health = 100.0f;
        }

    }
}
