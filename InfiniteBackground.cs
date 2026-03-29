using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public Transform player; // referência do barco

    private float spriteHeight;

    void Start()
    {
        // pega automaticamente o tamanho da imagem
        spriteHeight = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        // quando o player passa da imagem
        if (player.position.y > transform.position.y + spriteHeight)
        {
            // move a imagem lá pra frente (em cima)
            transform.position += new Vector3(0, spriteHeight * 2f, 0);
        }
    }
}