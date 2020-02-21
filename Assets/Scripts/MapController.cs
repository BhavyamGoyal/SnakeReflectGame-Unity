using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : Singleton<MapController>
{
    public GameObject tilePrefab;
    public Sprite grassSprite;
    GameObject levelHolder;
    SpriteRenderer food;
    int width, height;
    List<int> map=new List<int>();
    List<Sprite> foodSprites;
    public  MapController() { }
    public MapController(GameObject tilePrefab,Sprite grassSprite,GameObject food,List<Sprite> foodSprites)
    {
        this.food = GameObject.Instantiate(food).GetComponent<SpriteRenderer>();
        this.foodSprites = foodSprites;
        this.food.gameObject.SetActive(false);
        this.tilePrefab = tilePrefab;
        this.grassSprite = grassSprite;
        SetInstance(this);
    }
    public void SpawnFood()
    {
        int randomTile = Random.Range(0, map.Count);
        int tile = map[randomTile];
        Vector3 spawnPoint=new Vector3(tile/height,tile%height,-.01f);
        int xOffset = (width / 2);
        int yOffset = (height / 2);
        spawnPoint.x = spawnPoint.x - xOffset;
        spawnPoint.y = spawnPoint.y - yOffset;
        food.transform.position = spawnPoint;
        food.gameObject.SetActive(true);
        int ftype = Random.Range(0,foodSprites.Count);
        food.sprite = foodSprites[ftype];
    }

    public void RemoveFood()
    {
        food.gameObject.SetActive(false);
    }
    public void GenerateMap(int width, int height)
    {
        this.width = width;
        this.height = height;
        levelHolder = new GameObject();
        levelHolder.name = "levelHolder";
        levelHolder.transform.position = Vector3.zero;
        for (int i = -width / 2; i <= width / 2; i++)
        {
            for (int j = -height / 2; j <= height / 2; j++)
            {
                GameObject.Instantiate(tilePrefab, new Vector2(i, j), Quaternion.identity, levelHolder.transform).name=map.Count.ToString();
                //Debug.Log(map.Count);
                map.Add(map.Count);
            }
        }
    }
    public void RemoveTile(GameObject tile)
    {
        tile.GetComponent<SpriteRenderer>().sprite = grassSprite;
        tile.GetComponent<BoxCollider2D>().enabled = false;
        int tileNum = int.Parse(tile.name);
        map.Remove(tileNum);
        if (map.Count == 0)
        {
            Time.timeScale = 0;
            UIManager.Instance.ShowGameOver();
        }
    }
}