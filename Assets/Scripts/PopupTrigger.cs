using UnityEngine;

public class PopupTrigger : MonoBehaviour
{
    [SerializeField] private GameObject popupObj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (popupObj != null)
            PopupSpawner.Instance.SpawnPopup(popupObj);
        else
            PopupSpawner.Instance.SpawnPopups = true;
    }
}
