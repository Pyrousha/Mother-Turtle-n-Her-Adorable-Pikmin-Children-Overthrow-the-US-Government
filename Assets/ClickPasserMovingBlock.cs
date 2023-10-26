using UnityEngine;

public class ClickPasserMovingBlock : MonoBehaviour
{
    [SerializeField] private MovingBlock block;

    private void OnMouseDown()
    {
        block.OnMouseDown();
    }
}
