using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void LateUpdate()
    {
        Vector3 newPos = transform.position;
        newPos.x = target.position.x;
        transform.position = newPos;
    }
}
