using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertWindow : MonoBehaviour
{
    //カスタム画面のアラートウィンドウを閉じる
    public void CloseAlertWindow()
    {
        this.gameObject.SetActive(false);
    }

}
