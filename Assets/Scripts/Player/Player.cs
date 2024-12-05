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

    public float SlideSpeed { get; set; }
    public float SlideDuration { get; set; }
    public float SlideTimer { get; set; }
    public float Speed { get; set; }
    public float JumpSpeed { get; set; }
    public float InvincibilityTime { get; set; }
    public float PrevVx { get; set; }
    public float Vx { get; set; }

    private int experienceForLevel;

    public Weapon weapon;

    public Player(int hp, int damage, int experience, int coins)
    {
        weapon = new Weapon();
        SlideSpeed = 10f;
        SlideDuration = 0.5f;
        SlideTimer = 0f;
        Speed = 5;
        JumpSpeed = 10;
        InvincibilityTime = 1f;
        PrevVx = 0;
        Vx = 0;

        MaxHp = hp;
        Hp = hp;
        Damage = damage;
        Coins = coins;
        Experience = experience;
        Level = 1;
        //경험치 필요량
        experienceForLevel = CalculateExperienceNextLevel(Level);
    }

    public void GetDamage(int damage)
    {
        Hp -= damage;
        if (Hp < 0)
        {
            Hp = 0;
        }
    }

    public void Heal(int hp)
    {
        if (Hp > 100)
        {
            Hp = 100;
        }
        else
        {
            Hp += hp;
        }

    }


    public void SetCoins(int coins, string type)
    {
        if (type == "Up")
        {
            Coins += coins;
        }
        else if (type == "Down")
        {
            Coins -= coins;

        }
        UIController.Instance.UpdateCoinUI(Coins);
    }


    public void SetSkillPoints(int skillPoint, string type)
    {
        if (type == "Up")
        {
            SkillPoints += skillPoint;
        }
        else if (type == "Down")
        {
            if (skillPoint > 0)
            {
                SkillPoints -= skillPoint;
            }
        }
        UIController.Instance.UpdateSkillPointUI(SkillPoints);
    }

    public void GetExperience(int exp)
    {
        Experience += exp;
        //레벨업 조건 달성 시 레벨업
        while (Experience >= experienceForLevel)
        {
            LevelUp();
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
        SetSkillPoints(1, "Up");
    }

    public int CalculateExperienceNextLevel(int level)
    {
        return level * 10;
    }

    public bool IsAlive()
    {
        return Hp > 0;
    }

}
