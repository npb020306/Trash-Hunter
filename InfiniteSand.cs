using UnityEngine;

public class InfiniteSandSide : MonoBehaviour
{
    public Transform player;       // referência ao player
    public float spriteHeight;     // altura da sprite
    private Vector3 startPos;      // posição inicial

 
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Debug.Log("Altura da sprite: " + sr.bounds.size.y);
    }

    void Update()
    {
        // se o player passar da sprite, move ela pra frente no eixo Y
        if (player.position.y - transform.position.y > spriteHeight)
        {
            transform.position += new Vector3(0, spriteHeight * 2f, 0);
        }
    }
}