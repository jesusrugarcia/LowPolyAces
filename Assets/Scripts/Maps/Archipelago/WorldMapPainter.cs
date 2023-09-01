using System;
using UnityEngine;

public class WorldMapPainter : MonoBehaviour
{
    public WorldMapManager mapManager;
    public WorldMapGenerator mapGenerator;
    public StageColorsScriptableObject colors;

    public GameObject nodeObject;
    public CameraPointCalculator pointCalculator;
    public GameObject shopIcon;
    public GameObject BossIcon;
    public GameObject water;
    public Renderer waterRenderer;

    public int layers;
    public int nodesPerLayer;
    public int maxNodes;

    public Vector3 initialPos;
    public Vector3 finalPos;
    public float xIncrease;
    public float zIncrease;
    public int maxFinalNodes;
    public float minDistance;
    public float distanceAdd;

    public float lineThickness = 1;

    public Material lineMaterial;
    public Material lineMaterialAvailable;
    public Material lineMaterialSelected;
    public Material lineMaterialVisited;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void generateAndDrawMap(){
        pointCalculator.realDistance = pointCalculator.transform.position.y ;
        pointCalculator.calculateBoundaries();

        xIncrease = (pointCalculator.x * 2) / (layers + 2);
        initialPos.x = -pointCalculator.x + (xIncrease/4);
        finalPos.x = pointCalculator.x - (xIncrease/4);
        zIncrease = (pointCalculator.z * 2) / (nodesPerLayer + layers + 4);

        mapManager.mapGraph = mapGenerator.generateWorldMapGraph(layers, nodesPerLayer, maxNodes, initialPos, finalPos, xIncrease, zIncrease, pointCalculator.x - xIncrease, pointCalculator.z - zIncrease, minDistance, distanceAdd, maxFinalNodes, mapManager.decoIslandSize);
        drawMap();
        FileManager.saveMap(mapManager.mapGraph);
        
    }

    public void drawMap(){
        createNodesList();
        for (int i=0; i < mapManager.mapGraph.size; i++){
            mapManager.nodes [i] = Instantiate(nodeObject,mapManager.mapGraph.nodes[i].screenPos, Quaternion.identity);
            var generator = nodeObject.GetComponent<MapGenerator>();
            generator.seed = mapManager.mapGraph.nodes[i].seed;
            generator.GenerateMap();
            if(mapManager.mapGraph.nodes[i].type == NodeType.Shop){
                var shop = Instantiate(shopIcon, mapManager.mapGraph.nodes[i].screenPos + new Vector3(0,1000,0), Quaternion.identity);
                shop.transform.localScale *= 2000;
            }
            var nodeRenderer = mapManager.nodes[i].GetComponent<MapTextureManager>().terrainRenderer;
            nodeRenderer.material = colors.mapMaterials[mapManager.rogueliteSave.stage];
        }

        //paint water
        waterRenderer = water.GetComponent<Renderer>();
        waterRenderer.material = colors.mapWaterMaterials[mapManager.rogueliteSave.stage];

        //boss icon
        var boss = Instantiate(BossIcon, mapManager.mapGraph.nodes[0].screenPos + new Vector3(0,1000,0), Quaternion.Euler(90,0,0));

        DrawLines(mapManager.mapGraph.nodes[mapManager.mapGraph.currentMapNode].getConnectedNodes()[0]);
        drawDecoIslands();
    }

    public void createNodesList(){
        try {
             for (int i=0; i < mapManager.nodes.Length; i++){
                Destroy(mapManager.nodes [i]);
             }
            mapManager.nodes = new GameObject[mapManager.mapGraph.size];
        } catch (Exception e){
            mapManager.nodes = new GameObject[mapManager.mapGraph.size];
            Debug.Log(e);
        } 
    }

    public void DrawLines(int selected)
    {
        createLinesList();
        for (int i=0; i < mapManager.mapGraph.size; i++){
            for (int j=i+1; j < mapManager.mapGraph.nodes[i].connectedNodes.Length ; j++){
                if (mapManager.mapGraph.nodes[i].connectedNodes[j] == true){
                    bool isSelected = false;
                    if(j == selected && i == mapManager.mapGraph.currentMapNode|| i == selected && j == mapManager.mapGraph.currentMapNode){
                        isSelected = true;
                    }
                    bool isVisited = false;
                    if(mapManager.mapGraph.nodes[j].visited == true && mapManager.mapGraph.nodes[i].visited == true){
                        isVisited = true;
                    }
                    if(mapManager.mapGraph.nodes[i].pos == mapManager.mapGraph.currentMapNode || mapManager.mapGraph.nodes[j].pos == mapManager.mapGraph.currentMapNode){
                        addLine(drawLine(mapManager.mapGraph.nodes[i].screenPos, mapManager.mapGraph.nodes[j].screenPos, lineThickness, true, isSelected,isVisited));
                    } else {
                        addLine(drawLine(mapManager.mapGraph.nodes[i].screenPos, mapManager.mapGraph.nodes[j].screenPos, lineThickness, false, isSelected, isVisited));
                    }
                    
                }
            }
        }
    }

    public void createLinesList(){
        try {
             for (int i=0; i < mapManager.lines.Length; i++){
                Destroy(mapManager.lines [i]);
             }
             mapManager.lines = new GameObject[0];
        } catch (Exception e){
            mapManager.lines = new GameObject[0];
            Debug.Log(e);
        } 
    }

    public void addLine(GameObject line){
        var newLines = new GameObject[mapManager.lines.Length + 1];
        for (int i=0; i < mapManager.lines.Length; i++){
            newLines[i] = mapManager.lines[i];
        }
        newLines[mapManager.lines.Length] = line;
        mapManager.lines = newLines;
    }

    public GameObject drawLine(Vector3 start, Vector3 end, float thickness, bool available, bool selected, bool visited){
        if (thickness == 0){
            thickness = 0.01f;
        }

        GameObject line = new GameObject();
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
        if(selected){
            lineRenderer.material = lineMaterialSelected;
            thickness *= 3f;
        } else if(visited){
            if(available){
                thickness *= 3f;
            } else {
                thickness *= 1.5f;
            }
            lineRenderer.material = lineMaterialVisited;
        } else if(available){
            lineRenderer.material = lineMaterialAvailable;
            thickness *= 3f;
        }  else {
            lineRenderer.material = lineMaterial;
        }
        
        
        
        
        lineRenderer.startWidth = thickness;
        lineRenderer.endWidth = thickness;

        Bezier bezier = line.AddComponent<Bezier>();
        bezier.lineRenderer = lineRenderer;
        bezier.controlPoints= new Vector3[3]{start, Vector3.Lerp(start, end, 0.5f) + new Vector3(0,Vector3.Distance(start, end)/2,0), end};
        bezier.DrawCurve();
        //lineRenderer.SetPosition(0,start);
        //lineRenderer.SetPosition(1,end);

        return line;
    }

    public void drawDecoIslands(){
        mapManager.decoIslands = new GameObject[mapManager.decoIslandSize];
        for(int i=0; i < mapManager.decoIslandSize;i++){
            mapManager.decoIslands[i] = Instantiate(nodeObject,mapManager.mapGraph.decoIslands[i].screenPos, Quaternion.identity);
            mapManager.decoIslands[i].transform.localScale *= 0.5f;
            var generator = nodeObject.GetComponent<MapGenerator>();
            generator.seed = mapManager.mapGraph.decoIslands[i].seed;
            generator.GenerateMap();
            
        }
         
    }


}
