using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthManager : MonoBehaviour
{
    //ポイント
    float pointCount;

    //現在ライフ数（ゲーム共通）
    int playerLifeCount;

    //ヘルスコスト
    [SerializeField]
    int health2Cost = 8000;
    [SerializeField]
    int health3Cost = 25000;

    //取得ヘルスタイプ
    GameObject healthType;
    GameObject healthUpType;
    GameObject nextHealthUp;
    int healthCost;
    int nextHealthCost;
    int getHealthNum;

    //ヘルスオブジェクト
    public GameObject health2;
    public GameObject healthUp2;
    public GameObject health3;
    public GameObject healthUp3;

    //メッセージウィンドウ
    public GameObject messageWindow;
    public GameObject alertWindow;

    public Text healthText;

    public TextMeshProUGUI getMessage;
    public string healthMessage = "ライフ　+1";

    // Start is called before the first frame update
    void Start()
    {
        pointCount = PlayerPrefs.GetFloat("TotalScore", 0);
        playerLifeCount = PlayerPrefs.GetInt("PlayerLifeCount", 1);

        //確認用後で消す
        //PlayerPrefs.SetFloat("TotalScore", 200000);

        switch (playerLifeCount)
        {
            case 1:
                healthText.text = "Next: " + health2Cost;
                break;
            case 2:
                healthText.text = "Next: " + health3Cost;
                healthUp2.SetActive(false);
                health2.SetActive(true);
                healthUp3.SetActive(true);
                break;
            case 3:
                healthText.text = "Next: 0";
                healthUp2.SetActive(false);
                health2.SetActive(true);
                healthUp3.SetActive(false);
                health3.SetActive(true);
                //healthUp4.SetActive(true);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        pointCount = PlayerPrefs.GetFloat("TotalScore", 0);
    }

    public void TouchHealth2()
    {
        if (pointCount >= health2Cost)
        {
            healthType = health2;
            healthUpType = healthUp2;
            healthCost = health2Cost;
            nextHealthUp = healthUp3;
            nextHealthCost = health3Cost;
            getHealthNum = 2;

            getMessage.text = healthMessage;
            //ポイントが足りていればメッセージウィンドウ表示
            messageWindow.SetActive(true);
        }
        else
        {
            //ポイントが足りなければ警告表示
            alertWindow.SetActive(true);
        }
    }

    public void TouchHealth3()
    {
        if (pointCount >= health3Cost)
        {
            healthType = health3;
            healthUpType = healthUp3;
            healthCost = health3Cost;
            nextHealthUp = null;
            nextHealthCost = 0;
            getHealthNum = 3;

            getMessage.text = healthMessage;
            //ポイントが足りていればメッセージウィンドウ表示
            messageWindow.SetActive(true);
        }
        else
        {
            //ポイントが足りなければ警告表示
            alertWindow.SetActive(true);
        }
    }

    public void GetHealth()
    {
        //ウィンドウ、アイコンの表示非表示
        messageWindow.SetActive(false);

        healthType.SetActive(true);
        healthUpType.SetActive(false);
        PlayerPrefs.SetInt("PlayerLifeCount", getHealthNum);

        if (nextHealthUp != null)
        {
            nextHealthUp.SetActive(true);
        }

        healthText.text = "Next: " + nextHealthCost;
        //ポイントを減らす
        PlayerPrefs.SetFloat("TotalScore", pointCount-healthCost);
    }

    public void CloseAlertWindow()
    {
        alertWindow.SetActive(false);
    }

    public void CloseMessageWindow()
    {
        messageWindow.SetActive(false);
    }
}
