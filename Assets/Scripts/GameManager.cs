using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoSingleton<GameManager>
{
    public int width = 10, height = 10;
    public GameObject tilePrefab;
    public Sprite grassSprite;
    public GameObject player;
    public GameObject food;
    public int foodTimer;
    Camera cameraObj;
    UIManager uiManager;
    GameObject edges;
    MapController mapController;
    void Start()
    {
        cameraObj = GameObject.FindObjectOfType<Camera>();
        edges = Resources.Load("edges") as GameObject;
        uiManager = new UIManager();
        mapController = new MapController(tilePrefab, grassSprite, food);
        if (width % 2 == 0) { width++; }
        if (height % 2 == 0) { height++; }
        mapController.GenerateMap(width, height);
        CreateArena(width, height, cameraObj);
       
    }
    void CreateArena(int x, int y, Camera cam)
    {
        int camSize = Mathf.Max(x, y);
        float xPos = ((float)x / 2);
        float yPos = ((float)y / 2);
        cam.orthographicSize = (camSize / 2) + 1;
        GameObject edge;
        edge = GameObject.Instantiate(edges, new Vector3(-xPos - .25f, 0, 0), Quaternion.identity);
        edge.name = "left";
        edge.transform.localScale = new Vector3(edge.transform.localScale.x, y + .5f, edge.transform.localScale.z);
        edge = GameObject.Instantiate(edges, new Vector3(xPos + .25f, 0, 0), Quaternion.identity);
        edge.name = "right";
        edge.transform.localScale = new Vector3(edge.transform.localScale.x, y + .5f, edge.transform.localScale.z);
        edge = GameObject.Instantiate(edges, new Vector3(0, yPos + .25f, 0), Quaternion.identity);
        edge.name = "top";
        edge.transform.localScale = new Vector3(x + .5f, edge.transform.localScale.y, edge.transform.localScale.z);
        edge = GameObject.Instantiate(edges, new Vector3(0, -yPos - .25f, 0), Quaternion.identity);
        edge.name = "bottom";
        edge.transform.localScale = new Vector3(x + .5f, edge.transform.localScale.y, edge.transform.localScale.z);
    }
    public void SpawnPlayer()
    {
        Debug.Log("player");
        Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity, null);
        StartCoroutine(FoodTimerClock());
    }
    void Update()
    {

    }
    public void RemoveTile(GameObject tile)
    {
        mapController.RemoveTile(tile);
    }

    IEnumerator FoodTimerClock()
    {
        while (true)
        {
            mapController.SpawnFood();
            yield return new WaitForSeconds(foodTimer);
            mapController.RemoveFood();
            yield return new WaitForSeconds(1.5f);
        }
    }

}
