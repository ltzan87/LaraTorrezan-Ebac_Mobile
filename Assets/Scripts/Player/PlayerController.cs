using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ebac.Core.Singleton;
using DG.Tweening;

public class PlayerController : Singleton<PlayerController>
{
    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 1f;

    public float speed = 1f;
    public string tagCheckEnemy = "Enemy";
    public string tagCheckEndLine = "Finish";
    public GameObject endScene;

    public bool invencible = true;

    [Header("Text")]
    public TextMeshPro uiTextPowerUp;

    [Header("Coin Setup")]
    public GameObject coinCollector;

    [Header("Animation")]
    public AnimatorManager animatorManager;

    private Vector3 _pos;
    private bool _canRun;
    private float _currentSpeed;
    private Vector3 _startPosition;

    private void Start() {
        _startPosition = transform.position;
        ResetSpeed();
    }

    void Update()
    {
        if (!_canRun) return;


        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.transform.tag == tagCheckEnemy)
        {
            MoveBack(collision.transform);
            if(!invencible) EndGame(AnimatorManager.AnimationType.DEATH);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag == tagCheckEndLine)
        {
            if(!invencible) EndGame();
        }
    }

        
    public void EndGame(AnimatorManager.AnimationType animationType = AnimatorManager.AnimationType.INDLE)
    {
        _canRun = false;

        endScene.SetActive(true);
        
        animatorManager.Play(animationType);
    }

    private void MoveBack(Transform t)
    {
        transform.DOMoveZ(1f, .3f).SetRelative();
    }

    public void StartToRun()
    {
        _canRun = true;
        animatorManager.Play(AnimatorManager.AnimationType.RUN);
    }

    #region PU
    public void SetPowerUpText(string s)
    {
        uiTextPowerUp.text = s;
    }

    public void SetPowerUpSpeedUp(float f)
    {
        _currentSpeed = f;
    }

    public void SetInvencible(bool b = true)
    {
        invencible = b;
    }

    public void ResetSpeed()
    {
        _currentSpeed = speed;
    }

    public void ChangeHeight(float amount, float duration, float animationDuration, Ease ease)
    {
        /*var p = transform.position;
        p.y = _startPosition.y + amount;
        transform.position = p;*/

        transform.DOMoveY(_startPosition.y + amount, animationDuration).SetEase(ease);//OnComplete(ResetHeight);
        Invoke(nameof(ResetHeight), duration);
    }

    public void ResetHeight()
    {
        /*var p = transform.position;
        p.y = _startPosition.y;
        transform.position = p;*/

        transform.DOMoveY(_startPosition.y, .1f);
    }

    public void ChangeCoinCollectorSize(float amount)
    {
        coinCollector.transform.localScale = Vector3.one * amount;
    }
    #endregion
}