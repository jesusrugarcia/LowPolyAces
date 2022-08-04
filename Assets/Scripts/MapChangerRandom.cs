using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChangerRandom : MonoBehaviour
{
    public GameController controller;
    public float timer = 0;

    private void FixedUpdate() {
        timer += Time.deltaTime;
        if(timer >= 1){
            timer = 0;
            controller.mapGenerator.seed = Random.Range(-9999,9999);
            controller.mapGenerator.GenerateMap();
        }
    }
}
