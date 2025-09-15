using UnityEngine;
using System.Collections.Generic;

public class BattleStateMachine : MonoBehaviour
{
    public enum PerformAction
    {
        WAIT, TAKEACTION, PERFORMACTION
    }
    
    public PerformAction action = PerformAction.WAIT;
    public List<TurnHandler> turnHandlers = new List<TurnHandler>();
    public List<GameObject> heroesInBattle = new List<GameObject>();
    public List<GameObject> enemiesInBattle = new List<GameObject>();
    
    void Start()
    {
        action = PerformAction.WAIT;
        enemiesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        heroesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Hero"));
    }

    void Update()
    {
        switch (action)
        {
            case PerformAction.WAIT:
                
                break;
            case PerformAction.TAKEACTION:
                
                break;
            case PerformAction.PERFORMACTION:
                
                break;
        }
    }

    public void CollectActions(TurnHandler turnHandler)
    {
        turnHandlers.Add(turnHandler);
    }
}
