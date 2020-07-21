using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyShipPrefab;
    
    [SerializeField]
    private GameObject[] powerups;

    private GameManager _gameManager;
    

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

       
            StartCoroutine(EnemySpawnRoutine());
            StartCoroutine(PowerUpSpawnRoutine());
        
    }
    
    public void StartSpawn()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }

    IEnumerator EnemySpawnRoutine()
    {
       
            while (_gameManager.gameOver == false)
            {
               
                    Instantiate(enemyShipPrefab, new Vector3(Random.Range(-7f, 7f), 7, 0), Quaternion.identity);
                    yield return new WaitForSeconds(5.0f);
            }
        }
    
    
    IEnumerator PowerUpSpawnRoutine()
    {
        while (_gameManager.gameOver == false)
        {
          
            
                int randomPowerUp = Random.Range(0, 3);
                Instantiate(powerups[randomPowerUp], new Vector3(Random.Range(-7f, 7f), 7, 0), Quaternion.identity);
            
            yield return new WaitForSeconds(5.0f);
        }
    }

}
