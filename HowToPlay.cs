using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    //遊び方パネルオブジェクト
    public GameObject HowToPlayPanel;
    public GameObject firstPage;
    public GameObject secondPage;
    public GameObject thirdPage;
    public GameObject fourthPage;

    //各ボタンを押した際のメソッド
    public void PushHowToPlayButton()
    {
        HowToPlayPanel.SetActive(true);
    }

    public void FirstPageToSecond()
    {
        firstPage.SetActive(false);
        secondPage.SetActive(true);
    }

    public void SecondToFirst()
    {
        firstPage.SetActive(true);
        secondPage.SetActive(false);
    }

    public void SecondToThird()
    {
        thirdPage.SetActive(true);
        secondPage.SetActive(false);
    }

    public void ThirdToSecond()
    {
        thirdPage.SetActive(false);
        secondPage.SetActive(true);
    }

    public void ThirdToFourth()
    {
        fourthPage.SetActive(true);
        thirdPage.SetActive(false);
    }

    public void FourthToThird()
    {
        fourthPage.SetActive(false);
        thirdPage.SetActive(true);
    }

    public void CloseWindow()
    {
        fourthPage.SetActive(false);
        firstPage.SetActive(true);
        HowToPlayPanel.SetActive(false);
    }
}
