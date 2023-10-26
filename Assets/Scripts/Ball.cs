using System.Collections;
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

    public event System.Action OnRespawn;
    public event System.Action OnStartMove;

    private void OnMouseDown()
    {
        if (!moving)
        {
            moving = true;
            rb.gravityScale = 1;
            rb.velocity = startingVelocity;

            respawning = false;

            clickText.SetActive(false);
            OnStartMove?.Invoke();
        }
    }

    public IEnumerator Respawn()
    {
        if (respawning)
            yield break;

        AudioManager.Instance.Play(AudioType.EXPLODE);

        respawning = true;

        RespawnAnimation.Instance.Anim.SetTrigger("FadeToBlack");

        yield return new WaitForSeconds(0.5f);

        transform.position = Checkpoint.Pos;
        startingVelocity = Checkpoint.Velocity;

        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;

        OnRespawn?.Invoke();

        PopupSpawner.Instance.RemoveAllPopups();

        yield return new WaitForSeconds(0.25f);
        moving = false;
        clickText.SetActive(true);
    }
}
