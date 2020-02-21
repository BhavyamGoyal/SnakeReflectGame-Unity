using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerConfig", menuName = "PlayerConfig", order = 1)]
public class PlayerConfig : ScriptableObject
{
    public GameObject playerPrefab;
    public float playerSpeed;
    public int playerRotationSpeed;
}