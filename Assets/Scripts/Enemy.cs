using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject deathFX;
    [SerializeField] private Transform parent;
    [SerializeField] private int scorePerHit = 12; 
    [SerializeField] private int hits = 10; 

    private ScoreBoard scoreBoard;
    // Start is called before the first frame update
    void Start()
    {
        AddNonTriggerBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddNonTriggerBoxCollider()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (hits <= 0)
        {
            KillEnemy();
        }
    }

    void ProcessHit()
    {
        scoreBoard.ScoreHit(scorePerHit);
        hits--;
        //todo consider hit FX
    }
    private void KillEnemy()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        Destroy(gameObject);
    }
}
