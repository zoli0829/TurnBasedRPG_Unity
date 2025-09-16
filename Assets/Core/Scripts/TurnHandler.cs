using UnityEngine;

[System.Serializable]
public class TurnHandler
{
    public string Attacker; // name of the attacker
    public string Type; 
    public GameObject AttackerGO; // who attacks
    public GameObject AttackersTarget; // who is being attacked
    
    // which attack is performed
}
