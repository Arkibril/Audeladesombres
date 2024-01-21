using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public List<DragAndDrop> dragAndDropScripts; // Liste des scripts DragAndDrop
    public GameObject finish;

    private void Start()
    {
        CheckAllScripts();
    }


    private void LateUpdate()
    {
        CheckAllScripts();
    }
    public void CheckAllScripts()
    {
        bool allScriptsCorrect = true;

        foreach (DragAndDrop script in dragAndDropScripts)
        {
            if (!script.Finish)
            {
                allScriptsCorrect = false;
            }
        }

        if (allScriptsCorrect)
        {
            Debug.Log("All scripts are finished and correct. Your game is ready!");
            finish.SetActive(true);

        }
        else
        {
            Debug.LogError("Some scripts are not finished or have issues. Please check and fix them.");
            finish.SetActive(false);

        }
    }
}
