using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeButton", menuName = "Scriptable Objects/UpgradeButton")]
public class UpgradeButton : ScriptableObject
{
    public string upgradeName; //업그레이드 이름
    public int level; //현재 레벨
    public int maxLevel; //업그레이드 최대 레벨
    public int coinCost; //coin
    public int skillPointCost; //스킬포인트

    public void UpgradeLevel()
    {
        if(level < maxLevel)
        {
            level++;
            coinCost += coinCost;
        }
    }
    
}
