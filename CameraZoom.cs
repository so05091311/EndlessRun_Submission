using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraZoom : MonoBehaviour
{
    private Camera cam;
    //カメラズーム先ターゲット
    public GameObject moveTarget;
    float distXPos;
    float distYPos;

    //ズーム用変数
    float zoom = 1.7f;
    float waitTime = 0.5f;
    float defaultFov;

    //ズーム用ボタンオブジェクト
    public GameObject zoomInButton;
    public GameObject zoomOutButton;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        defaultFov = cam.orthographicSize;

        distXPos = moveTarget.transform.position.x;
        distYPos = moveTarget.transform.position.y;
    }

    public void ZoomIn()
    {
        //DoTweenでカメラのfovを動かす
        DOTween.To(() => cam.orthographicSize, fov => cam.orthographicSize = fov, defaultFov / zoom, waitTime);
        //DOMoveでカメラ位置も移動
        this.gameObject.transform.DOMove(new Vector3(distXPos, distYPos, -10), waitTime);


        //ズームボタン切り替え
        zoomInButton.SetActive(false);
        zoomOutButton.SetActive(true);
    }

    public void ZoomOut()
    {
        //DoTweenでカメラのfovを戻す
        DOTween.To(() => cam.orthographicSize, fov => cam.orthographicSize = fov, defaultFov, waitTime);
        //DOMoveでカメラ位置を戻す
        this.gameObject.transform.DOMove(new Vector3(0, 0, -10), waitTime);

        //ズームボタン切り替え
        zoomInButton.SetActive(true);
        zoomOutButton.SetActive(false);
    }
}
