using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WorldMapMovement : MonoBehaviour
{
    public PlayerInput input;
    public InputAction selectAction;
    public InputAction rotateAction;
    public InputAction pauseAction;


    public WorldMapManager mapManager;

    public GameObject plane;

    public int currentNode;
    public int[] availableNodes;
    public int selectedNode;
    public int selectedNodePos;

    public Vector3[] route;
    public bool isMoving = false;
    public int routePos = 0;

    public float timeToAdvance = 0.015f;
    public float timer = 0;
    public float selectTimer = 0;

    static bool isDown(InputAction action) => action.phase == InputActionPhase.Performed;
    static bool isUp(InputAction action) => action.phase == InputActionPhase.Canceled;

    public float pauseTimer = 0;

    void Start()
    {
        currentNode = mapManager.mapGraph.currentMapNode;
        availableNodes = mapManager.mapGraph.nodes[currentNode].getConnectedNodes();
        nextLayerNode();
        mapManager.mapPainter.DrawLines(selectedNode);
        calculateMovement();

        input = gameObject.GetComponent<PlayerInput>();

        var map = input.currentActionMap;
        selectAction = map.FindAction("Shoot",true);
        rotateAction = map.FindAction("Rotate",true);
        pauseAction = map.FindAction("Pause",true);
    }

    void Update()
    {
        if(!mapManager.isPaused){
            rotate();
            if(isDown(selectAction) && !isMoving){
                move();
            }
        }
        pause();
        
    }

    void FixedUpdate()
    {
        advance();
        selectTimer += Time.deltaTime;
    }

    public void rotate(){
        if(isDown(rotateAction) && selectTimer >= 0.1f && !isMoving){
            selectTimer = 0;
            Vector2 move = rotateAction.ReadValue<Vector2>();
            if(move.x < 0){
                if (selectedNodePos == 0){
                    selectedNodePos = availableNodes.Length - 1;
                    selectedNode = availableNodes[selectedNodePos];
                } else {
                    selectedNodePos += -1;
                    selectedNode = availableNodes[selectedNodePos];
                }
                mapManager.mapPainter.DrawLines(selectedNode);
            } else if(move.x > 0) {
                if (selectedNodePos == availableNodes.Length - 1){
                    selectedNodePos = 0;
                    selectedNode = availableNodes[selectedNodePos];
                } else {
                    selectedNodePos += 1;
                    selectedNode = availableNodes[selectedNodePos];
                }
                mapManager.mapPainter.DrawLines(selectedNode);
            }
        }
    }

    public void advance(){
        timer += Time.deltaTime;
        if(timer >= timeToAdvance && isMoving){
            timer = 0;
            routePos += 1;
            transform.position = route[routePos];
            if(routePos < route.Length - 1){
                transform.LookAt(route[routePos + 1]);
                transform.Rotate(-90,-90,0);
            } else {
                isMoving = false;
                transform.rotation = Quaternion.Euler(-90,0,0);
                mapManager.mapGraph.currentMapNode = selectedNode;
                currentNode = selectedNode;
                mapManager.mapGraph.nodes[currentNode].visited = true;
                availableNodes = mapManager.mapGraph.nodes[currentNode].getConnectedNodes();
                nextLayerNode();
                mapManager.mapPainter.DrawLines(selectedNode);
                mapManager.checkNodeAction(currentNode);
            }
            
        }
    }

    public void nextLayerNode(){
        for(int i=0; i< availableNodes.Length;i++){
            //Debug.Log("node: " + i + " layer: " + mapManager.mapGraph.nodes[availableNodes[i]].layer + " ; current node layer: " + mapManager.mapGraph.nodes[currentNode].layer);
            if(mapManager.mapGraph.nodes[availableNodes[i]].layer > mapManager.mapGraph.nodes[currentNode].layer){
                selectedNodePos = i;
                selectedNode = availableNodes[i];
                return;
            }
        }
        //Debug.Log("No bigger layer");
        selectedNodePos = 0;
        selectedNode = availableNodes[0];
    }

    public void move(){
        calculateMovement();
        isMoving = true;
        transform.position = route[0];
    }

    public void calculateMovement(int numberOfPoints = 25){
        routePos = 0;
        route = new Vector3[numberOfPoints];
        
        Vector3 p0 = mapManager.mapGraph.nodes[currentNode].screenPos + mapManager.planePosCorrection;
		Vector3 p2 = mapManager.mapGraph.nodes[selectedNode].screenPos + mapManager.planePosCorrection;
        Vector3 p1 = Vector3.Lerp(p0,p2,0.5f) + new Vector3(0,Vector3.Distance(p0,p2)/4,0);
		float t;
		Vector3 position;
		for(int i = 0; i < numberOfPoints; i++)
		{
			t = i / (numberOfPoints - 1.0f);
			position = (1.0f - t) * (1.0f - t) * p0 
			+ 2.0f * (1.0f - t) * t * p1 + t * t * p2;
			route[i] = position;
		}
    }

    public void pause(){
        pauseTimer += Time.deltaTime;
        if(isDown(pauseAction) && pauseTimer >= 0.25f){
            pauseTimer = 0;
            if(mapManager.isPaused){
                mapManager.resume();
              
            } else {
                mapManager.pause();
               
            }
        } 
    }
}
