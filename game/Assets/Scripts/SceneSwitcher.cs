using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [Header("Scene Names")]
    public string sceneToLoad;  // Name of the scene you want to switch to

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
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
