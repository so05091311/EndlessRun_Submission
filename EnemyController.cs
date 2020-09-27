using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //敵スピード
    public float speed;
    //敵非アクティブポイント
    public GameObject destructionPoint;

    Animator animator;

    public GameObject scoreManagerObject;
    ScoreManager scoreManager;

    //フェイズ切り替え用
    bool isPhase2 = false;
    bool isPhase3 = false;
    bool isPhase4 = false;
    bool isPhase5 = false;

    //敵ヒットSE
    public AudioClip hitSE;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        scoreManagerObject = GameObject.FindGameObjectWithTag("ScoreManager");
        scoreManager = scoreManagerObject.GetComponent<ScoreManager>();
        audioSource = GetComponent<AudioSource>();

        destructionPoint = GameObject.FindGameObjectWithTag("DestroyPoint");
    }

    // Update is called once per frame
    void Update()
    {
        //敵が常に左に移動する
        transform.position += Vector3.left * speed * Time.deltaTime;

        //非アクティブポイントに到達したら敵が非アクティブ
        if (transform.position.x < destructionPoint.transform.position.x || transform.position.y < destructionPoint.transform.position.y)
        {
            gameObject.SetActive(false);
        }
        //見かけ上停止の敵のフェイズごとの速度調整
        if(LayerMask.LayerToName(gameObject.layer) == "Enemy")
        {
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
        }else if(LayerMask.LayerToName(gameObject.layer) == "EnemyMove")
        {//見かけ上動いている敵のフェイズごとの速度調整
            switch (scoreManager.stagePhase)
            {
                case ScoreManager.StagePhase.Phase2:
                    speed = 13.5f;
                    break;
                case ScoreManager.StagePhase.Phase3:
                    speed = 14.5f;
                    break;
                case ScoreManager.StagePhase.Phase4:
                    speed = 15.5f;
                    break;
                case ScoreManager.StagePhase.Phase5:
                    speed = 16.5f;
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤーの攻撃判定に接触した場合
        if (collision.name == "attackCollision")
        {
            audioSource.PlayOneShot(hitSE);

            //死亡アニメーション再生
            animator.Play("Melee_Die");

            //停止エネミーのレイヤー切り替え
            if (LayerMask.LayerToName(gameObject.layer) == "Enemy")
            {
                gameObject.layer = LayerMask.NameToLayer("Invisible");
            }
            //歩行エネミーのレイヤー切り替え
            if (LayerMask.LayerToName(gameObject.layer) == "EnemyMove")
            {
                gameObject.layer = LayerMask.NameToLayer("InvisibleMove");
            }
        }
    }

    //スペシャル１使用の際のメソッド
    public void AffectedBySpecial1()
    {
        //死亡アニメーション再生
        animator.Play("Melee_Die");

        //停止エネミーのレイヤー切り替え
        if (LayerMask.LayerToName(gameObject.layer) == "Enemy")
        {
            gameObject.layer = LayerMask.NameToLayer("Invisible");
        }
        //歩行エネミーのレイヤー切り替え
        if (LayerMask.LayerToName(gameObject.layer) == "EnemyMove")
        {
            gameObject.layer = LayerMask.NameToLayer("InvisibleMove");
        }
    }

}
