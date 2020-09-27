using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
public class SoundManager : MonoBehaviour
{
    // 現在存在しているオブジェクト実体の記憶領域
    static SoundManager _instance = null;

    // オブジェクト実体の参照（初期参照時、実体の登録も行う）
    static SoundManager instance
    {
        get { return _instance ?? (_instance = FindObjectOfType<SoundManager>()); }
    }

    //ヒエラルキーからD&Dしておく
    public AudioSource BGM_title;
    public AudioSource BGM_customize;
    public AudioSource BGM_result;


    public AudioClip[] mainBGMs;
    public AudioClip currentBGM;
    //１つ前のシーン名
    private string beforeScene = "TitleScreen";
    public AudioSource audioSource;
    public AudioSource titleSE;

    // Use this for initialization
    void Start()
    {
        Invoke("TitleSE", 2.6f);
        Invoke("TitleBGM", 4f);
        //オブジェクトが重複していたらここで破棄される

        // 自身がインスタンスでなければ自滅
        if (this != instance)
        {
            Destroy(gameObject);
            return;
        }

        //自分と各BGMオブジェクトをシーン切り替え時も破棄しないようにする
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(BGM_title.gameObject);
        DontDestroyOnLoad(BGM_customize.gameObject);
        DontDestroyOnLoad(titleSE.gameObject);

        audioSource = GetComponent<AudioSource>();

        currentBGM = mainBGMs[Random.Range(0, 5)];
        //シーンが切り替わった時に呼ばれるメソッドを登録
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ScreenCapture.CaptureScreenshot("ScreenShot.png");
            // スクリーンショットを保存
        }
    }

    //シーンが切り替わった時に呼ばれるメソッド
    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        //シーンがどう変わったかで判定

        //メニューからメインへ
        if (beforeScene == "TitleScreen" && nextScene.name == "MainScene")
        {
            BGM_title.Stop();
            currentBGM = mainBGMs[Random.Range(0, 5)];
            audioSource.clip = currentBGM;
            audioSource.Play();
        }

        //メインからメニューへ
        if (beforeScene == "MainScene" && nextScene.name == "TitleScreen")
        {
            audioSource.Stop();
            BGM_result.Stop();
            //currentBGM.Stop();
            Invoke("TitleSE", 2.6f);
            Invoke("TitleBGM", 4f);
        }

        //メインからカスタムへ
        if (beforeScene == "MainScene" && nextScene.name == "Customize")
        {
            audioSource.Stop();
            BGM_result.Stop();
            BGM_customize.Play();
        }

        //メインでリスタート
        if (beforeScene == "MainScene" && nextScene.name == "MainScene")
        {
            audioSource.Stop();
            BGM_result.Stop();
            currentBGM = mainBGMs[Random.Range(0, 5)];
            audioSource.clip = currentBGM;
            audioSource.Play();
        }

        if (beforeScene == "TitleScreen" && nextScene.name == "Customize")
        {
            BGM_title.Stop();
            BGM_customize.Play();
        }

        //カスタムからメニューへ
        if (beforeScene == "Customize" && nextScene.name == "TitleScreen")
        {
            BGM_customize.Stop();
            Invoke("TitleSE", 2.6f);
            Invoke("TitleBGM", 4f);
        }

        //カスタムからメインへ
        if (beforeScene == "Customize" && nextScene.name == "MainScene")
        {
            BGM_customize.Stop();
            currentBGM = mainBGMs[Random.Range(0, 5)];
            audioSource.clip = currentBGM;
            audioSource.Play();
        }

        //遷移後のシーン名を「１つ前のシーン名」として保持
        beforeScene = nextScene.name;
    }

    private void OnDestroy()
    {
        // ※破棄時に、登録した実体の解除を行なっている

        // 自身がインスタンスなら登録を解除
        if (this == instance) _instance = null;
    }

    private void TitleBGM()
    {
        BGM_title.Play();
    }

    private void TitleSE()
    {
        titleSE.Play();
    }
    
}