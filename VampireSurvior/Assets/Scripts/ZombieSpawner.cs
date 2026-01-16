using UnityEngine;
using System.Collections.Generic;

public class ZombieSpawner : MonoBehaviour
{
    public enum SpawnState
    {
        None, Spawn
    }
    public SpawnState spawnState = SpawnState.None; //소환상태
    public List<Transform> spawnPos = new List<Transform>(); //소환장소
    public GameObject zombiePrefab; //좀비 프리팹

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
