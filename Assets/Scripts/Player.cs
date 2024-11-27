using UnityEngine;

public class Player
{
    public int Hp { get; private set; }
    public int MaxHp { get; private set; }
    public int Damage { get; private set; }
    public int Experience { get; private set; }
    public int Level{ get; private set; }   
    public int Coins { get; private set; }
    
    private int experienceForLevel;

    public Player(int hp, int damage, int experience, int coins)
    {
        MaxHp = hp;
        Hp = hp;
        Damage = damage;
        Coins = coins;
        Experience = experience;
        Level =1;
        //경험치 필요량
        experienceForLevel = CalculateExperienceNextLevel(Level);
    }

    public void PlayerDamage(int damage) 
    {
        Hp -= damage;
        if(Hp < 0)
        {
            Hp = 0;
        }
    }

    public void Heal(int hp) 
    { 
        Hp += hp;   
    }

    public void GetExperience(int exp)
    {
        Experience += exp; 
        //레벨업 조건 달성 시 레벨업
         while(Experience >= experienceForLevel)
         {
            LevelUp();
         }
         //exp ui 업데이트
         UIController.Instance.UpdateExpUI(Experience, experienceForLevel);
        UIController.Instance.UpdateLevelUI(Level);
    }

    private void LevelUp()
    {
        //현재 경험치 초기화
        Experience -= experienceForLevel;
        Level++;
        //레벨업 후 필요 경험치 늘리기
        experienceForLevel = CalculateExperienceNextLevel(Level);
        UIController.Instance.LevelUpButtonActive();
    }

    public bool IsAlive()
    {
        return Hp > 0;
    }

    public void GetCoins(int coins)
    {
        Coins += coins; 
        // UI 업데이트 호출
        UIController.Instance.UpdateCoinUI(Coins);
    }
    
    public int CalculateExperienceNextLevel(int level)
    {
        return level * 10;
    }
}
