using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupPoints : MonoBehaviour
{
    public int scoreToGive;
    public float speed;

    private ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        switch (scoreManager.stagePhase)
        {
            case ScoreManager.StagePhase.Phase2:
                speed = 12;
                break;
            case ScoreManager.StagePhase.Phase3:
                speed = 13;
                break;
            case ScoreManager.StagePhase.Phase4:
                speed = 14;
                break;
            case ScoreManager.StagePhase.Phase5:
                speed = 15;
                break;
        }
    }

    //プレイヤー接触時ポイント増加
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            scoreManager.AddScore(scoreToGive);
            this.gameObject.SetActive(false);
        }
    }
}
