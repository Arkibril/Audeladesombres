using UnityEngine;
using System.Collections.Generic;

public class ResetButton : MonoBehaviour
{
    public List<GameObject> parentObjectsToDisableChildren;
    public List<GameObject> objectsToUpdateRuntime;

    // Fonction appelée lorsque le bouton est cliqué
    public void ResetChildrenAndRuntime()
    {
        // Désactiver les enfants de plusieurs objets parents
        foreach (GameObject parentObject in parentObjectsToDisableChildren)
        {
            if (parentObject != null)
            {
                // Désactiver tous les objets enfants
                foreach (Transform child in parentObject.transform)
                {
                    child.gameObject.SetActive(false);
                }
            }
            else
            {
                Debug.LogError("Veuillez assigner tous les objets parents à désactiver dans l'inspecteur Unity.");
            }
        }

        // Mettre à jour les booléens runtime de plusieurs objets
        foreach (GameObject objectToUpdate in objectsToUpdateRuntime)
        {          

           DragAndDrop dragScript = objectToUpdate.GetComponent<DragAndDrop>();

            dragScript.setAllToFalse();          
        }
    }
}
