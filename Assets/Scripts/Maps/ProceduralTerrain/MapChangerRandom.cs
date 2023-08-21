using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChangerRandom : MonoBehaviour
{
    public GameController controller;
    public MapTextureManager textureManager;
    public float timer = 0;
    public bool randomUpdate = false;
    

    private void Start() {
        controller.gameOptions = FileManager.loadOptions();
        if(controller.gameOptions.mode != gameMode.roguelite){
            changeMap();
        }
        
    }

    private void FixedUpdate() {
        if (randomUpdate){
            timer += Time.deltaTime;
            if(timer >= 1){
                timer = 0;
                changeMap();
            }
        }
        
    }

    public void changeMap(){
        controller.mapGenerator.seed = Random.Range(-9999,9999);
        controller.mapGenerator.GenerateMap();
        textureManager.changeColor(Random.Range(0,textureManager.colors.waterMaterials.Length));
    }

}
