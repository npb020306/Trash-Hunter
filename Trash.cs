using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public Transform player;
    public float destroyDistance = 8f;

    void Update()
    {
        // se ficou muito abaixo do player → perdeu
        if (transform.position.y < player.position.y - destroyDistance)
        {
            GameManager.Instance.AddScore(-2); // perde ponto
            Destroy(gameObject);
        }
    }
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
}