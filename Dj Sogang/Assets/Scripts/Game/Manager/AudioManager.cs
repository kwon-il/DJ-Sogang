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
        myAudio.Play(100000);
    }
}
