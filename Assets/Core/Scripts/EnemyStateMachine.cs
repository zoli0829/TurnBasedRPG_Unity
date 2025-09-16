using System.Collections;
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
    // time for action stuff
    private bool actionStarted = false;
    public GameObject heroToAttack;
    private float animSpeed = 5f;
    
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
                StartCoroutine(TimeForAction());
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
        myAttack.Type = "Enemy";
        myAttack.AttackerGO = this.gameObject;
        myAttack.AttackersTarget = battleStateMachine.heroesInBattle[Random.Range(0, battleStateMachine.heroesInBattle.Count)];
        battleStateMachine.CollectActions(myAttack);
    }

    IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }
        
        actionStarted = true;
        
        // animate the enemy near the hero to attack
        Vector3 heroPosition = new Vector3(heroToAttack.transform.position.x - 1.5f, heroToAttack.transform.position.y, heroToAttack.transform.position.z);
        while (MoveTowardsTarget(heroPosition)) yield return null;
        
        // wait a bit
        yield return new WaitForSeconds(0.5f);
        
        // do damage
        
        // animate back to start position
        Vector3 positionToReturnTo = startPosition; // firstPosition
        while (MoveTowardsStartPosition(positionToReturnTo)) yield return null;
        
        // remove this performer from the list in battleStateMachine
        battleStateMachine.turnHandlers.RemoveAt(0);
        
        // reset the battleStateMachine -> Wait
        battleStateMachine.action = BattleStateMachine.PerformAction.WAIT;

        // end coroutine
        actionStarted = false;
        
        // reset this enemy state
        currentCooldown = 0f;
        currentState = TurnState.PROCESSING;
    }

    bool MoveTowardsTarget(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }
    
    bool MoveTowardsStartPosition(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }
}
