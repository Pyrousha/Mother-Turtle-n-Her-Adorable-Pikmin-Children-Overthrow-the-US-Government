using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool beingDragged = false;

    [SerializeField] private bool isDraggable = true;
    [SerializeField] private bool startGreen = false;

    [SerializeField] private Transform minXY;
    [SerializeField] private Transform maxXY;

    private Vector3 offsetFromMouse;

    private Vector3 startingPos;

    private static float moveSpeed = 0.125f;

    private void Start()
    {
        startingPos = transform.position;

        if (startGreen)
            GetComponent<Animator>().SetTrigger("Green");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(Ball.Instance.Respawn());
    }

    void OnMouseDown()
    {
        if (!isDraggable)
            return;

        beingDragged = true;
        offsetFromMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        offsetFromMouse.z = 0;
    }

    private void Update()
    {
        if (!isDraggable)
            return;

        if (Input.GetMouseButtonUp(0))
            beingDragged = false;

        if (beingDragged)
        {
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - offsetFromMouse;
            newPos.z = 0;

            transform.position = newPos;

            if (minXY != null)
            {
                Vector3 currPos = transform.position;
                currPos.x = Mathf.Max(minXY.position.x, currPos.x);
                currPos.y = Mathf.Max(minXY.position.y, currPos.y);
                transform.position = currPos;
            }
            if (maxXY != null)
            {
                Vector3 currPos = transform.position;
                currPos.x = Mathf.Min(maxXY.position.x, currPos.x);
                currPos.y = Mathf.Min(maxXY.position.y, currPos.y);
                transform.position = currPos;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isDraggable)
            return;

        if (!beingDragged)
        {
            if ((transform.position - startingPos).sqrMagnitude > 0.01f)
                transform.position = Vector3.MoveTowards(transform.position, startingPos, moveSpeed);
            else
                transform.position = startingPos;

            if (minXY != null)
            {
                Vector3 currPos = transform.position;
                currPos.x = Mathf.Max(minXY.position.x, currPos.x);
                currPos.y = Mathf.Max(minXY.position.y, currPos.y);
                transform.position = currPos;
            }
            if (maxXY != null)
            {
                Vector3 currPos = transform.position;
                currPos.x = Mathf.Min(maxXY.position.x, currPos.x);
                currPos.y = Mathf.Min(maxXY.position.y, currPos.y);
                transform.position = currPos;
            }
        }
    }
}
