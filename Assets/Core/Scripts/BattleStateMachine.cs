using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class BattleStateMachine : MonoBehaviour
{
    public enum PerformAction
    {
        WAIT, TAKEACTION, PERFORMACTION
    }
    
    public PerformAction action = PerformAction.WAIT; // battleState
    public List<TurnHandler> turnHandlers = new List<TurnHandler>(); // PerformList
    public List<GameObject> heroesInBattle = new List<GameObject>();
    public List<GameObject> enemiesInBattle = new List<GameObject>();

    public enum HeroGUI
    {
        ACTIVATE, WAITING, INPUT1, INPUT2, DONE
    }
    public HeroGUI heroInput;
    public List<GameObject> heroesToManage = new List<GameObject>();
    private TurnHandler heroChoice;

    public GameObject enemyButton;
    public Transform Spacer;
    
    void Start()
    {
        action = PerformAction.WAIT;
        enemiesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        heroesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Hero"));
        
        InstantiateEnemyButtons();
    }

    void Update()
    {
        switch (action)
        {
            case PerformAction.WAIT:
                if (turnHandlers.Count > 0)
                {
                    action = PerformAction.TAKEACTION;
                }
                break;
            case PerformAction.TAKEACTION:
                GameObject performer = GameObject.Find(turnHandlers[0].Attacker);
                if (turnHandlers[0].Type == "Enemy")
                {
                    EnemyStateMachine enemyStateMachine = performer.GetComponent<EnemyStateMachine>();
                    enemyStateMachine.heroToAttack = turnHandlers[0].AttackersTarget;
                    enemyStateMachine.currentState = EnemyStateMachine.TurnState.ACTION;
                }
                if (turnHandlers[0].Type == "Hero")
                {
                    
                }
                action = PerformAction.PERFORMACTION;
                break;
            case PerformAction.PERFORMACTION:
                
                break;
        }
    }

    public void CollectActions(TurnHandler turnHandler)
    {
        turnHandlers.Add(turnHandler);
    }

    void InstantiateEnemyButtons()
    {
        foreach (GameObject enemy in enemiesInBattle)
        {
            GameObject newButton = Instantiate(enemyButton);
            EnemySelectButton button = newButton.GetComponent<EnemySelectButton>();

            EnemyStateMachine currentEnemy = enemy.GetComponent<EnemyStateMachine>();
            
            TextMeshProUGUI buttonText = newButton.transform.Find("Text").GetComponent<TextMeshProUGUI>();
            buttonText.text = currentEnemy.name;

            button.enemyPrefab = enemy;
            
            newButton.transform.SetParent(Spacer); // or , false if it overflows
            newButton.transform.localScale = Vector3.one;
        }
    }
}
