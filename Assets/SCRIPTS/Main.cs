using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class Main : MonoBehaviour
{
    private float animationTimer = 0.0f;
    private float timer = 3.5f;
    public TextMeshPro counter;
    public bool timerFinished = false;
    public bool animationTimerFinished = false;
    public AudioSource counterSound;
    public AudioSource introSource;
    public AudioClip introAudioClip;

    void Update()
    {
        if (counter)
        {
            if (Math.Round(animationTimer) == 8)
            {
                if (!counterSound.isPlaying)
                {
                    // counterSound.Play();
                }
                counter.enabled = true;
                animationTimerFinished = true;
                if (Math.Round(timer) > 1)
                {
                    timer -= Time.deltaTime;
                    counter.text = timer.ToString();
                }
                else if (Math.Round(timer) == 1)
                {
                    timerFinished = true;
                    Destroy(counter);
                }
            }
            else
            {
                animationTimer += Time.deltaTime;
                counter.enabled = false;
            }
        }
    }

    public void PlayIntroSound(){
        introSource.time = 8f;
        introSource.PlayOneShot(introAudioClip, 0.7F);
        introSource.SetScheduledEndTime(AudioSettings.dspTime + 8f);
    }
}
