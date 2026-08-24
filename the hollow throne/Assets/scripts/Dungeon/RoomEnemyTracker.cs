using System;
using UnityEngine;

public class RoomEnemyTracker : MonoBehaviour
{
    private int enemiesRemaining;
    private bool spawningFinished;
    private bool roomCleared;

    public event Action OnRoomCleared;

    public void RegisterEnemy(Health enemy)
    {
        if (enemy == null)
            return;

        enemiesRemaining++;
        enemy.OnDeath += HandleEnemyDeath;
    }

    public void SpawningFinished()
    {
        spawningFinished = true;
        CheckRoomCleared();
    }

    private void HandleEnemyDeath()
    {
        enemiesRemaining--;

        CheckRoomCleared();
    }

    private void CheckRoomCleared()
    {
        if (roomCleared)
            return;

        if (spawningFinished && enemiesRemaining <= 0)
        {
            enemiesRemaining = 0;
            roomCleared = true;

            OnRoomCleared?.Invoke();
            Debug.Log("Room cleared. All enemies defeated.");
        }
    }
}