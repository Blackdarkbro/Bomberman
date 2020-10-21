using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BanditLayerController : MonoBehaviour
{
    private bool isMovement;
    [SerializeField] private LayerMask raycastMask;
    [SerializeField] private LayerMask explosionMask;

    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject firePrefab;

    // Update is called once per frame
    void Update()
    {
        if (isMovement)
        {
            return;

        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MovePlayerTo(Vector2.left);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MovePlayerTo(Vector2.right);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MovePlayerTo(Vector2.up);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MovePlayerTo(Vector2.down);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DropBomb();
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var obj = RaycastFromCamera();
            if (obj != null && obj.CompareTag("Explosive"))
            {
                Destroy(obj);
                var colliders = Physics2D.OverlapCircleAll(obj.transform.position, 1f, explosionMask);
                foreach (var cldr in colliders)
                {
                    Destroy(cldr.gameObject);
                }
            }
        }
    }

    private void MovePlayerTo(Vector2 dir)
    {
        if (Raycast(dir))
        {
            return;
        }

        isMovement = true;
        var pos = (Vector2) transform.position + dir;
        transform.DOMove(pos, 0.5f).OnComplete(() =>
        {
            isMovement = false;
        });

    }

    private bool Raycast(Vector2 dir)
    {
        var hit = Physics2D.Raycast(transform.position, dir, 1f, raycastMask);
        return hit.collider != null;
    }

    private GameObject RaycastFromCamera()
    {
        var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1f);
        return hit.collider ? hit.collider.gameObject : null;
    }

    private void DropBomb()
    {
        var molotovBomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        //StartCoroutine(DOExplosion(molotovBomb));
    }

    // IEnumerator DOExplosion(GameObject molotovBomb)
    // {
    //     
    // }

}
