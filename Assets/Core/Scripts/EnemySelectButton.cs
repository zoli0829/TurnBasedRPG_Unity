using UnityEngine;

public class EnemySelectButton : MonoBehaviour
{
    public GameObject enemyPrefab;

    public void SelectEnemy()
    {
        GameObject.FindFirstObjectByType<BattleStateMachine>(); // save input enemy prefab
    }
}
