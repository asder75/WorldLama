using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Loot : MonoBehaviour
{
   public int countMeet { get; set; }
    public int countGrass { get; set; }
    public int countPoisonGrass { get; set; }
    public int countBerry { get; set; }
    public int countPoisonBerry { get; set; }
    public int countEgg { get; set; }

    public bool winterHat { get; set; }
    public bool tigerCap { get; set; }

    public int currentLoot { get; set; }
    public int maxLoot { get; set; }
}
