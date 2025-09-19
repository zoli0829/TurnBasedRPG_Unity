using UnityEngine;

public class EnemySelectButton : MonoBehaviour
{
    public GameObject enemyPrefab;

    public void SelectEnemy()
    {
        GameObject.FindFirstObjectByType<BattleStateMachine>().Input2(enemyPrefab); // save input enemy prefab
    }
}
