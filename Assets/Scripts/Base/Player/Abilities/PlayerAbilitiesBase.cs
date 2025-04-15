using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilitiesBase : MonoBehaviour
{
    protected Player player;

    protected Inputs inputs;

    private void OnValidate()
    {
        if (player == null)
        {
            player = GetComponent<Player>();
        }
    }

    private void Start()
    {
        inputs = new Inputs();
        inputs.Enable();

        Init();
        OnValidate();
        RegisterListeners();
    }

    private void OnEnable()
    {
        if (inputs != null)
        {
            inputs.Enable();
        }

    }

    private void OnDisable()
    {
        if (inputs != null)
        {
            inputs.Disable();
        }
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    protected virtual void Init() { }
    protected virtual void RegisterListeners() { }
    protected virtual void RemoveListeners() { }

}
