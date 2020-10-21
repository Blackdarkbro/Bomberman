using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private LayerMask nonDestr;
    [SerializeField] private GameObject explosionPrefab;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(CreateExplosion(Vector3.left));
        StartCoroutine(CreateExplosion(Vector3.right));
        StartCoroutine(CreateExplosion(Vector3.up));
        StartCoroutine(CreateExplosion(Vector3.down));
        Destroy(gameObject);
    }
    
    IEnumerator CreateExplosion(Vector3 direction)
    {
        for (int i = 1; i < 3; i++)
        {
            GameObject explosion;
            var hit = Physics2D.Raycast(transform.position + new Vector3(0, .5f, 0), direction, i, nonDestr);
            if (!hit.collider)
            {
               explosion = Instantiate(explosionPrefab, transform.position + (i * direction), Quaternion.identity);
            }
            else
            {
                break;
            }
            
            yield return new WaitForSeconds(.5f);
        }
    }
}
