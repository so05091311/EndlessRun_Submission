using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text hiScoreText;

    public float scoreCount;
    public float hiScoreCount;

    public float pointsPerSec;

    public bool scoreIncreasing;
    public bool specialScoreIncreasing;

    public enum StagePhase
    {
        Phase1,
        Phase2,
        Phase3,
        Phase4,
        Phase5
    }

    public StagePhase stagePhase;
    // Start is called before the first frame update
    void Start()
    {
        //ハイスコア取得
        hiScoreCount = PlayerPrefs.GetFloat("HighScore", 0);
        //フェイズを初期化
        stagePhase = StagePhase.Phase1;
    }

    // Update is called once per frame
    void Update()
    {
        //スコア増加フラグがTRUE時
        if (scoreIncreasing)
        {
            scoreCount += pointsPerSec * Time.deltaTime;
        }else if (specialScoreIncreasing)  //スペシャル３発動時
        {
            scoreCount += 3 * pointsPerSec * Time.deltaTime;
        }

        //ハイスコアを超えた場合更新し続ける
        if(scoreCount > hiScoreCount)
        {
            hiScoreCount = scoreCount;
            PlayerPrefs.SetFloat("HighScore", hiScoreCount);
        }
        scoreText.text = "スコア: " + Mathf.Round(scoreCount);
        hiScoreText.text = "ハイスコア: " + Mathf.Round(hiScoreCount);

        //現在スコアでフェイズの切り替え
        if(scoreCount < 2000 && scoreCount >= 800)
        {
            stagePhase = StagePhase.Phase2;
        }else if(scoreCount < 4000 && scoreCount >= 2000)
        {
            stagePhase = StagePhase.Phase3;
        }else if(scoreCount < 7000 && scoreCount >= 4000)
        {
            stagePhase = StagePhase.Phase4;
        }
        else if (scoreCount >= 7000)
        {
            stagePhase = StagePhase.Phase5;
        }
    }

    //アイテム獲得時にスコア増加メソッド
    public void AddScore(int pointsToAdd)
    {
        scoreCount += pointsToAdd;
    }
}
