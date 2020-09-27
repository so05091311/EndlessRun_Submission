using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using SocialConnector;
public class TweetButton : MonoBehaviour
{

    ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }

    //ツイート用メソッド
    public void Tweet()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            string text = Mathf.Round(scoreManager.scoreCount) + "m 走破！";
            string URL = "https://play.google.com/store/apps/details?id=com.SalmiyaLab.KunoichiSLASH";
       
                SocialConnector.SocialConnector.Share(text, URL, null);
        }
        //else if (Application.platform == RuntimePlatform.IPhonePlayer)
        //{

        //}
    }
}
