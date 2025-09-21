using UnityEngine;

public class EnemySelectButton : MonoBehaviour
{
    public GameObject enemyPrefab;
    private GameObject selector;
    private bool showSelector;

    public void SelectEnemy()
    {
        GameObject.FindFirstObjectByType<BattleStateMachine>().Input2(enemyPrefab); // save input enemy prefab
    }

    public void HideSelector()
    {
        selector = enemyPrefab.GetComponent<EnemyStateMachine>().GetSelector();
        selector.SetActive(false);
    }
    
    public void ShowSelector()
    {
        selector = enemyPrefab.GetComponent<EnemyStateMachine>().GetSelector();
        selector.SetActive(true);
    }
}
