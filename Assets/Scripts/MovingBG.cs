using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBG : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private void Start()
    {
        Time.timeScale = 0.5f;
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(moveSpeed, 0, 0);
    }
}
