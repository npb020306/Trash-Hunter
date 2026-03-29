using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopMapa : MonoBehaviour
{
    public Transform player;
    public float tamanhoDoTile = 20f;

    void Update()
    {
        if (player == null) return;

        if (player.position.y > transform.position.y + tamanhoDoTile)
        {
            transform.position += new Vector3(0, tamanhoDoTile * 2, 0);
        }
    }
}