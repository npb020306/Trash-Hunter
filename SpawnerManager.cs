using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public Transform player; // referência do barquinho

    public BoxCollider2D spawnArea; // área (quadrado invisível) onde os objetos aparecem

    public float spawnInterval = 2f; // tempo entre spawns
    public float spawnDistance = 6f; // distância na frente do player onde o spawnArea fica

    public GameObject trashPrefab; // prefab do lixo
    public GameObject obstaclePrefab; // prefab do obstáculo

    [Header("Limites")]
    public int maxTrash = 5; // quantidade máxima de lixo na cena
    public int maxObstacles = 3; // quantidade máxima de obstáculos na cena

    private List<GameObject> trashList = new List<GameObject>(); // lista de lixos ativos
    private List<GameObject> obstacleList = new List<GameObject>(); // lista de obstáculos ativos

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    void Update()
    {
        // FAZ O QUADRADO DE SPAWN ANDAR JUNTO COM O PLAYER
        if (player != null && spawnArea != null)
        {
            Vector3 newPosition = spawnArea.transform.position;

            // mantém o X do spawnArea igual ao do player (centralizado)
            newPosition.x = player.position.x;

            // coloca o spawnArea sempre na frente do player
            newPosition.y = player.position.y + spawnDistance;

            spawnArea.transform.position = newPosition;
        }
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            SpawnTrash();
            SpawnObstacle();

            CleanList(trashList);
            CleanList(obstacleList);
        }
    }

    // 🔹 Pega uma posição aleatória DENTRO do quadrado (spawnArea)
    Vector2 GetRandomPositionInBounds()
    {
        Bounds bounds = spawnArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector2(x, y);
    }

    void SpawnTrash()
    {
        if (trashPrefab == null || player == null) return;
        if (trashList.Count >= maxTrash) return;

        // agora o lixo também nasce dentro do quadrado (mais consistente)
        Vector2 pos = GetRandomPositionInBounds();

        GameObject trash = Instantiate(trashPrefab, pos, Quaternion.identity);
        trashList.Add(trash);
    }

    void SpawnObstacle()
    {
        if (obstaclePrefab == null || player == null) return;
        if (obstacleList.Count >= maxObstacles) return;

        // obstáculo também usa o mesmo sistema (padronizado)
        Vector2 pos = GetRandomPositionInBounds();

        GameObject obstacle = Instantiate(obstaclePrefab, pos, Quaternion.identity);
        obstacleList.Add(obstacle);
    }

    void CleanList(List<GameObject> list)
    {
        // remove objetos destruídos da lista
        list.RemoveAll(item => item == null);
    }
}