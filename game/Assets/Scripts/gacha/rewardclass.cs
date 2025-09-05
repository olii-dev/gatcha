using UnityEngine;

[System.Serializable]
public class GachaReward
{
    public GameObject asset;      // The reward object
    public Rarity rarity;         // Its rarity category
    [Range(0, 100)]
    public float dropChance;      // Weight for random selection
}
