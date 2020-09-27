using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;
using UnityEngine.UI;
using TMPro;

public class PointAds : MonoBehaviour
{
    public GameObject adsButton;
    public Image adsButtonImage;
    public GameObject pointText;
    public GameObject timeTextObject;
    public TextMeshProUGUI timeText;

    //次回広告表示時間
    private const int Respawn_Time = 1800;

    //前回終了時間
    private DateTime lastDateTime;
    //前回終了からのタイムスパン
    TimeSpan timeSpan;
    //セーブ用時間
    private const string KEY_TIME = "TIME";
    string time;
    //動画広告のPlacementID
    private static readonly string VIDEO_PLACEMENT_ID = "video";

    public GameObject pointObject;
    PointDisplay pointDisplay;

    //　制限時間
    TimeSpan displayTime;
    
    bool adIsReady;
    private void Start()
    {
        pointDisplay = pointObject.GetComponent<PointDisplay>();

        time = PlayerPrefs.GetString(KEY_TIME, "");
        if(time == "")
        {
            timeTextObject.SetActive(false);
            pointText.SetActive(true);
            adIsReady = true;
            adsButtonImage.color = new Color(255, 255, 255, 1f);
        }
        else
        {
            long temp = Convert.ToInt64(time);
            lastDateTime = DateTime.FromBinary(temp);
        }

        timeSpan = DateTime.UtcNow - lastDateTime;
        if (timeSpan < TimeSpan.FromSeconds(Respawn_Time))
        {
            adIsReady = false;
            pointText.SetActive(false);
            timeTextObject.SetActive(true);
            adsButtonImage.color = new Color(255, 255, 255, 0.5f);
        }

        //ゲームIDをAndroidとそれ以外(iOS)で分ける
#if UNITY_ANDROID
        string gameID = "3747065";
#else
    string gameID = "3395905";
#endif

        //広告の初期化
        Advertisement.Initialize(gameID, testMode: true);
    }

    private void Update()
    {
        //時間の差　＝　現在時刻　－　前回の時刻
        timeSpan = DateTime.UtcNow - lastDateTime;

        if(timeSpan >= TimeSpan.FromSeconds(Respawn_Time))
        {
            timeTextObject.SetActive(false);
            pointText.SetActive(true);
            adIsReady = true;
            adsButtonImage.color = new Color(255, 255, 255, 1f);
        }

        displayTime = TimeSpan.FromSeconds(Respawn_Time) - timeSpan;

        timeText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", displayTime.Hours, displayTime.Minutes, displayTime.Seconds);

        PlayerPrefs.SetString(KEY_TIME, lastDateTime.ToBinary().ToString());
    }

    public void ShowAd()
    {
        if (!Advertisement.IsReady())
        {
            Debug.LogWarning("動画広告の準備が出来ていません");
            return;
        }

        var state = Advertisement.GetPlacementState(VIDEO_PLACEMENT_ID);

        if (state != PlacementState.Ready)
        {
            Debug.LogWarning($"{VIDEO_PLACEMENT_ID}の準備が出来ていません。現在の状態 : {state}");
            return; 
        }

        //Ads準備完了時
        if (Advertisement.IsReady())
        {
            if(adIsReady == true)
            {
                Advertisement.Show();
                pointDisplay.AddPoint();
                lastDateTime = DateTime.UtcNow;
                adIsReady = false;
                timeTextObject.SetActive(true);
                pointText.SetActive(false);
                adsButtonImage.color = new Color(255, 255, 255, 0.5f);
            }
        }
    }
}
