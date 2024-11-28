using UnityEngine;

public class Player
{
    public int Hp { get; private set; }
    public int MaxHp { get; private set; }
    public int Experience { get; private set; }
    public int Level { get; private set; }
    public int Coins { get; private set; }
    public int SkillPoints { get; private set; }

    public float Damage { get; set; }
    public int AttackSpeed { get; private set; }


    private int experienceForLevel;

    public Player(int hp, int damage, int experience, int coins)
    {
        MaxHp = hp;
        Hp = hp;
        Damage = damage;
        Coins = coins;
        Experience = experience;
        Level = 1;
        //경험치 필요량
        experienceForLevel = CalculateExperienceNextLevel(Level);
    }

    public void PlayerDamage(int damage)
    {
        Hp -= damage;
        if (Hp < 0)
        {
            Hp = 0;
        }
    }

    public void Heal(int hp)
    {
        Hp += hp;
    }

    public void GetCoins(int coins)
    {
        Coins += coins;
        // UI 업데이트 호출
        UIController.Instance.UpdateCoinUI(Coins);
    }

    public void UsedCoins(int coins)
    {
        if (Coins > 0)
        {
            Coins -= coins;
            // UI 업데이트 호출
            UIController.Instance.UpdateCoinUI(Coins);
        }
    }

    public void GetSkillPoints(int skillPoint)
    {
        SkillPoints += skillPoint;
    }

    public void GetExperience(int exp)
    {
        Experience += exp;
        //레벨업 조건 달성 시 레벨업
        while (Experience >= experienceForLevel)
        {
            LevelUp();
            SkillPoints += 1;
            UIController.Instance.skillPoint.text = SkillPoints.ToString();
            //스킬포인트 텍스트에 스킬포인트를 넣어줬는데 사용 가능한 만큼만 넣어주고 쓰면 0으로 바꿔줘야돼
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

    public int CalculateExperienceNextLevel(int level)
    {
        return level * 10;
    }

    public bool IsAlive()
    {
        return Hp > 0;
    }



    // public void PlayerReset()
    // {
    //     Hp = MaxHp;
    //     Experience = 0;
    //     Level = 1;
    //     Coins = 0;
    //     experienceForLevel = CalculateExperienceNextLevel(Level);


    //     PlayerController.Instance.hpGauge.fillAmount = Hp / MaxHp;
    //     UIController.Instance.UpdateExpUI(Experience, experienceForLevel);
    //     UIController.Instance.UpdateLevelUI(Level);
    //     UIController.Instance.UpdateCoinUI(Coins);
    //     UIController.Instance.ResetUpButton();
    // }
}
