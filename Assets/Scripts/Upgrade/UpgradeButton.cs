using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeButton", menuName = "Scriptable Objects/UpgradeButton")]
public class UpgradeButton : ScriptableObject
{
    public string upgradeTitle; //업그레이드 이름
    public int level; //현재 레벨
    public int maxLevel; //업그레이드 최대 레벨
    public int cost; //coin & 스킬 포인트

    public void UpgradeLevel()
    {
        //레벨, cost 올리기
        level++;
        cost += cost;
    }

}
