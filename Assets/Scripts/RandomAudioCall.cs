using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioCall : MonoBehaviour
{
    //public AudioSource myCall;
    public List<AudioSource> calls = new List<AudioSource>();
    public float minimum_delay = 3.5f;
    public float maximum_delay = 10.5f;
    private float last_fire = 0.0f;
    private float delay = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        delay = Random.Range(minimum_delay, maximum_delay);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > last_fire + delay)
        {
            //myCall.Play();
            int index = Random.Range(0,calls.Count);
            float pitch = 1.0f;
            if(tag == "Enemy")
            {
                pitch = Random.Range(0.5f, 1.5f);
            }
            calls[index].Play();
            calls[index].pitch = pitch;
            last_fire = Time.time;
            delay = Random.Range(minimum_delay, maximum_delay);
        }
    }

}
