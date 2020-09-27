using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointDisplay : MonoBehaviour
{
    public Text displayPoint;

    public float pointCount;

    // Start is called before the first frame update
    void Start()
    {
        pointCount = PlayerPrefs.GetFloat("TotalScore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        pointCount = PlayerPrefs.GetFloat("TotalScore", 0);
        displayPoint.text = "" + Mathf.Round(pointCount);
    }

    public void AddPoint()
    {
        PlayerPrefs.SetFloat("TotalScore", pointCount + 1000);
    }
}
