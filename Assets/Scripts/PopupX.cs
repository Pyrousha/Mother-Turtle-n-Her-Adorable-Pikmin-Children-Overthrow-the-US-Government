using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeauRoutine;
using UnityEditor;

public class PopupX : MonoBehaviour
{
    [SerializeField] private Transform parentObj;
    [SerializeField] private Vector2 maxSize;
    [SerializeField] private bool isBadClick;

    private Routine lerpRoutine = Routine.Null;

    private static float LerpDuration = 0.25f;

    private static Vector2 CamSize = new Vector2((16f/9f) * 10, 10);

    private Vector2 sprSize;

    private Vector3 startPos;

    public void SetXPos()
    {
        SpriteRenderer sprRend = parentObj.GetComponent<SpriteRenderer>();
        BoxCollider2D _collider = parentObj.GetComponent<BoxCollider2D>();

        _collider.offset = new Vector2(0, 0);
        sprSize = sprRend.sprite.rect.size / sprRend.sprite.pixelsPerUnit;

        if (isBadClick)
        {
            transform.localPosition = Vector3.zero;
            transform.localScale = new Vector3(sprSize.x, sprSize.y, 0.5f);
        }
        else
        {
            transform.localPosition = sprSize / 2.0f - new Vector2(0.35f, 0.35f);
            transform.localScale = new Vector3(0.75f, 0.75f, 1);
        }
    }

    public void AfterSpawned()
    {
        SpriteRenderer sprRend = parentObj.GetComponent<SpriteRenderer>();
        BoxCollider2D _collider = parentObj.GetComponent<BoxCollider2D>();

        _collider.offset = new Vector2(0, 0);
        sprSize = sprRend.sprite.rect.size / sprRend.sprite.pixelsPerUnit;
        _collider.size = sprSize;


        startPos = parentObj.localPosition;
        parentObj.localScale = Vector3.zero;

        Vector2 screenRange = (CamSize - sprSize)/2f;
        Vector3 newPos = new Vector3(Random.Range(-screenRange.x, screenRange.x), Random.Range(-screenRange.y, screenRange.y), parentObj.localPosition.z);

        lerpRoutine = Routine.Start(this, MoveToPos(maxSize, newPos));
    }

    private void OnMouseDown()
    {
        lerpRoutine.Stop();
        lerpRoutine = Routine.Start(this, MoveToPos(Vector3.zero, startPos, true));

        if (isBadClick)
            PopupSpawner.Instance.SpeedUp();
    }

    private IEnumerator MoveToPos(Vector3 _localScale, Vector3 _localPos, bool destroyAfter = false)
    {
        Vector3 startingPos = parentObj.localPosition;
        Vector3 startingScale = parentObj.localScale;

        yield return Tween.Float(0f, 1f, (t) => 
        {
            parentObj.localPosition = Vector3.Lerp(startingPos, _localPos, t);
            parentObj.localScale = Vector3.Lerp(startingScale, _localScale, t);
        }, LerpDuration);

        if (destroyAfter)
            Destroy(parentObj.gameObject);
    }

    private void OnDestroy()
    {
        PopupSpawner.Instance.RemoveFromList(gameObject);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(PopupX))]
class DecalMeshHelperEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("SetPos"))
            {
            ((PopupX) target).SetXPos();
            }
    }
}
#endif