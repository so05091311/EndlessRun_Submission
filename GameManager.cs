using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NCMB;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject scoreManager;
    ScoreManager sManager;

    //ポイント取得変数
    float playerTotalScore;

    //プレイヤーミス判定フラグ
    public bool playerIsMissed;

    public GameObject missText;

    [SerializeField] GameObject player;
    [SerializeField] GameObject startingPos;
    [SerializeField] GameObject[] playerLife;
    [SerializeField] GameObject[] playerLifeImage;

    //ライフアニメーション
    public DOTweenAnimation[] lifeAnimation;
    public int playerLifeCount;

    public TMP_InputField inputField;
    public GameObject submitButton;
    public Text scoreBoardText;

    PlayerController playerController;
    SoundManager soundManager;

    public bool sceneChanging;

    // Start is called before the first frame update
    void Start()
    {
        playerLifeCount = PlayerPrefs.GetInt("PlayerLifeCount", 1);
        playerTotalScore = PlayerPrefs.GetFloat("TotalScore", 0);

        playerController = player.GetComponent<PlayerController>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();

        sceneChanging = false;

        for (int i = 0; i <= playerLifeCount - 1; i++)
        {
            playerLife[i].SetActive(true);
        }

        sManager = scoreManager.GetComponent<ScoreManager>();
        playerIsMissed = false;

        for (int i = 0; i<= playerLifeCount-1; i++)
        {
            lifeAnimation[i] = playerLifeImage[i].GetComponent<DOTweenAnimation>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsMissed == true)
        {
            //ミス時にタッチした場合
            if (Input.GetMouseButtonDown(0) && playerLifeCount != 0)
            {
                player.SetActive(true);
                player.gameObject.layer = LayerMask.NameToLayer("Player");
                playerController.playerMiss = false;

                player.transform.position = startingPos.transform.position;
                sManager.scoreIncreasing = true;
                sManager.specialScoreIncreasing = false;
                playerIsMissed = false;
                missText.SetActive(false);
            }
        }

        if (playerLifeCount == 0)
        {
            sManager.scoreIncreasing = false;
            sManager.specialScoreIncreasing = false;
        }
    }


    //ミス時にPlayerから呼ばれる
    public void LifeLose()
    {
        if(sManager.scoreIncreasing == true)
        {
            sManager.scoreIncreasing = false;
            sManager.specialScoreIncreasing = false;

            lifeAnimation[playerLifeCount - 1].DOPlay();
            playerLifeCount -= 1;

            if (playerLifeCount == 0)
            {
                soundManager.audioSource.Stop();
                soundManager.BGM_result.Play();

                if (sceneChanging == false)
                {
                    naichilab.RankingLoader.Instance.SendScoreAndShowRanking(Mathf.Round(sManager.scoreCount));

                    PlayerPrefs.SetFloat("TotalScore", sManager.scoreCount + playerTotalScore);
                }
            }
            else
            {
                missText.SetActive(true);
            }
        }
    }
}
