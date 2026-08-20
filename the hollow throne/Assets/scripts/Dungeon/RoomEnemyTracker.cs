using System;
using UnityEngine;

public class RoomEnemyTracker : MonoBehaviour
{
    private int enemiesRemaining;

    public event Action OnRoomCleared;

    public void RegisterEnemy(Health enemy)
    {
        if (enemy == null)
            return;

        enemiesRemaining++;
        enemy.OnDeath += HandleEnemyDeath;
    }

    private void HandleEnemyDeath()
    {
        enemiesRemaining--;

        if (enemiesRemaining <= 0)
        {
            OnRoomCleared?.Invoke();
        }
    }
}