using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Configuração de Spawn")]
    [SerializeField] private float startDelay = 2f;
    [SerializeField] private float repeatRate = 1.5f;

    [Header("Prefabricados Obstaculos")]
    [SerializeField] private List<GameObject> obstaclePrefab;

    [Header("Spawn Referencia")]
    [SerializeField] private Transform spawnPoint;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), startDelay, repeatRate);
    }

    void SpawnObstacle()
    {
        if(obstaclePrefab.Count != 0 && spawnPoint != null)
        {
            //Aleatório para escolher um prefabricado da lista
            int index = Random.Range(0, obstaclePrefab.Count);
            GameObject prefabEscolhido = obstaclePrefab[index];

            //Spawnar
            Instantiate(prefabEscolhido, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.Log("Não foi possível spawnar");
        }
    
    }
}
