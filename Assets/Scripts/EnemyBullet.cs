using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Vector3 movementDirection; //huong dan bay ra
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        if(movementDirection == Vector3.zero) return;
        transform.position += movementDirection * Time.deltaTime;
    }
    public void SetMovementDirection(Vector3 direction)
    {
        movementDirection = direction;
    }
}
