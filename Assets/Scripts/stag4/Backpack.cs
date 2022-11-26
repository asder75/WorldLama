using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backpack : Loot
{
    public Text countMeetText;
    public Text countGrassText;
    public Text countPoisonGrassText;
    public Text countBerryText;
    public Text countPoisonBerryText;
    public Text countEggText;
    public Text currentLootText;

    private void Start()
    {
        countMeet = 0;
        countGrass = 0;
        countPoisonGrass = 0;
        countBerry = 0;
        countPoisonBerry = 0;
        countEgg = 0;

        winterHat = false;
        tigerCap = false;

        currentLoot = 0;
        maxLoot = 10;
    }

    private void FixedUpdate()
    {
        countMeetText.text = "" + countMeet;
        countGrassText.text = "" + countGrass;
        countPoisonGrassText.text = "" + countPoisonGrass;
        countBerryText.text = "" + countBerry;
        countPoisonBerryText.text = "" + countPoisonBerry;
        countEggText.text = "" + countEgg;

        currentLoot = countMeet + countGrass + countPoisonGrass + countBerry + countPoisonBerry +  countEgg;
        currentLootText.text = "" + currentLoot + "/" + maxLoot;
    }
}
