using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//遊び方のスペシャルボタン用スクリプト
public class TitleSpecialButton : MonoBehaviour
{
    //スペシャルボタンイメージ
    public Image specialButtonImage;
    public Image specialButtonFrame;
    public Image specialGaugeImg;
    //ゲージ量
    float specialCount;
    //ゲージ最大秒数
    public float countTime = 5.0f;
    //プッシュ文字オブジェクト
    public GameObject push;
    //ボタン押下フラグ
    bool isDisplayPush;
    // Update is called once per frame
    void Update()
    {
        // 最大１で止まるように
        if (specialCount <= 1.0f)
        {
            specialCount += 1.0f / countTime * Time.deltaTime;
            isDisplayPush = false;
        }
        else
        {
            //最大になった時にカラー変更
            specialButtonImage.color = new Color(255, 255, 255, 1.0f);
            specialButtonFrame.color = new Color(255, 218, 0, 1.0f);

            isDisplayPush = true;
        }

        if(isDisplayPush)
            push.SetActive(true);

        //ゲージをためていく
        specialGaugeImg.fillAmount += 1.0f / countTime * Time.deltaTime;
    }

    //ボタン押下時のメソッド
    public void PushSpecial()
    {
        if (specialCount >= 1.0f)
        {
            specialGaugeImg.fillAmount = 0;
            specialCount = 0;
            specialButtonFrame.color = new Color(255, 255, 255, 1.0f);
            specialButtonImage.color = new Color(255, 255, 255, 0.4f);
            push.SetActive(false);
        }
    }
}
