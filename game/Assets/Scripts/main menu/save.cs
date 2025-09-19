using UnityEngine;

public class OptionsSceneController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameSaveManager.SaveGame();
        }
    }
}
