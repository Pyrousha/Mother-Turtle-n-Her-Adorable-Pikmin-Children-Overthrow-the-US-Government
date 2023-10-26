using BeauRoutine;
using System.Collections;
using UnityEngine;

public class TempBlock : MonoBehaviour
{
    [SerializeField] private Transform blockObj;

    private bool activated = false;

    private Routine routine;

    private void Start()
    {
        Ball.Instance.OnRespawn += OnRespawn;
    }

    private void OnRespawn()
    {
        activated = true;
        OnMouseDown();
    }

    public void OnMouseDown()
    {
        routine.Stop();

        activated = !activated;

        if (activated)
            routine = Routine.Start(this, MoveRoutine(Vector3.one));
        else
            routine = Routine.Start(this, MoveRoutine(Vector3.zero));
    }

    private IEnumerator MoveRoutine(Vector3 _newLocalScale)
    {
        Vector3 startingScale = blockObj.localScale;

        yield return Tween.Float(0f, 1f, (t) =>
        {
            blockObj.localScale = Vector3.Lerp(startingScale, _newLocalScale, t);
        }, 0.25f);
    }
}
