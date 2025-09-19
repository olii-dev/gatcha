using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public static int gold = 0; 
    public TMP_Text goldText;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gold = gold + 1;
        }
        goldText.SetText("Gold: " + gold);   

    }
}
