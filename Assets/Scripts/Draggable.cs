using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool beingDragged = false;

    [SerializeField] private Transform minXY;
    [SerializeField] private Transform maxXY;

    private Vector3 offsetFromMouse;

    private Vector3 startingPos;

    void OnMouseDown()
    {
        beingDragged = true;
        offsetFromMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.localPosition;
        offsetFromMouse.z = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
            beingDragged = false;

        if(beingDragged)
        {
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - offsetFromMouse;
            newPos.z = 0;

            transform.localPosition = newPos;

            if(minXY != null)
            {
                Vector3 currPos = transform.position;
                currPos.x = Mathf.Max(minXY.position.x, currPos.x);
                currPos.y = Mathf.Max(minXY.position.y, currPos.y);
                transform.position = currPos;
            }
            if(maxXY != null)
            {
                Vector3 currPos = transform.position;
                currPos.x = Mathf.Min(maxXY.position.x, currPos.x);
                currPos.y = Mathf.Min(maxXY.position.y, currPos.y);
                transform.position = currPos;
            }
        }
    }
}
