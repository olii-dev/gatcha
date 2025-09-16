using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance; // static reference to itself

    private void Awake()
    {
        // Ensure only one MainManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // keep it across scenes
        }
        else
        {
            Destroy(gameObject); // kill duplicates if another scene already has one
        }
    }
}
