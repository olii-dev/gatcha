using UnityEngine;

public class GachaPanel : MonoBehaviour
{
    public GachaReward[] rewards;       // Panel-specific rewards
    public Animator panelAnimator;      // Optional pull animation

    public void TriggerGacha()
    {
        // Play animation
        if (panelAnimator != null)
            panelAnimator.SetTrigger("Pull");

        if (rewards.Length == 0) return;

        // Weighted random selection
        float totalWeight = 0f;
        foreach (var r in rewards)
            totalWeight += r.dropChance;

        float randomPoint = Random.value * totalWeight;

        foreach (var r in rewards)
        {
            if (randomPoint < r.dropChance)
            {
                SpawnReward(r.asset, r.rarity);
                return;
            }
            else
            {
                randomPoint -= r.dropChance;
            }
        }
    }

    void SpawnReward(GameObject reward, Rarity rarity)
    {
        // Spawn reward (adjust position if needed)
        Instantiate(reward, transform.position + Vector3.up * 100, Quaternion.identity);
        Debug.Log(name + " Gacha Pull: " + reward.name + " [" + rarity + "]");
    }
}
