using System;
using UnityEngine;

[Serializable]
public class UpgradeLevel
{
    public string UpgradeTitle;
    public int Level;
    public int MaxLevel;

    public bool IsMaxLevel()    
    {
        return Level == MaxLevel;
    }

    public void UpLevel()
    {
        Level += 1;
    }
}
