using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropChild : MonoBehaviour, IPointerClickHandler
{
    private void Start()
    {
        // Assurez-vous que le composant Collider2D est pr�sent
        if (GetComponent<Collider2D>() == null)
        {
            Debug.LogError("DragAndDropChild script requires a Collider2D component.");
            Destroy(this); // D�sactivez le script si le composant Collider2D est manquant
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Child clicked!");
        gameObject.SetActive(false);
    }
}
