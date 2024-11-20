using UnityEngine;

public class Player
{
    public int Hp { get; private set; }
    public int Damage { get; private set; }
    public int Experience { get; private set; }

    
    public Player(int hp, int damage, int experience)
    {
        Hp = hp;
        Damage = damage;
        Experience = experience;
    }

    public void PlayerDamage(int damage) 
    {
        Hp -= damage;
        if(Hp < 0)
        {
            Hp = 0;
        }
    }

    public void Heal(int damage) 
    { 
        Hp += damage;   
    }

    public void GetExperience(int exp)
    {
        Experience += exp;  
    }

    public bool IsAlive()
    {
        return Hp > 0;
    }
}
