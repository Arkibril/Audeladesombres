using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // Fonction appelée lorsque le bouton est cliqué ou tout autre événement déclencheur
    public void QuitGameFunction()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
