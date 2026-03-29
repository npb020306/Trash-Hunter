using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public Transform player;
    public float destroyDistance = 8f;

    void Start()
    {
        // se não recebeu o player, procura automaticamente
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");

            if (p != null)
            {
                player = p.transform;
            }
        }
    }
}