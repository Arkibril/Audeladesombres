using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private RectTransform canvasRectTransform;
    private RectTransform blockRectTransform;
    private Canvas canvas;

    private GameObject currentObject;
    public GameObject enfant;

    public GameObject manSpecificObject;
    public GameObject iceCreamVanSpecificObject;
    public GameObject fil;

    private Vector2 originalLocalPointerPosition;
    private Vector3 originalLocalPosition;

    public bool canContainObjects = false;

    [Header("Combination Settings")]
    public bool editorEnfantCombination = false;
    public bool editorManCombination = false;
    public bool editorVanCombination = false;

    [Header("Runtime Combination State")]
    public bool runtimeEnfantActivated = false; // Boolean pour v�rifier si "enfant" est activ� pendant le jeu
    public bool runtimeManActivated = false; // Boolean pour v�rifier si "man" est activ� pendant le jeu
    public bool runtimeVanActivated = false; // Boolean pour v�rifier si "van" est activ� pendant le jeu


    public bool islast;
    public bool Finish = false;
    public GameManager gameManager;


    public DragAndDrop dragAndDrop;

    public GameObject Cinematique;


    public void SetFinishState(bool state)
    {
        Finish = state;
        gameManager.CheckAllScripts(); // Appelez la fonction pour v�rifier l'�tat de tous les scripts
    }
    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        canvasRectTransform = canvas.GetComponent<RectTransform>();
        blockRectTransform = GetComponent<RectTransform>();
    }

    // Ajoutez l'objet au bloc
    public void AddObjectToBlock(GameObject obj)
    {
        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;
        currentObject = obj;

        UpdateRuntimeCombinationState();
    }

    // Retire l'objet du bloc
    public void RemoveObjectFromBlock()
    {
        if (currentObject != null)
        {
            currentObject.transform.SetParent(null);
            currentObject = null;

            ResetRuntimeCombinationState();
        }
    }

    // Fonction pour activer quelque chose en fonction du tag de l'objet
    public void ActivateEnfant(string objectTag)
    {
        // Personnalisez cette fonction en fonction de ce que vous souhaitez activer en fonction du tag de l'objet
        Debug.Log("Object with tag " + objectTag + " dropped on the block. Activating something in the child...");

        // V�rifiez le tag de l'objet pour activer le bon �l�ment
        if (objectTag == "Enfant")
        {
            runtimeEnfantActivated = true;
            enfant.SetActive(true);
        }
        else if (objectTag == "Man")
        {
            runtimeManActivated = true;
            ActivateMan();
        }
        else if (objectTag == "IceCreamVan")
        {
            runtimeVanActivated = true;
            ActivateIceCreamVan();
        }

        UpdateRuntimeCombinationState();
    }

    // Fonction pour activer quelque chose sp�cifique pour "Man"
    private void ActivateMan()
    {
        if (manSpecificObject != null)
        {
            manSpecificObject.SetActive(true);
        }
    }


    // Mettre � jour l'�tat de la combinaison pendant le jeu
    private void UpdateRuntimeCombinationState()
    {
        // V�rifiez si les �l�ments n�cessaires pour chaque combinaison sont activ�s pendant le jeu
        bool runtimeEnfantCombination = runtimeEnfantActivated && runtimeManActivated;
        bool runtimeManCombination = runtimeEnfantActivated && runtimeVanActivated;
        bool runtimeVanCombination = runtimeVanActivated;

        CheckCombination(runtimeEnfantCombination, runtimeManCombination, runtimeVanCombination);
    }

    // R�initialiser l'�tat de la combinaison lorsqu'un objet est retir� du bloc
    private void ResetRuntimeCombinationState()
    {
        runtimeEnfantActivated = false;
        runtimeManActivated = false;
        runtimeVanActivated = false;

        CheckCombination(false, false, false);
    }

    private void Update()
    {
        if (!Finish)
        {
            fil.SetActive(false);
        }
}

    private void ActivateIceCreamVan()
    {
        if (iceCreamVanSpecificObject != null)
        {
            iceCreamVanSpecificObject.SetActive(true);

        }
    }
    // Fonction pour v�rifier les combinaisons
    public void CheckCombination(bool runtimeEnfantCombination, bool runtimeManCombination, bool runtimeVanCombination)
    {
        if (!editorManCombination && !editorVanCombination && editorEnfantCombination && runtimeEnfantActivated && !runtimeVanActivated)
        {
            Debug.Log("Enfant combination achieved!");
            //dragAndDrop.enabled = false;
            fil.SetActive(true);
            Finish = true;
        }
        else if (editorManCombination && !editorEnfantCombination && !editorVanCombination && runtimeManActivated && !runtimeEnfantActivated && !runtimeVanActivated)
        {
            Debug.Log("Man combination achieved!");
            //dragAndDrop.enabled = false;
            fil.SetActive(true);
            Finish = true;

        }
        else if (!editorManCombination && !editorEnfantCombination && editorVanCombination && runtimeVanActivated && !runtimeManActivated && !runtimeEnfantActivated)
        {
            Debug.Log("Van combination achieved!");
            //dragAndDrop.enabled = false;
            fil.SetActive(true);
            Finish = true;


        }
        else if (editorEnfantCombination && editorManCombination && runtimeEnfantActivated && runtimeManActivated && !runtimeVanActivated)
        {
            Debug.Log("Enfant and Man combination achieved!");
            //dragAndDrop.enabled = false;
            fil.SetActive(true);
            Finish = true;

        }
        else if (editorEnfantCombination && !editorManCombination && editorVanCombination && runtimeEnfantActivated && !runtimeManActivated && runtimeVanActivated)
        {
            Debug.Log("Enfant and Van combination achieved!");
            //dragAndDrop.enabled = false;
            fil.SetActive(true);
            Finish = true;

        }
        else if ( !editorEnfantCombination && editorManCombination && editorVanCombination && !runtimeEnfantActivated && runtimeManActivated && runtimeVanActivated)
        {
            Debug.Log("Man and Van combination achieved!");
            //dragAndDrop.enabled = false;
            fil.SetActive(true);
            Finish = true;

        }
        else if (editorEnfantCombination && editorManCombination && editorVanCombination && runtimeEnfantActivated && runtimeManActivated && runtimeVanActivated)
        {
            Debug.Log("Enfant, Man, and Van combination achieved!");
            //dragAndDrop.enabled = false;
            fil.SetActive(true);
            Finish = true;

        }
        else
        {
            Debug.Log("Incorrect combination!");
            fil.SetActive(false);
            Finish = false;
        }
    }


    public void ClickVan()
    {
        if (runtimeVanActivated)
        {
            runtimeVanActivated = false; // Si besoin de d�sactiver le bool�en �galement
            UpdateRuntimeCombinationState();
            CheckCombination(false, false, false);
        }
    }

    public void ClickEnfant()
    {
        if (runtimeEnfantActivated)
        {
            runtimeVanActivated = false; // Si besoin de d�sactiver le bool�en �galement
            UpdateRuntimeCombinationState();
            CheckCombination(false, false, false);
        }
    }

    public void ClickMan()
    {
        if (runtimeManActivated)
        {
            runtimeManActivated = false; // Si besoin de d�sactiver le bool�en �galement
            UpdateRuntimeCombinationState();
            CheckCombination(false, false, false);
        }
    }

    public void setAllToFalse()
    {
        runtimeEnfantActivated = false;
        runtimeVanActivated = false; 
        runtimeManActivated = false;
        Finish = false;
    }

}
