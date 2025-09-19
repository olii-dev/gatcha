using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour
{
    [Header("Cookie Clicker")]
    public string gameSceneName = "Cookie Clicker";
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Load save first
            GameSaveManager.LoadGame();

            // Then switch to the main game scene
            SceneManager.LoadScene(gameSceneName);
        }
    }
}
