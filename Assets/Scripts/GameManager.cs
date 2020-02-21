using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoSingleton<GameManager>
{
    public List<LevelConfig> levels;
    Camera cameraObj;
    UIManager uiManager;
    MapController mapController;
    public int currentLevel;
    Coroutine foodClock;
    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("currentLevel", 0);
        if (currentLevel >= levels.Count) { currentLevel = levels.Count - 1; }

        cameraObj = GameObject.FindObjectOfType<Camera>();
        uiManager = new UIManager();
        mapController = new MapController(levels[currentLevel].tilePrefab, levels[currentLevel].grassSprite, levels[currentLevel].food, levels[currentLevel].foodSprites);
        if (levels[currentLevel].width % 2 == 0) { levels[currentLevel].width++; }
        if (levels[currentLevel].height % 2 == 0) { levels[currentLevel].height++; }
        mapController.GenerateMap(levels[currentLevel].width, levels[currentLevel].height);
        CreateArena(levels[currentLevel].width, levels[currentLevel].height, cameraObj);
       
    }
    public void UpdateLevel()
    {
        PlayerPrefs.SetInt("currentLevel", ++currentLevel);
    }
    void CreateArena(int x, int y, Camera cam)
    {
        int camSize = Mathf.Max(x, y);
        float xPos = ((float)x / 2);
        float yPos = ((float)y / 2);
        cam.orthographicSize = (camSize / 2) + 1;
        GameObject edge;
        edge = GameObject.Instantiate(levels[currentLevel].edges, new Vector3(-xPos - .25f, 0, 0), Quaternion.identity);
        edge.name = "left";
        edge.transform.localScale = new Vector3(edge.transform.localScale.x, y + .5f, edge.transform.localScale.z);
        edge = GameObject.Instantiate(levels[currentLevel].edges, new Vector3(xPos + .25f, 0, 0), Quaternion.identity);
        edge.name = "right";
        edge.transform.localScale = new Vector3(edge.transform.localScale.x, y + .5f, edge.transform.localScale.z);
        edge = GameObject.Instantiate(levels[currentLevel].edges, new Vector3(0, yPos + .25f, 0), Quaternion.identity);
        edge.name = "top";
        edge.transform.localScale = new Vector3(x + .5f, edge.transform.localScale.y, edge.transform.localScale.z);
        edge = GameObject.Instantiate(levels[currentLevel].edges, new Vector3(0, -yPos - .25f, 0), Quaternion.identity);
        edge.name = "bottom";
        edge.transform.localScale = new Vector3(x + .5f, edge.transform.localScale.y, edge.transform.localScale.z);
    }
    public void SpawnPlayer()
    {
        Instantiate(levels[currentLevel].player.playerPrefab, new Vector3(0, 0, 0), Quaternion.identity, null).GetComponent<Snake>();
        foodClock=StartCoroutine(FoodTimerClock());
    }
    public void ResetFoodClock()
    {
        MapController.Instance.RemoveFood();
        StopCoroutine(foodClock);
        foodClock=StartCoroutine(FoodTimerClock());
    }
    public void RemoveTile(GameObject tile)
    {
        mapController.RemoveTile(tile);
    }

    IEnumerator FoodTimerClock()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            mapController.SpawnFood();
            yield return new WaitForSeconds(levels[currentLevel].foodTimer);
            mapController.RemoveFood();
        }
    }

}
