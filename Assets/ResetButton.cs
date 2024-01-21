using UnityEngine;
using System.Collections.Generic;

public class ResetButton : MonoBehaviour
{
    public List<GameObject> parentObjectsToDisableChildren;
    public List<GameObject> objectsToUpdateRuntime;

    // Fonction appel�e lorsque le bouton est cliqu�
    public void ResetChildrenAndRuntime()
    {
        // D�sactiver les enfants de plusieurs objets parents
        foreach (GameObject parentObject in parentObjectsToDisableChildren)
        {
            if (parentObject != null)
            {
                // D�sactiver tous les objets enfants
                foreach (Transform child in parentObject.transform)
                {
                    child.gameObject.SetActive(false);
                }
            }
            else
            {
                Debug.LogError("Veuillez assigner tous les objets parents � d�sactiver dans l'inspecteur Unity.");
            }
        }

        // Mettre � jour les bool�ens runtime de plusieurs objets
        foreach (GameObject objectToUpdate in objectsToUpdateRuntime)
        {          

           DragAndDrop dragScript = objectToUpdate.GetComponent<DragAndDrop>();

            dragScript.setAllToFalse();          
        }
    }
}
