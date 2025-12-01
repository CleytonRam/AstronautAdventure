using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ItemCollectableBase : MonoBehaviour
{


    public string compareTag = "Player";

    [SerializeField] protected int _amount = 1;

    public ParticleSystem particleSystem;
    public float timeToHide = 0.5f;
    public GameObject gaphicItem;

    [Header("Sounds")]
    public AudioSource audioSource;


    private void Awake()
    {
        if (particleSystem != null)
        {
            particleSystem.transform.SetParent(null);
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();



        }
    }

    protected virtual void Collect()
    {

        if (gaphicItem != null) { gaphicItem.SetActive(false); }
        Invoke("HideObject", timeToHide);
        OnCollect();
    }

    private void HideObject()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnCollect()
    {

        if (particleSystem != null)
        {
            particleSystem.Play();
        }

        if (audioSource != null)
        {
            audioSource.Play();
        }


    }


}