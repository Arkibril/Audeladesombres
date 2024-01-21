using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    public string sceneToLoad; // Le nom de la scène à charger
    public float delayInSeconds = 3f; // Le délai en secondes avant de charger la scène

    private void Start()
    {
        // Appeler la fonction LoadSceneDelayed après le délai spécifié
        Invoke("LoadSceneDelayed", delayInSeconds);
    }

    public void LoadSceneDelayed()
    {
        // Charger la scène après le délai
        SceneManager.LoadScene(sceneToLoad);
    }
}
