using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //敵の弾丸スピード
    public float speed;

    //ScoreManager格納用
    public GameObject scoreManagerObject;
    ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        scoreManagerObject = GameObject.FindGameObjectWithTag("ScoreManager");
        scoreManager = scoreManagerObject.GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //弾丸を左に移動
        transform.position += Vector3.left * speed * Time.deltaTime;

        //フェイズごとに弾丸速度の変化
        switch (scoreManager.stagePhase)
        {
            case ScoreManager.StagePhase.Phase2:
                speed = 15;
                break;
            case ScoreManager.StagePhase.Phase3:
                speed = 16;
                break;
            case ScoreManager.StagePhase.Phase4:
                speed = 17;
                break;
            case ScoreManager.StagePhase.Phase5:
                speed = 18;
                break;
        }
    }
}
