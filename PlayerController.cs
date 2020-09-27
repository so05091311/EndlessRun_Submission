using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject gameManager;
    GameManager gm;

    PlayerJump pj;
    Animator animator;

    public GameObject shield;
    public bool isSpecial2EffectOn;

    public AudioClip itemGetSE;
    public AudioClip enemyHitSE;
    public AudioClip shieldSE;
    public AudioClip shieldBreakSE;
    AudioSource audioSource;

    //プレイヤーミスフラグ
    public bool playerMiss; 
    // Start is called before the first frame update
    void Start()
    {
        gm = gameManager.GetComponent<GameManager>();
        pj = this.GetComponent<PlayerJump>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //ステージ外に接触時
        if(other.gameObject.tag == "Gameover")
        {
            playerMiss = true;

            //スペシャル2・プレイヤー非アクティブ、ライフ-1メソッド
            shield.SetActive(false);
            isSpecial2EffectOn = false;
            gm.LifeLose();
            gameObject.SetActive(false);

            Invoke("PlayerMissedOn", 1.5f);
        }

        //アイテム接触時取得SE
        if (other.gameObject.tag == "Item")
        {
            audioSource.PlayOneShot(itemGetSE);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //敵、障害物接触時
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Obstacle")
        {
            //スペシャル2発動時
            if (isSpecial2EffectOn)
            {
                shield.SetActive(false);

                StartCoroutine("DeactivateShield");
            }
            else
            {
                audioSource.PlayOneShot(enemyHitSE);
                //落ちたときに動けない用フラグ
                playerMiss = true;

                gameObject.layer = LayerMask.NameToLayer("Invisible");
                animator.Play("Damage");

                StartCoroutine("PlayerMove");
            }
        }
    }

    //スペシャル2発動メソッド
    public void Special2Activate()
    {
        if(playerMiss == false)
        {
            audioSource.PlayOneShot(shieldSE);
            shield.SetActive(true);
            isSpecial2EffectOn = true;
        }
    }

    //スペシャル2解除コルーチン
    IEnumerator DeactivateShield()
    {
        MeshRenderer meshRenderer = this.GetComponent<MeshRenderer>();

        gameObject.layer = LayerMask.NameToLayer("Invisible");

        audioSource.PlayOneShot(shieldBreakSE);
        for (int i = 0; i <= 60 ;i++)
        {
            meshRenderer.enabled = !meshRenderer.enabled;

            yield return new WaitForSeconds(0.01f);
        }
        meshRenderer.enabled = true;
        gameObject.layer = LayerMask.NameToLayer("Player");
        isSpecial2EffectOn = false;
    }

    public void  PlayerMissedOn()
    {
        //GameManagerのミスフラグオン
        gm.playerIsMissed = true;
        //ジャンプカウントリセット
        pj.jumpCount = pj.MAX_JUMP_COUNT;
    }

}
