using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Spineキャラクターのアクション時のSE管理スクリプト
public class SkeltonEvents : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip footStepSound;
    public AudioClip attackSound;
    public AudioClip jumpSound;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void footstep()
    {
        audioSource.PlayOneShot(footStepSound);
    }

    public void attack()
    {
        audioSource.PlayOneShot(attackSound);
    }
}
