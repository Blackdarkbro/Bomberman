using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private Vector3 startPos;
    void Start()
    {
        startPos = player.transform.position;
    }

    private void OnEnable()
    {
        StartCoroutine(DestroyExplosion());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            Instantiate(player, startPos, Quaternion.identity);
        }
    }

    IEnumerator DestroyExplosion()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}
