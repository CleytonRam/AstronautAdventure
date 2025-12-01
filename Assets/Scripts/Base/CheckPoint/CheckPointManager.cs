using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class CheckPointManager : Singleton<CheckPointManager>
{
    public int lastCheckPoint = 0;
    public List<CheckPointBase> checkPoints;

    [SerializeField] private Vector3 _fallbackPosition = new Vector3(0, 1, 0);

    // Adicione inicialização no Awake
    protected override void Awake()
    {
        base.Awake();
        InitializeCheckPoints();
    }

    private void InitializeCheckPoints()
    {
        // Garantir que a lista está limpa
        checkPoints.Clear();

        // Encontrar todos os checkpoints na cena
        CheckPointBase[] allCheckPoints = FindObjectsOfType<CheckPointBase>();
        checkPoints.AddRange(allCheckPoints);

        // Ordenar por chave (opcional)
        checkPoints.Sort((a, b) => a.key.CompareTo(b.key));
    }


    public bool HasCheckPoint()
    {
        return lastCheckPoint > 0;
    }

    public void SaveCheckPoint(int i)
    {
        if (lastCheckPoint < i)
        {
            lastCheckPoint = i;
            Debug.Log("Checkpoint saved: " + lastCheckPoint);
        }
    }


    public Vector3 GetPositionFromLastCheckPoint()
    {
        var checkpoint = checkPoints.Find(i => i.key == lastCheckPoint);

        if (checkpoint != null)
        {
            // Verifica se há um spawnPoint específico definido
            if (checkpoint.spawnPoint != null)
            {
                return checkpoint.spawnPoint.position;
            }
            else
            {
                // Fallback: posição do checkpoint + offset
                return checkpoint.transform.position + new Vector3(0, 0, 2f);
            }
        }
        else
        {
            Debug.LogError($"Checkpoint com key {lastCheckPoint} não encontrado!");
            return _fallbackPosition;
        }
    }
}
