using UnityEngine;

[System.Serializable]
public class BaseEnemy : BaseCharacter
{
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
}
