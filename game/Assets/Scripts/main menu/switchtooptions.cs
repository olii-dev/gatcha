using UnityEngine;
using UnityEngine.SceneManagement;

public class Switchtooptions : MonoBehaviour
{
    [Header("Scene Names")]
    public string sceneToLoad;  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            // Check that a scene name is assigned
            if (!string.IsNullOrEmpty(sceneToLoad))
            {
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.LogWarning("Scene name not set in SceneSwitcher!");
            }
        }
    }
}
