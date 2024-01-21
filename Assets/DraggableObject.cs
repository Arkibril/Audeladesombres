using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform canvasRectTransform;
    private RectTransform objectRectTransform;
    private Canvas canvas;

    private GameObject currentBlock;

    private Vector2 originalLocalPointerPosition;
    private Vector3 originalLocalPosition;

    public bool canBeDropped = true;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        canvasRectTransform = canvas.GetComponent<RectTransform>();
        objectRectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!canBeDropped) return;

        originalLocalPosition = objectRectTransform.localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, eventData.position, eventData.pressEventCamera, out originalLocalPointerPosition);

        // Si l'objet était dans un bloc, libère cet objet du bloc
        if (currentBlock != null)
        {
            currentBlock.GetComponent<DragAndDrop>().RemoveObjectFromBlock();
            currentBlock = null;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!canBeDropped) return;

        Vector2 localPointerPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, eventData.position, eventData.pressEventCamera, out localPointerPosition);

        objectRectTransform.localPosition = originalLocalPosition + (Vector3)localPointerPosition - new Vector3(originalLocalPointerPosition.x, originalLocalPointerPosition.y, 0f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!canBeDropped) return;

        // Trouver les blocs sous l'objet
        Collider2D[] colliders = Physics2D.OverlapBoxAll(objectRectTransform.position, objectRectTransform.sizeDelta / 2f, 0f);

        foreach (var collider in colliders)
        {
            GameObject hitObject = collider.gameObject;

            // Vérifier si l'objet est un bloc
            DragAndDrop blockScript = hitObject.GetComponent<DragAndDrop>();
            if (blockScript != null && blockScript.canContainObjects)
            {
                blockScript.ActivateEnfant(objectTag: gameObject.tag); // Activer quelque chose dans le bloc
                currentBlock = hitObject;

                // Désactiver la possibilité de glisser à nouveau dans tous les autres blocs
                foreach (var otherCollider in colliders)
                {
                    GameObject otherHitObject = otherCollider.gameObject;
                    if (otherHitObject != currentBlock)
                    {
                        DraggableObject otherBlockScript = otherHitObject.GetComponent<DraggableObject>();
                        if (otherBlockScript != null)
                        {
                            otherBlockScript.canBeDropped = false;
                        }
                    }
                }

                // Ramener l'objet à sa position initiale
                objectRectTransform.localPosition = originalLocalPosition;

                return;
            }
        }

        // Si l'objet n'est pas relâché sur un bloc, le ramener à sa position initiale
        objectRectTransform.localPosition = originalLocalPosition;
    }
}
