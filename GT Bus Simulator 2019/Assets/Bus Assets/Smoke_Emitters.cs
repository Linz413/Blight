using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke_Emitters : MonoBehaviour
{
    // Start is called before the first frame update
    public float damageLevel;
    public GameObject regular;
    public GameObject medium;
    public GameObject heavy;
    private ParticleSystem regularps, mediumps, heavyps;
    
    void Start()
    {
        regularps = regular.GetComponent<ParticleSystem>();
        mediumps = medium.GetComponent<ParticleSystem>();
        heavyps = heavy.GetComponent<ParticleSystem>();
        mediumps.Stop();
        heavyps.Stop();

    }

    // Update is called once per frame
    void Update()
    {
        if(damageLevel < 0f)
        {
            regularps.Stop();
            mediumps.Stop();
            heavyps.Stop();
        } else if(damageLevel <= 33f)
        {
            if (!regularps.isPlaying)
            {
                regularps.Play();
            }
            mediumps.Stop();
            heavyps.Stop();
        } else if (damageLevel <= 66f)
        {
            if (!mediumps.isPlaying)
            {
                mediumps.Play();
            }
            regularps.Stop();
            heavyps.Stop();
        }
        else if (damageLevel <= 100f)
        {
            if (!heavyps.isPlaying)
            {
                heavyps.Play();
            }
            mediumps.Stop();
            regularps.Stop();
        }
    }
    public void deactivate()
    {
        regularps.Stop();
        mediumps.Stop();
        heavyps.Stop();
    }
    public void start()
    {
        regularps.Play();
        mediumps.Play();
        heavyps.Play();
    }
}
