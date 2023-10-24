using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Vector3 Pos { get; private set; }
    public static Vector2 Velocity { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Pos = collision.transform.position;
        Velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;

        gameObject.SetActive(false); 
    }
}
