using UnityEngine;
using TMPro;

public class GoldDisplay : MonoBehaviour
{
    public TMP_Text goldText;  
    void Update()
    {
        if (goldText != null)
        {
            goldText.SetText("Gold: " + NewMonoBehaviourScript.gold);
        }
    }
}
