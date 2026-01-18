using UnityEngine;
using System.Collections.Generic;

public class ZombieSpawner : MonoBehaviour
{
    public Transform target; //타겟
    public enum SpawnState
    {
        None, Spawn
    }
    public SpawnState spawnState = SpawnState.Spawn; //소환상태
    private float spawnTime; //스폰타임
    public float spawnInterval; //소환간격
    public List<Transform> spawnPos = new List<Transform>(); //소환장소
    public GameObject zombiePrefab; //좀비 프리팹
    private int spawnCount; //소환갯수

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnIntervalOn();
    }

    // Update is called once per frame
    void Update()
    {
        //스포너가 타겟을 추적한다
        transform.position = new Vector3(target.position.x, target.position.y, target.position.z);
        switch(spawnState)
        {
            case SpawnState.Spawn: //소환중 상태
                {
                    spawnTime += Time.deltaTime; //소환시간재생
                    //소환 시간이 소환 간격과 같거나 클 경우
                    if(spawnTime >= spawnInterval)
                    {
                        for (int i = 0; i < spawnCount; i++) 
                        {
                            CreateZombie(); //좀비소환
                        } 
                        SpawnIntervalOn(); //소환 간격 초기화
                        spawnTime = 0; //소환시간 초기화
                    }
                    break;
                }
        }
    }

    //소환 간격 랜덤 함수
    void SpawnIntervalOn()
    {
        spawnInterval = Random.Range(0.5f, 3.0f); //0.5초 부터 3초 사이의 무작위 수 추출
        spawnCount = Random.Range(10, 30); //10부터 30까지 사이의 무작위 수 추출
    }

    void CreateZombie()
    {
        int n = Random.Range(0, spawnPos.Count); //0번부터 스폰 포스의 무작위 수 추출 
        GameObject zombie = Instantiate(zombiePrefab, spawnPos[n].position, spawnPos[n].rotation);
        Destroy(zombie, 60.0f); //좀비를 60초 뒤에도 죽이지 않으면 자동으로 죽임
    }
}
