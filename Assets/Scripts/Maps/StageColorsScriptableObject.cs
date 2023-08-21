using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Proyecto Aviones/StageColorsObject")]
public class StageColorsScriptableObject : ScriptableObject
{
    public Material[] waterMaterials;
    public Material[] terrainMaterials;
    public Material[] mapMaterials;
    public Material[] mapWaterMaterials;
}
