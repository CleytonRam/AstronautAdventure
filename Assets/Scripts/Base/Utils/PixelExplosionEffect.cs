using UnityEngine;
using System.Collections;

public class PixelExplosionEffect : MonoBehaviour
{
    public Sprite[] explosionFrames; // Array com os frames da animação
    public float frameRate = 10f;    // Velocidade da animação

    private SpriteRenderer spriteRenderer;
    private int currentFrame = 0;

    void Start()
    {
        Debug.Log("Iniciando efeito de explosão!");

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer não encontrado!");
            return;
        }

        if (explosionFrames == null || explosionFrames.Length == 0)
        {
            Debug.LogError(" Nenhum frame de explosão atribuído!");
            return;
        }

        Debug.Log($" Total de frames: {explosionFrames.Length}");
        StartCoroutine(AnimateExplosion());
    }

    IEnumerator AnimateExplosion()
    {
        Debug.Log("Iniciando animação...");

        // Anima através de todos os frames
        for (int i = 0; i < explosionFrames.Length; i++)
        {
            if (spriteRenderer != null && explosionFrames[i] != null)
            {
                spriteRenderer.sprite = explosionFrames[i];
                Debug.Log($"Frame {i + 1}/{explosionFrames.Length}");
            }
            else
            {
                Debug.LogError($" Problema no frame {i}");
            }

            yield return new WaitForSeconds(1f / frameRate);
        }

        Debug.Log("Animação concluída!");

        // Destroi o objeto após a animação terminar
        Destroy(gameObject);
    }

    // Método para verificar configuração
    [ContextMenu("Verificar Configuração")]
    public void CheckConfig()
    {
        Debug.Log(" Verificando configuração do efeito:");
        Debug.Log($" Frames: {explosionFrames?.Length ?? 0}");
        Debug.Log($" Frame Rate: {frameRate}");
        Debug.Log($" SpriteRenderer: {spriteRenderer != null}");

        if (explosionFrames != null)
        {
            for (int i = 0; i < explosionFrames.Length; i++)
            {
                Debug.Log($" Frame {i}: {explosionFrames[i]?.name ?? "NULL"}");
            }
        }
    }
}