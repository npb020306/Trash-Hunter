using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Referências")]
    public Transform player; // 🔹 referência do barquinho (quem está jogando)

    public GameObject trashPrefab;     // prefab do lixo
    public GameObject obstaclePrefab;  // prefab do obstáculo

    [Header("Configurações")]
    public float spawnDistance = 10f;  // distância na frente do player onde os objetos vão aparecer
    public float spawnWidth = 6f;      // largura lateral onde podem spawnar (esquerda/direita)
    public float spawnInterval = 1.5f; // tempo entre cada spawn

    private float timer; // ⏳contador interno

    void Update()
    {
        // soma o tempo a cada frame
        timer += Time.deltaTime;

        // quando atingir o intervalo definido
        if (timer >= spawnInterval)
        {
            SpawnObjects(); // cria novos objetos
            timer = 0f;     // reseta o tempo
        }
    }

    void SpawnObjects()
    {
        // pega o tamanho da câmera
        float camHeight = Camera.main.orthographicSize;
        float camWidth = camHeight * Camera.main.aspect;

        // margem pra não nascer colado na borda
        float margem = 0.5f;

        // posição Y na frente do player
        float spawnY = player.position.y + spawnDistance;

        // posição X limitada à tela (AGORA CORRETO)
        float spawnX = Random.Range(-camWidth + margem, camWidth - margem);

        // posição final
        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0);

        // decide se vai spawnar lixo ou obstáculo
        int rand = Random.Range(0, 100);

        if (rand < 70)
        {
            // SPAWN DO LIXO
            GameObject trash = Instantiate(trashPrefab, spawnPos, Quaternion.identity);

            // conecta o player
            Trash trashScript = trash.GetComponent<Trash>();

            if (trashScript != null)
            {
                trashScript.player = player;
            }
        }
        else
        {
            // SPAWN DO OBSTÁCULO
            GameObject obstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);

            // conecta o player
            ObstacleMovement obstacleScript = obstacle.GetComponent<ObstacleMovement>();

            if (obstacleScript != null)
            {
                obstacleScript.player = player;
            }
        }
    }
}