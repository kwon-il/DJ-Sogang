using RainbowArt.CleanFlatUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource myAudio;
    public void Start()
    {
        myAudio = GetComponent<AudioSource>();  
    }

    public void audioPlay()
    {
        myAudio.Play(100000);
    }

    public void audioStop()
    {
        myAudio.Stop();
    }
}
