using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreDisplay : MonoBehaviour
{
    //タイトル用ハイスコア表示
    public Text hiScoreText;

    float hiScoreCount;
    // Start is called before the first frame update
    void Start()
    {
        hiScoreCount = PlayerPrefs.GetFloat("HighScore", 0);
        hiScoreText.text = "ハイスコア: " + Mathf.Round(hiScoreCount);
    }
}
