using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Singleton<Ball>
{
    private Rigidbody2D rb;
    [SerializeField] private Vector2 startingVelocity;

    private bool moving;
    private bool respawning;

    [SerializeField] private GameObject clickText;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDown()
    {
        if(!moving)
        {
            moving = true;
            rb.gravityScale = 1;
            rb.velocity = startingVelocity;

            respawning = false;

            clickText.SetActive(false);
        }
    }

    public IEnumerator Respawn()
    {
        if (respawning)
            yield break;

        respawning = true;

        RespawnAnimation.Instance.Anim.SetTrigger("FadeToBlack");

        yield return new WaitForSeconds(0.5f);

        transform.position = Checkpoint.Pos;
        startingVelocity = Checkpoint.Velocity;

        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;

        PopupSpawner.Instance.RemoveAllPopups();

        yield return new WaitForSeconds(0.25f);
        moving = false;
        clickText.SetActive(true);
    }
}
