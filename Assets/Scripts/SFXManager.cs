using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource Explosion;

    public void playExplosion(){
        Explosion.Play();
    }
}
