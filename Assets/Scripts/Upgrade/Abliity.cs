using System;
using UnityEngine;

[Serializable]
public class Abliity : UpgradeLevel
{
    public int Cost;

    public void SetCost()
    {
        Cost = 10 * Level;
    }
}
