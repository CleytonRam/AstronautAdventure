using Animation;
using DG.Tweening;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{

    public float startLife = 10f;
    public bool destroyOnKill = false;
    public float currentLife;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;
    public UIFillUpdate uiGunUpdater;

    private void Awake()
    {
        Init();
    }
    public void Init()
    {
        ResetLife();
    }

    public void ResetLife()
    {
        currentLife = startLife;
    }

    protected virtual void Kill()
    {
        if (destroyOnKill)
        {
            Destroy(gameObject, 3f);
        }
        OnKill?.Invoke(this);
    }

    [NaughtyAttributes.Button]
    public void Damage()
    {
        Damage(5);
    }


    public void Damage(float f)
    {
    
        currentLife -= f;
        if (currentLife <= 0)
        {
            Kill();
        }
        UpdateUi();
        OnDamage?.Invoke(this);

    }

    public void Damage(float damage, Vector3 dir)
    {
        Damage(damage);
    }

    private void UpdateUi()
    {
        if (uiGunUpdater != null)
        {
            uiGunUpdater.UpdateValue((float)currentLife / startLife);
        }
    }
}
