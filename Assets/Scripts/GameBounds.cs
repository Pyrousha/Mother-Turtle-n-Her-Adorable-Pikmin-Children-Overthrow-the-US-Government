using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBounds : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(Ball.Instance != null)
            StartCoroutine(Ball.Instance.Respawn());
    }
}
