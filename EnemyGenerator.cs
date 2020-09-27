using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{

    public PlatformPooler[] enemyPool;

    private int enemySelector;

    public float distanceBetweenEnemies;

    float randomPosX;
    float randomPosY;

    private void Update()
    {
        enemySelector = Random.Range(0, enemyPool.Length);

        randomPosX = Random.Range(-0.5f, 1f);
        randomPosY = Random.Range(0f, 7f);
    }

    public void SpawnEnemies(Vector3 startPos)
    {
        GameObject enemy = enemyPool[enemySelector].GetPooledObject();
        enemy.transform.position = new Vector3(startPos.x + randomPosX, startPos.y + randomPosY, startPos.z);

        if (LayerMask.LayerToName(enemy.gameObject.layer) == "Invisible")
        {
            enemy.gameObject.layer = LayerMask.NameToLayer("Enemy");
        }


        if (LayerMask.LayerToName(enemy.gameObject.layer) == "InvisibleMove")
        {
            enemy.gameObject.layer = LayerMask.NameToLayer("EnemyMove");
        }


        enemy.SetActive(true);

        
    }
}
