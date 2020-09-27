using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpecialManager : MonoBehaviour
{
    //現在ポイント（ゲーム共通）
    float pointCount;

    //スペシャル取得数（ゲーム共通）
    int specialGetCount;

    //現在装備中スペシャル
    int currentSpecial;

    //スペシャル取得コスト
    [SerializeField]
    int special1Cost = 5000;
    [SerializeField]
    int special2Cost = 10000;
    [SerializeField]
    int special3Cost = 20000;

    //スペシャルイメージオブジェクト
    public GameObject special1;
    public GameObject special2;
    public GameObject special3;
    public GameObject special1Up;
    public GameObject special2Up;
    public GameObject special3Up;
    public GameObject specialFrame1;
    public GameObject specialFrame2;
    public GameObject specialFrame3;

    //取得スペシャルタイプ
    GameObject specialType;
    GameObject specialUpType;
    GameObject nextSpecialUp;
    int specialCost;
    int nextSpecialCost;
    int getSpecialNum;

    //スペシャル用テキスト
    public Text specialText;

    //ウィンドウオブジェクト
    public GameObject messageWindow;
    public GameObject alertWindow;

    public TextMeshProUGUI getMessage;
    public string special1Message = "がめんじょう　ぜんたいこうげき";
    public string special2Message = "１かい　ダメージをふせぐ";
    public string special3Message = "すこしのあいだ　ポイントぞうか";

    void Start()
    {
        //現在ポイント取得
        pointCount = PlayerPrefs.GetFloat("TotalScore", 0);
        //持っているスペシャル数を取得
        specialGetCount = PlayerPrefs.GetInt("SpecialGetCount", 0);
        //現在装備中スペシャル取得
        currentSpecial = PlayerPrefs.GetInt("CurrentSpecial", 0);

        switch (specialGetCount)
        {
            case 0:
                specialText.text = "Next: 5000";
                break;
            case 1:
                specialText.text = "Next: 10000";
                special1Up.SetActive(false);
                special1.SetActive(true);
                
                special2Up.SetActive(true);
                break;
            case 2:
                specialText.text = "Next: 20000";
                special1Up.SetActive(false);
                special1.SetActive(true);
                special2.SetActive(true);
                special3Up.SetActive(true);
                break;
            case 3:
                specialText.text = "Next: 0";
                special1.SetActive(true);
                special2.SetActive(true);
                special3.SetActive(true);
                break;
        }

        switch (currentSpecial)
        {
            case 0:
                break;
            case 1:
                special1.SetActive(true);
                specialFrame1.SetActive(true);
                break;
            case 2:
                special2.SetActive(true);
                specialFrame2.SetActive(true);
                break;
            case 3:
                special3.SetActive(true);
                specialFrame3.SetActive(true);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //ポイントを常に取得
        pointCount = PlayerPrefs.GetFloat("TotalScore", 0);
    }

    public void TouchSpecial1()
    {
        //ポイントがコストよりも多ければ
        if(pointCount >= special1Cost)
        {
            //設定
            specialType = special1;
            specialUpType = special1Up;
            specialCost = special1Cost;
            nextSpecialUp = special2Up;
            nextSpecialCost = special2Cost;
            getSpecialNum = 1;

            getMessage.text = special1Message;
            //ポイントが足りていればメッセージウィンドウ表示
            messageWindow.SetActive(true);
        }
        else
        {
            //ポイントが足りなければ警告表示
            alertWindow.SetActive(true);
        }
    }

    public void TouchSpecial2()
    {
        //ポイントがコストよりも多ければ
        if (pointCount >= special2Cost)
        {
            //設定
            specialType = special2;
            specialUpType = special2Up;
            specialCost = special2Cost;
            nextSpecialUp = special3Up;
            nextSpecialCost = special3Cost;
            getSpecialNum = 2;

            getMessage.text = special2Message;
            //ポイントが足りていればメッセージウィンドウ表示
            messageWindow.SetActive(true);
        }
        else
        {
            //ポイントが足りなければ警告表示
            alertWindow.SetActive(true);
        }
    }

    public void TouchSpecial3()
    {
        //ポイントがコストよりも多ければ
        if (pointCount >= special3Cost)
        {
            //設定
            specialType = special3;
            specialUpType = special3Up;
            specialCost = special3Cost;
            nextSpecialUp = null;
            nextSpecialCost = 0;
            getSpecialNum = 3;

            getMessage.text = special3Message;
            //ポイントが足りていればメッセージウィンドウ表示
            messageWindow.SetActive(true);
        }
        else
        {
            //ポイントが足りなければ警告表示
            alertWindow.SetActive(true);
        }
    }

    public void GetSpecial()
    {
        //ウィンドウ、アイコンの表示非表示
        messageWindow.SetActive(false);

        specialType.SetActive(true);
        specialUpType.SetActive(false);

        if(getSpecialNum == 1)
        {
            //取得スペシャルが1であれば装備フレームと現在装備を1に自動セット
            specialFrame1.SetActive(true);
            PlayerPrefs.SetInt("CurrentSpecial", 1);
        }

        PlayerPrefs.SetInt("SpecialGetCount", getSpecialNum);

        if(nextSpecialUp != null)
        {
            //次レベルのスペシャルをアクティブにする
            nextSpecialUp.SetActive(true);
        }
        //次レベルのスペシャルのコストセット
        specialText.text = "Next: " + nextSpecialCost;
        //ポイントを減らす
        PlayerPrefs.SetFloat("TotalScore", pointCount - specialCost);
    }

    //装備スペシャルを1にセット
    public void ChangeToSpecial1()
    {
        currentSpecial = PlayerPrefs.GetInt("CurrentSpecial", 0);
        if (currentSpecial != 1)
        {
            specialFrame1.SetActive(true);
            specialFrame2.SetActive(false);
            specialFrame3.SetActive(false);
            PlayerPrefs.SetInt("CurrentSpecial", 1);
        }
    }
    //装備スペシャルを2にセット
    public void ChangeToSpecial2()
    {
        currentSpecial = PlayerPrefs.GetInt("CurrentSpecial", 0);
        if (currentSpecial != 2)
        {
            specialFrame1.SetActive(false);
            specialFrame2.SetActive(true);
            specialFrame3.SetActive(false);
            PlayerPrefs.SetInt("CurrentSpecial", 2);
        }
    }
    //装備スペシャルを3にセット
    public void ChangeToSpecial3()
    {
        currentSpecial = PlayerPrefs.GetInt("CurrentSpecial", 0);
        if (currentSpecial != 3)
        {
            specialFrame1.SetActive(false);
            specialFrame2.SetActive(false);
            specialFrame3.SetActive(true);
            PlayerPrefs.SetInt("CurrentSpecial", 3);
        }
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
