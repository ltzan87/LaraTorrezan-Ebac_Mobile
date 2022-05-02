using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollactableBase : MonoBehaviour
{  
    public string compareTag = "Player";

    public ParticleSystem particleSystemCoin;
    public float timeToHide = 3f;
    public GameObject graphicItem;

    protected Collider2D _collider;

    [Header("Sounds")]
    public AudioSource audioSource;

    private void Awake() {
        //if (particleSystem != null) particleSystem.transform.SetParent(null);
        //Init();
    }

    private void OnTriggerEnter(Collider collision) {
        if(collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    protected virtual void Init() {
        _collider = GetComponent<Collider2D>();
    }

    protected virtual void Collect() {
        if(graphicItem != null) graphicItem.SetActive(false);
        Invoke(nameof(HideObject), timeToHide);
        OnCollect();       
    }

    private void HideObject() {
        gameObject.SetActive(false);
    }

    protected virtual void OnCollect() {
        if (particleSystemCoin != null) 
        {
            particleSystemCoin.transform.SetParent(null);
            particleSystemCoin.Play();
        }

        if (audioSource != null) audioSource.Play();
    }
}