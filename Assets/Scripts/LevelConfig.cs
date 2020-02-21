using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LevelConfig", menuName = "LevelConfig", order = 1)]
public class LevelConfig : ScriptableObject
{
    public int width = 10, height = 10;
    public GameObject tilePrefab;
    public Sprite grassSprite;
    public GameObject food;
    public int foodTimer;
    public GameObject edges;
    public PlayerConfig player;
    public List<Sprite> foodSprites;
}