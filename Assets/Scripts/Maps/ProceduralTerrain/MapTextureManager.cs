using System;
using UnityEngine;

public class MapTextureManager : MonoBehaviour
{
    public GameObject water;
    public Renderer waterRenderer;
    public GameObject terrain;
    public Renderer terrainRenderer;
    public Material[] waterMaterials;
    public Material[] terrainMaterials;

    private void Start() {
        try{
            waterRenderer = water.GetComponent<Renderer>();
            terrainRenderer = terrain.GetComponent<Renderer>();
        } catch (Exception e){
            Debug.Log(e);
        }
        
    }
    
    public void changeWaterColor(int color){
        waterRenderer.material= waterMaterials[color];
    }

    public void changeTerrainColor(int color){
        terrainRenderer.material = terrainMaterials[color];
    }

    public void changeColor(int color){
        changeTerrainColor(color);
        changeWaterColor(color);
    }
}
