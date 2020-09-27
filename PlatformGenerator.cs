using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject thePlatform;
    
    private int platformSelector;

    public PlatformPooler[] platformPool;

    private float[] platformWidths;

    //プラットフォーム出現の高さオフセット
    private float heightOffset;

    public GameObject itemGeneratorObject;
    ItemGenerator itemGenerator;
    public float randomItemThreshold;  //アイテム出現のランダム閾値

    public GameObject enemyGeneratorObject;
    EnemyGenerator enemyGenerator;
    public float randomEnemyThreshold;  //敵出現のランダム閾値

    public GameObject scoreManagerObject;
    ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        itemGenerator = itemGeneratorObject.GetComponent<ItemGenerator>();

        enemyGenerator = enemyGeneratorObject.GetComponent<EnemyGenerator>();

        scoreManager = scoreManagerObject.GetComponent<ScoreManager>();
        StartCoroutine(GeneratePlatform(1.35f));
    }

    // Update is called once per frame
    void Update()
    {
        heightOffset = Random.Range(-3f, 0.4f);

        //フェイズによって出現プラットフォームの種類変化
        switch (scoreManager.stagePhase)
        {
            case ScoreManager.StagePhase.Phase1:
                platformSelector = Random.Range(0, platformPool.Length - 10);
                break;
            case ScoreManager.StagePhase.Phase2:
                platformSelector = Random.Range(0, platformPool.Length - 7);
                break;
            case ScoreManager.StagePhase.Phase3:
                platformSelector = Random.Range(0, platformPool.Length - 4);
                break;
            case ScoreManager.StagePhase.Phase4:
                platformSelector = Random.Range(0, platformPool.Length - 2);
                break;
            case ScoreManager.StagePhase.Phase5:
                platformSelector = Random.Range(0, platformPool.Length);
                break;
        }
    }

    //プラットフォーム出現コルーチン
    IEnumerator GeneratePlatform(float second)
    {
        while (true)
        {
            // secondで指定した秒数ループ
            yield return new WaitForSeconds(second);

            GameObject newPlatform = platformPool[platformSelector].GetPooledObject();
            newPlatform.transform.position = new Vector3(transform.position.x, transform.position.y + heightOffset, transform.position.z);
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            if(Random.Range(0f, 100f) < randomItemThreshold)
            {
                itemGenerator.SpawnItems(new Vector3(newPlatform.transform.position.x, newPlatform.transform.position.y + 1f, newPlatform.transform.position.z));
            }

            if (Random.Range(100f, 200f) < randomEnemyThreshold)
            {
                enemyGenerator.SpawnEnemies(new Vector3(newPlatform.transform.position.x, newPlatform.transform.position.y + 1f, newPlatform.transform.position.z));
            }
        }
    }
}
