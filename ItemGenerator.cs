using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public PlatformPooler[] itemPool;

    private int itemSelector;

    float randomPosX;
    float randomPosY;

    private void Update()
    {
        //アイテムのランダム選択
        itemSelector = Random.Range(0, itemPool.Length);

        //X、Yオフセットのランダム取得
        randomPosX = Random.Range(-3f, 2f);
        randomPosY = Random.Range(0f, 5f);
    }

    public void SpawnItems(Vector3 startPos)
    {
        GameObject item1 = itemPool[itemSelector].GetPooledObject();
        item1.transform.position = new Vector3(startPos.x + randomPosX, startPos.y + randomPosY, startPos.z);
        item1.SetActive(true);
    }
}
