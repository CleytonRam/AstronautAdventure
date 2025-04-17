using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    [Header("Setup")]
    public Color color = Color.red;
    public float duration = 0.1f;

    private Color _defaultColor;
    private Tween _currentTween;


    public void Start()
    {
        if (!_currentTween.IsActive())
        {
            _defaultColor = meshRenderer.material.GetColor("_EmissionColor");
        }
    }

    [NaughtyAttributes.Button("Flash")]
    public void Flash()
    {
        meshRenderer.material.DOColor(color, "_EmissionColor", duration).SetLoops(2, LoopType.Yoyo);          
    }
}
