using UnityEngine;
using UnityEngine.UI;

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
    
    void Start()
    {
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
}
