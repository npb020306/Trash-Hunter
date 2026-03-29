using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaDePressao : MonoBehaviour
{
    public float speed = 2f;

    void Start()
    {
        // 📏 calcula automaticamente o tamanho da tela
        float height = Camera.main.orthographicSize * 2f;
        float width = height * Camera.main.aspect;

        // 🔥 ajusta o tamanho do objeto pra cobrir a tela inteira
        transform.localScale = new Vector3(width, 1f, 1f);
    }

    void Update()
    {
        // 🌊 sobe constantemente
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("O jogador ficou para trás!");
        }
    }
}