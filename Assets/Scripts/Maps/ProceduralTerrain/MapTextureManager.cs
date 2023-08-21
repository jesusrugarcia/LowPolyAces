using System;
using UnityEngine;

public class MapTextureManager : MonoBehaviour
{
    public GameObject water;
    public Renderer waterRenderer;
    public GameObject terrain;
    public Renderer terrainRenderer;
    public StageColorsScriptableObject colors;
    public bool changeStageColors = false;
    public GameController controller;

    private void Start() {
        try{
            waterRenderer = water.GetComponent<Renderer>();
            terrainRenderer = terrain.GetComponent<Renderer>();
        } catch (Exception e){
            Debug.Log(e);
        }
        
    }
    
    public void changeWaterColor(int color){
        waterRenderer.material= colors.waterMaterials[color];
    }

    public void changeTerrainColor(int color){
        terrainRenderer.material = colors.terrainMaterials[color];
    }

    public void changeColor(int color = 0){
        if (controller.gameOptions.mode == gameMode.roguelite){
            waterRenderer.material = colors.waterMaterials[controller.rogueliteSave.stage];
            terrainRenderer.material = colors.terrainMaterials[controller.rogueliteSave.stage];
        } else {
            changeTerrainColor(color);
            changeWaterColor(color);
        }
        
    }
}
