using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject _enemyPrefab;
    
    private bool _stopspawning = false;
    public GameObject[] powerups; 

    public List<GameObject> all_spawned = new List<GameObject>();

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyRoutine()
    {
        // add random delay before spawning
        yield return new WaitForSeconds(6.0f );
        while (_stopspawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f,8f), 7, 0);
            GameObject newEnemy =  Instantiate(_enemyPrefab,transform.position+ posToSpawn, Quaternion.identity,transform);


            all_spawned.Add(newEnemy);

            // Calculate the number of fixed updates for 3 seconds and 8 seconds
            int minFixedUpdates = Mathf.CeilToInt(3f / 0.02f);
            int maxFixedUpdates = Mathf.CeilToInt(8f / 0.02f);

        // Generate a random number of fixed updates to wait
        int randomFixedUpdates = Random.Range(minFixedUpdates, maxFixedUpdates + 1);

        // Wait for the calculated number of fixed updates
        for (int i = 0; i < randomFixedUpdates; i++)
        {
            yield return new WaitForFixedUpdate();
        }
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        while (_stopspawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);

            int randomPowerUp = Random.Range(0, 3);
            GameObject new_powerUp =  Instantiate(powerups[randomPowerUp], transform.position + posToSpawn, Quaternion.identity);

            all_spawned.Add(new_powerUp);


            // Calculate the number of fixed updates for 3 seconds and 8 seconds
            int minFixedUpdates = Mathf.CeilToInt(4f / 0.02f);
            int maxFixedUpdates = Mathf.CeilToInt(8f / 0.02f);

            // Generate a random number of fixed updates to wait
            int randomFixedUpdates = Random.Range(minFixedUpdates, maxFixedUpdates + 1);

            // Wait for the calculated number of fixed updates
            for (int i = 0; i < randomFixedUpdates; i++)
            {
                yield return new WaitForFixedUpdate(); // WaitForSeconds(Random.Range(4, 8)  ); 
            }
        }    
    }

    public void OnPlayerDeath()
    {
        _stopspawning= true;
    }

    public void OnPlayerRebirth()
    {
        _stopspawning = false;
    }

    public void DestroyAll()
    {
        foreach (GameObject obj in all_spawned)
        {
            Destroy(obj);
        }
        // reset list 
        all_spawned = new List<GameObject>();
    }

}
