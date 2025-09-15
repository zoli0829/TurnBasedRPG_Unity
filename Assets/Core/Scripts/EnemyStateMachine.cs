using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    private BattleStateMachine battleStateMachine;
    public BaseEnemy enemy;
    
    public enum TurnState
    {
        PROCESSING, CHOOSEACTION, WAITING, ACTION, DEAD
    }
    
    public TurnState currentState;
    
    // for the ProgressBar
    private float currentCooldown = 0f;
    private float maximumCooldown = 5f;
    
    // this gameObject
    private Vector3 startPosition;
    
    void Start()
    {
        currentState = TurnState.PROCESSING;
        battleStateMachine = GameObject.FindFirstObjectByType<BattleStateMachine>();
        startPosition = transform.position;
    }

    void Update()
    {
        switch (currentState)
        {
            case TurnState.PROCESSING:
                UpdateProgressBar();
                break;
            case TurnState.CHOOSEACTION:
                ChooseAction();
                currentState = TurnState.WAITING;
                break;
            case TurnState.WAITING:
                // idle state
                break;
            case TurnState.ACTION:
                
                break;
            case TurnState.DEAD:
                
                break;
        }
    }
    
    void UpdateProgressBar()
    {
        currentCooldown += Time.deltaTime;
        if (currentCooldown >= maximumCooldown)
        {
            currentState = TurnState.CHOOSEACTION;
        }
    }

    void ChooseAction()
    {
        TurnHandler myAttack = new TurnHandler();
        myAttack.Attacker = enemy.name;
        myAttack.AttackerGO = this.gameObject;
        myAttack.AttackersTarget = battleStateMachine.heroesInBattle[Random.Range(0, battleStateMachine.heroesInBattle.Count)];
        battleStateMachine.CollectActions(myAttack);
    }
}
