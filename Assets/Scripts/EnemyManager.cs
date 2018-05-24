using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;
    public Vector3 DebugSphere;
    NavMeshHit hit;
    GameObject Spawner;

    public int maxEnemiesToSpawn = 5;
    public int spawnedEnemies;
    float searchRadius = 150;


	

    public void EnemySpawner(RaycastHit rHit)
    {
        if(rHit.collider.tag == "Player")
        {

            // if max amout of enemies on map dont spawn more
            if(spawnedEnemies >= maxEnemiesToSpawn)
            {
                return;
            }

            else
            { 
                GameObject tgo = Instantiate(enemy, RandomSpawnerLoc(), Quaternion.identity);
                spawnedEnemies++;
            }

        }
    }
    public Vector3 RandomSpawnerLoc()
    {
        Vector3 temploc = transform.position + Random.insideUnitSphere * searchRadius;

        NavMesh.SamplePosition(temploc, out hit, searchRadius, 1);
        Vector3 RandomPos = hit.position;
       return RandomPos;
    }

    void PlayerDamage()
    {

    }
}
