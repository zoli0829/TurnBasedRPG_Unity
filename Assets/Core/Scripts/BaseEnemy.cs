using UnityEngine;

[System.Serializable]
public class BaseEnemy
{
    public string name;

    public enum Type
    {
        GRASS, FIRE, WATER, ELECTRIC
    }

    public enum Rarity
    {
        COMMON, UNCOMMON, RARE, SUPERRARE
    }
    
    public Type EnemyType;
    public Rarity rarity;
    
    public float baseHP;
    public float currentHP;

    public float baseMP;
    public float currentMP;

    public float baseATK;
    public float currentATK;
    public float baseDEF;
    public float currentDEF;
}
