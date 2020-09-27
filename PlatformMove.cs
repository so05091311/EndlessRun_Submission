using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public float speed = 6;

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
        transform.position += Vector3.left * speed * Time.deltaTime;

        //フェイズによってプラットフォーム速度変化
        switch (scoreManager.stagePhase)
        {
            case ScoreManager.StagePhase.Phase2:
                speed = 12;
                break;
            case ScoreManager.StagePhase.Phase3:
                speed = 13;
                break;
            case ScoreManager.StagePhase.Phase4:
                speed = 14;
                break;
            case ScoreManager.StagePhase.Phase5:
                speed = 15;
                break;
        }
    }
}
