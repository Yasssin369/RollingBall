using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    float _nextSpawnTime;
    [SerializeField] float _delay = 5f;
    [SerializeField] Transform[] _spawnPoints;
    [SerializeField] Obstacles[] _obstacles;
    [SerializeField] Collectible _collectibles;
    public float clampDistanceX;
    void Update()
    {
        transform.position = new Vector3(0,transform.position.y,transform.position.z);
        if (ShouldSpawn())
            Spawn();
    }

    void Spawn()
    {
        _nextSpawnTime = Time.time + _delay;
        Transform spawnpoint = ChooseSpawner();
        
        Obstacles obstacle = ChooseEnemy();
        Instantiate(obstacle, spawnpoint.position, spawnpoint.rotation);
        spawnpoint = ChooseSpawner();
        Instantiate(_collectibles, spawnpoint.position, spawnpoint.rotation);
    }

    Obstacles ChooseEnemy()
    {
        int randomindex = UnityEngine.Random.Range(0, _obstacles.Length);
        var enemy = _obstacles[randomindex];
        return enemy;
    }

    Transform ChooseSpawner()
    {
        int randomindex = UnityEngine.Random.Range(0, _spawnPoints.Length);
        var spawnpoint = _spawnPoints[randomindex];
        return spawnpoint;
    }

    bool ShouldSpawn()
    {
        return Time.time >= _nextSpawnTime;
    }
}