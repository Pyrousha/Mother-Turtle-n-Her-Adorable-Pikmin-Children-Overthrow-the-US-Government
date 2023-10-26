using BeauRoutine;
using System.Collections;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    [SerializeField] private Transform blockObj;

    [SerializeField] private MovingBlock linkedBlock;

    private Animator blockAnim;

    private bool activated = false;

    [SerializeField] private bool startActivated;

    private Routine routine;

    private Quaternion startRot;
    private Vector3 startPos;

    private void Start()
    {
        blockAnim = blockObj.GetComponent<Animator>();

        Ball.Instance.OnRespawn += OnRespawn;

        startPos = blockObj.position;
        startRot = blockObj.rotation;

        if (startActivated)
            ToggleSelf();
    }

    private void OnRespawn()
    {
        activated = true;

        if (startActivated)
            activated = false;

        ToggleSelf();
    }

    public void OnMouseDown()
    {
        routine.Stop();

        activated = !activated;

        if (activated)
            routine = Routine.Start(this, MoveRoutine(transform.position, Quaternion.identity));
        else
            routine = Routine.Start(this, MoveRoutine(startPos, startRot));

        if (linkedBlock != null)
            linkedBlock.ToggleSelf();
    }

    public void ToggleSelf()
    {
        routine.Stop();

        activated = !activated;

        if (activated)
            routine = Routine.Start(this, MoveRoutine(transform.position, Quaternion.identity));
        else
            routine = Routine.Start(this, MoveRoutine(startPos, startRot));
    }

    private IEnumerator MoveRoutine(Vector3 _newPos, Quaternion _newRot)
    {
        if (!activated)
            blockAnim.SetTrigger("Red");

        Vector3 currPos = blockObj.position;
        Quaternion currRot = blockObj.rotation;


        yield return Tween.Float(0f, 1f, (t) =>
        {
            blockObj.SetPositionAndRotation(Vector3.Lerp(currPos, _newPos, t), Quaternion.Lerp(currRot, _newRot, t));
        }, 0.25f);

        if (activated)
            blockAnim.SetTrigger("Green");
    }
}
