using UnityEngine;

public class ClickPasser : MonoBehaviour
{
    [SerializeField] private TempBlock block;

    private void OnMouseDown()
    {
        block.OnMouseDown();
    }
}
