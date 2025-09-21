using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeroStateMachine : MonoBehaviour
{
    private BattleStateMachine battleStateMachine;
    public BaseHero hero;

    public enum TurnState
    {
        PROCESSING, ADDTOLIST, WAITING, SELECTING, ACTION, DEAD
    }
    
    public TurnState currentState;
    
    // for the ProgressBar
    private float currentCooldown = 0f;
    private float maximumCooldown = 5f;
    public Image progressBar;
    public GameObject selector;
    
    // IEnumerator
    public GameObject enemyToAttack;
    private bool actionStarted = false;
    private Vector3 startPosition;
    private float animSpeed = 10f;
    
    
    void Start()
    {
        startPosition = transform.position;
        currentCooldown = Random.Range(0, 2.5f);
        selector.SetActive(false);
        battleStateMachine = GameObject.FindFirstObjectByType<BattleStateMachine>();
        currentState = TurnState.PROCESSING;
    }
    
    void Update()
    {
        switch (currentState)
        {
            case TurnState.PROCESSING:
                UpdateProgressBar();
                break;
            case TurnState.ADDTOLIST:
                battleStateMachine.heroesToManage.Add(this.gameObject);
                currentState = TurnState.WAITING;
                break;
            case TurnState.WAITING:
                // idle
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
        float percentage = currentCooldown / maximumCooldown; 
        progressBar.transform.localScale = new Vector3(Mathf.Clamp(percentage, 0, 1), progressBar.transform.localScale.y, progressBar.transform.localScale.z);
        if (currentCooldown >= maximumCooldown)
        {
            currentState = TurnState.ADDTOLIST;
        }
    }

IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }
        
        actionStarted = true;
        
        // animate the enemy near the hero to attack
        Vector3 enemyPosition = new Vector3(enemyToAttack.transform.position.x + 1.5f, enemyToAttack.transform.position.y, enemyToAttack.transform.position.z);
        while (MoveTowardsTarget(enemyPosition)) yield return null;
        
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
