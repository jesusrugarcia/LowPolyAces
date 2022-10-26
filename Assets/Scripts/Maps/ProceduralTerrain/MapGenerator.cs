using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode{
        NoiseMap,
        ColorMap,
        Mesh,
        FallOff
    }
    public DrawMode drawMode;
    const int chunkSize = 241;
    [Range(0,6)]
    public int levelOfDetail;
    public int seed;
    public float noiseScale;
    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;
    public Vector2 offset;
    public float heightMultiplier;
    public AnimationCurve meshHeightCurve;
    public bool autoUpdate;

    public bool fallOff;
    public float fallA;
    public float fallB;
    public TerrainType[] regions;

    float[,] fallOffMap;

    public void GenerateMap(){
        float[,] noiseMap = Noise.GenerateNoiseMap(chunkSize,chunkSize,seed,noiseScale,octaves,persistance,lacunarity, offset);
        if(fallOff){
            fallOffMap = FallOffGenerator.GenerateFallOffMap(chunkSize,fallA,fallB);
        }
        Color[] colorMap = new Color[chunkSize * chunkSize];
        for(int y=0; y<chunkSize; y++){
          for(int x=0; x<chunkSize; x++){
                if(fallOff){
                    noiseMap[x,y] = Mathf.Clamp01(noiseMap[x,y] - fallOffMap[x,y]);
                }
                float currentHeight = noiseMap[x,y];
                for(int i=0; i<regions.Length;i++){
                    if(currentHeight <= regions[i].height){
                        colorMap[y * chunkSize + x] = regions[i].color;
                        break; 
                    }
                }
            }  
        }
        MapDisplay display = FindObjectOfType<MapDisplay>();
        if(drawMode == DrawMode.NoiseMap){
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        } else if(drawMode == DrawMode.ColorMap){
            display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, chunkSize, chunkSize));
        } else if(drawMode == DrawMode.Mesh){
            display.DrawMesh(MeshGenerator.GenerateMeshTerrain(noiseMap, heightMultiplier, meshHeightCurve, levelOfDetail), TextureGenerator.TextureFromColorMap(colorMap, chunkSize, chunkSize));
        } else if(drawMode == DrawMode.FallOff){
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(fallOffMap));
        }
        
    }

    void OnValidate()
    {
        if(lacunarity < 1){
            lacunarity = 1;
        }
        if(octaves < 0){
            octaves = 0;
        }
    }

}

[System.Serializable]
public struct TerrainType{
    public string name;
    public float height;
    public Color color;
}
