using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioCall : MonoBehaviour
{
    public AudioSource myCall;
    public float minimum_delay = 3.5f;
    public float maximum_delay = 10.5f;
    private float last_fire = 0.0f;
    private float delay = 0.5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > last_fire + delay)
        {
            myCall.Play();
            last_fire = Time.time;
            delay = Random.Range(minimum_delay, maximum_delay);
        }
    }

}
