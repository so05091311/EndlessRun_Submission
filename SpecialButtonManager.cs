using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class SpecialButtonManager : MonoBehaviour
{
    public Image specialGaugeImg;
    public float countTime = 15.0f;

    GameObject specialButton;
    public GameObject special1;
    public GameObject special2;
    public GameObject special3;

    public GameObject special1Effect;
    Animator special1Animator;

    public GameObject player;
    PlayerController playerController;

    public GameObject scoreManagerObject;
    ScoreManager scoreManager;

    public GameObject gameManagerObject;
    GameManager gameManager;

    public Text scoreText;
    public DOTweenAnimation scoreLargeAnimation;
    public DOTweenAnimation scoreSmallAnimation;

    //スペシャル１用エネミーリスト
    public List<EnemyController> enemyController;

    //現在装備中スペシャル
    int currentSpecial;

    //ゲージ量
    float specialCount;

    //スペシャル使用可能フラグ
    public bool isAvailableSpecial;

    Image specialButtonImage;
    public Image specialButtonFrame;

    //スペシャル３使用フラグ
    bool isUsingSpecial3;

    public AudioClip readySE;
    public AudioClip special1SE;
    public AudioClip special3SE;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        isAvailableSpecial = false;
        currentSpecial = PlayerPrefs.GetInt("CurrentSpecial", 0);

        audioSource = GetComponent<AudioSource>();
        playerController = player.GetComponent<PlayerController>();
        scoreManager = scoreManagerObject.GetComponent<ScoreManager>();
        gameManager = gameManagerObject.GetComponent<GameManager>();
        scoreLargeAnimation = scoreText.GetComponent<DOTweenAnimation>();
        scoreSmallAnimation = scoreText.GetComponent<DOTweenAnimation>();
        special1Animator = special1Effect.GetComponent<Animator>();

        switch (currentSpecial)
        {
            case 0:
                countTime = 0;
                specialButton = null;
                break;
            case 1:
                special1.SetActive(true);
                specialButton = special1;
                countTime = 10;
                break;
            case 2:
                special2.SetActive(true);
                specialButton = special2;
                countTime = 25;
                break;
            case 3:
                special3.SetActive(true);
                specialButton = special3;
                countTime = 7;
                break;
        }

        specialButtonImage = specialButton.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //ミスしていないとき、スペシャル３を発動していない時
        if(playerController.playerMiss == false && isUsingSpecial3 == false)
        {
            //最大１で止まるように
            if (specialCount <= 1.0f)
            {
                specialCount += 1.0f / countTime * Time.deltaTime;
            }
            else
            {
                specialButtonImage.color = new Color(255, 255, 255, 1.0f);
                specialButtonFrame.color = new Color(255, 218, 0, 1.0f);

                if(isAvailableSpecial == false)
                {
                    audioSource.PlayOneShot(readySE);
                    isAvailableSpecial = true;
                }
            }

            specialGaugeImg.fillAmount += 1.0f / countTime * Time.deltaTime;
        }

        //スペシャル３発動時
        if (isUsingSpecial3 == true)
        {
            specialGaugeImg.fillAmount -= 1.0f / 5 * Time.deltaTime;

            if(playerController.playerMiss == true)
            {
                specialGaugeImg.fillAmount -= 1.0f / 1 * Time.deltaTime;

                scoreText.transform.DOScale(new Vector3(1f, 1f, 1), 0.5f);

                scoreText.color = new Color(0, 0, 0, 1.0f);

                specialButtonFrame.color = new Color(255, 255, 255, 1.0f);
                specialButtonImage.color = new Color(255, 255, 255, 0.4f);
            }
        }
    }

    public void PushSpecial1()
    {
        //カウントMAX、ミスしてない時
        if (specialCount >= 1.0f && gameManager.playerIsMissed == false)
        {
            audioSource.PlayOneShot(special1SE);
            special1Animator.SetTrigger("end");

            //敵サーチ
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            enemyController = new List<EnemyController>();

            for (int i = 0; i <= enemies.Length - 1; i++)
            {
                enemyController.Add(enemies[i].GetComponent<EnemyController>());
                enemyController[i].AffectedBySpecial1();
            }

            specialGaugeImg.fillAmount = 0;
            specialCount = 0;
            specialButtonFrame.color = new Color(255, 255, 255, 1.0f);
            specialButtonImage.color = new Color(255, 255, 255, 0.4f);
            isAvailableSpecial = false;
        }
        
    }

    public void PushSpecial2()
    {
        if (specialCount >= 1.0f && playerController.isSpecial2EffectOn == false && gameManager.playerIsMissed == false)
        {
            playerController.Special2Activate();

            specialGaugeImg.fillAmount = 0;
            specialCount = 0;
            specialButtonFrame.color = new Color(255, 255, 255, 1.0f);
            specialButtonImage.color = new Color(255, 255, 255, 0.4f);
            isAvailableSpecial = false;
        }
    }

    public void PushSpecial3()
    {
        if (specialCount >= 1.0f && gameManager.playerIsMissed == false)
        {
            audioSource.PlayOneShot(special3SE);
            scoreManager.scoreIncreasing = false;
            scoreManager.specialScoreIncreasing = true;

            isUsingSpecial3 = true;

            scoreText.color = new Color(255, 0, 0, 1.0f);

            scoreText.transform.DOScale(new Vector3(1.2f, 1.2f, 1), 0.5f);
            
            specialCount = 0;
            specialButtonFrame.color = new Color(255, 0, 0, 1.0f);
            isAvailableSpecial = false;

            Invoke("Special3BackToNormal", 5f);
        }
    }

    //スペシャル3解除時
    void Special3BackToNormal()
    {
        if (gameManager.playerIsMissed == false)
        {
            scoreManager.scoreIncreasing = true;
        }
        scoreManager.specialScoreIncreasing = false;

        scoreText.transform.DOScale(new Vector3(1f, 1f, 1), 0.5f);
        scoreText.color = new Color(0, 0, 0, 1.0f);
        isUsingSpecial3 = false;

        specialButtonFrame.color = new Color(255, 255, 255, 1.0f);
        specialButtonImage.color = new Color(255, 255, 255, 0.4f);
    }
}
