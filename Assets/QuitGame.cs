using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // Fonction appel�e lorsque le bouton est cliqu� ou tout autre �v�nement d�clencheur
    public void QuitGameFunction()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
