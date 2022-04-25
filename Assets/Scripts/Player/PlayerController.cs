using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 1f;

    public float speed = 1f;
    public string tagCheckEnemy = "Enemy";
    public string tagCheckEndLine = "Finish";
    public GameObject endScene;

    private Vector3 _pos;
    private bool _canRun;


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
            EndGame();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag == tagCheckEndLine)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        _canRun = false;

        endScene.SetActive(true);
    }

    public void StartToRun()
    {
        _canRun = true;
    }
}