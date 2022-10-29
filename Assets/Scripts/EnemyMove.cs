using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    [SerializeField] private GameObject player;

    private float speed = 0.1f;

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed);
    }
}
