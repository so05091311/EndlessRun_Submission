using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public GameObject transitionPanel;
    public UnityEvent transitionEvent;

    //　非同期動作で使用するAsyncOperation
    private AsyncOperation async;
    //　読み込み率を表示するスライダー
    [SerializeField]
    private Slider slider;

    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void TitleToMain()
    {
        transitionPanel.SetActive(true);
        transitionEvent.Invoke();
        //　コルーチンを開始
        StartCoroutine("LoadMainScene");
    }

    public void MainToMain()
    {
        gameManager.sceneChanging = true;

        transitionPanel.SetActive(true);
        transitionEvent.Invoke();
        //　コルーチンを開始
        StartCoroutine("LoadMainScene");
    }

    public void MainToCustom()
    {
        gameManager.sceneChanging = true;

        transitionPanel.SetActive(true);
        transitionEvent.Invoke();
        //　コルーチンを開始
        StartCoroutine("LoadCustumScene");
    }

    public void MainToTitle()
    {
        gameManager.sceneChanging = true;

        Time.timeScale = 1;
        transitionPanel.SetActive(true);
        transitionEvent.Invoke();

        StartCoroutine("LoadTitle");
    }

    public void Restart()
    {
        gameManager.sceneChanging = true;

        Time.timeScale = 1;
        transitionPanel.SetActive(true);
        transitionEvent.Invoke();

        StartCoroutine("LoadMainScene");
    }

    public void TitleToCustom()
    {
        transitionPanel.SetActive(true);
        transitionEvent.Invoke();

        StartCoroutine("LoadCustumScene");
    }

    public void CustomToTitle()
    {
        transitionPanel.SetActive(true);
        transitionEvent.Invoke();

        StartCoroutine("LoadTitle");
    }

    public void CustomToMain()
    {
        transitionPanel.SetActive(true);
        transitionEvent.Invoke();
        //　コルーチンを開始
        StartCoroutine("LoadMainScene");
    }


    IEnumerator LoadTitle()
    {
        yield return new WaitForSeconds(4);
        // シーンの読み込みをする
        async = SceneManager.LoadSceneAsync("TitleScreen");

        //　読み込みが終わるまで進捗状況をスライダーの値に反映させる
        while (!async.isDone)
        {
            var progressVal = Mathf.Clamp01(async.progress / 0.9f);
            slider.value = progressVal;
            yield return null;
        }
    }

    IEnumerator LoadMainScene()
    {
        yield return new WaitForSeconds(4);
        // シーンの読み込みをする
        async = SceneManager.LoadSceneAsync("MainScene");

        //　読み込みが終わるまで進捗状況をスライダーの値に反映させる
        while (!async.isDone)
        {
            var progressVal = Mathf.Clamp01(async.progress / 0.9f);
            slider.value = progressVal;
            yield return null;
        }
    }

    IEnumerator LoadCustumScene()
    {
        yield return new WaitForSeconds(4);
        // シーンの読み込みをする
        async = SceneManager.LoadSceneAsync("Customize");

        //　読み込みが終わるまで進捗状況をスライダーの値に反映させる
        while (!async.isDone)
        {
            var progressVal = Mathf.Clamp01(async.progress / 0.9f);
            slider.value = progressVal;
            yield return null;
        }
    }
}
