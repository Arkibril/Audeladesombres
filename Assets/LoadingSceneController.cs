using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    public string sceneToLoad; // Le nom de la sc�ne � charger
    public float delayInSeconds = 3f; // Le d�lai en secondes avant de charger la sc�ne

    private void Start()
    {
        // Appeler la fonction LoadSceneDelayed apr�s le d�lai sp�cifi�
        Invoke("LoadSceneDelayed", delayInSeconds);
    }

    public void LoadSceneDelayed()
    {
        // Charger la sc�ne apr�s le d�lai
        SceneManager.LoadScene(sceneToLoad);
    }
}
