using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject poseMenu;

    //ポーズ時メソッド
    public void DisplayPoseMenu()
    {
        poseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    //ポーズ解除時メソッド
    public void ClosePoseMenu()
    {
        poseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    
}
