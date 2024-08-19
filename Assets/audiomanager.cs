using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiomanager : MonoBehaviour
{

    public AudioSource sound_audio;
    public AudioClip hitac;



    public void play_audio()
    {

        sound_audio.PlayOneShot(hitac);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
