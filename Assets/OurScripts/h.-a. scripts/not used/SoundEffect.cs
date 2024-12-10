using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
//this script can hold a list of sound files which can be played on command
//The sound files are added in the inspector

//when you want to play a sound in a different scribt, just reference this script
//use the play method and give the number for the audio file you want to play
    public AudioSource SoundSource;
    public AudioClip[] AudioList;


    public void play(int number)
    {
        SoundSource.clip = AudioList[number];
        SoundSource.Play();
    }
}
