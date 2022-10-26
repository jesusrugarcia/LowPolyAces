using System;
using UnityEngine;

public class WorldMapPainter : MonoBehaviour
{
    public bool loadMap;
    public WorldMapGraph mapGraph;
    public WorldMapGenerator mapGenerator;

    public GameObject nodeObject;
    public CameraPointCalculator pointCalculator;

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
    public GameObject[] lines;
    public GameObject[] nodes;

    public float timer  = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(!loadMap){
            generateAndDrawMap();
            
        } else {
            mapGraph = FileManager.loadMap();
            if (mapGraph != null){
                drawMap();
            } else {
                generateAndDrawMap();
            }
        }
       
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if(timer > 3){
            timer = 0;
            generateAndDrawMap();
        }
    }


    public void generateAndDrawMap(){
        pointCalculator.realDistance = pointCalculator.transform.position.y ;
        pointCalculator.calculateBoundaries();

        xIncrease = (pointCalculator.x * 2) / (layers + 2);
        initialPos.x = -pointCalculator.x + (xIncrease/4);
        finalPos.x = pointCalculator.x - (xIncrease/4);
        zIncrease = (pointCalculator.z * 2) / (nodesPerLayer + layers + 4);

        mapGraph = mapGenerator.generateWorldMapGraph(layers, nodesPerLayer, maxNodes, initialPos, finalPos, xIncrease, zIncrease, pointCalculator.x - xIncrease, pointCalculator.z - zIncrease, minDistance, distanceAdd, maxFinalNodes);
        mapGraph.currentMapNode = UnityEngine.Random.Range(0,mapGraph.size);
        drawMap();
        FileManager.saveMap(mapGraph);
    }

    public void drawMap(){
        createNodesList();
        for (int i=0; i < mapGraph.size; i++){
            nodes [i] = Instantiate(nodeObject,mapGraph.nodes[i].screenPos, Quaternion.identity);
            var generator = nodeObject.GetComponent<MapGenerator>();
            generator.seed = mapGraph.nodes[i].seed;
            generator.GenerateMap();
        }
        DrawLines();
    }

    public void createNodesList(){
        try {
             for (int i=0; i < nodes.Length; i++){
                Destroy(nodes [i]);
             }
            nodes = new GameObject[mapGraph.size];
        } catch (Exception e){
            nodes = new GameObject[mapGraph.size];
            Debug.Log(e);
        } 
    }

    public void DrawLines()
    {
        createLinesList();
        for (int i=0; i < mapGraph.size; i++){
            for (int j=i; j < mapGraph.nodes[i].connectedNodes.Length ; j++){
                if (mapGraph.nodes[i].connectedNodes[j] == true){
                    if(mapGraph.nodes[i].pos == mapGraph.currentMapNode || mapGraph.nodes[j].pos == mapGraph.currentMapNode){
                        addLine(drawLine(mapGraph.nodes[i].screenPos, mapGraph.nodes[j].screenPos, lineThickness, true));
                    } else {
                        addLine(drawLine(mapGraph.nodes[i].screenPos, mapGraph.nodes[j].screenPos, lineThickness, false));
                    }
                    
                }
            }
        }
    }

    public void createLinesList(){
        try {
             for (int i=0; i < lines.Length; i++){
                Destroy(lines [i]);
             }
             lines = new GameObject[0];
        } catch (Exception e){
            lines = new GameObject[0];
            Debug.Log(e);
        } 
    }

    public void addLine(GameObject line){
        var newLines = new GameObject[lines.Length + 1];
        for (int i=0; i < lines.Length; i++){
            newLines[i] = lines[i];
        }
        newLines[lines.Length] = line;
        lines = newLines;
    }

    public GameObject drawLine(Vector3 start, Vector3 end, float thickness, bool available){
        if (thickness == 0){
            thickness = 0.01f;
        }

        GameObject line = new GameObject();
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();

        if(available){
            var color = lineMaterialAvailable.GetColor("_Color");
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
            lineRenderer.material = lineMaterialAvailable;
            thickness *= 2f;
        } else {
            var color = lineMaterial.GetColor("_Color");
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
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

}
