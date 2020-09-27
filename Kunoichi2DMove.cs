using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Kunoichi2DMove : MonoBehaviour
{
    
    Animator animator;

    string clipName;
    AnimatorClipInfo[] clipInfo;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //アニメーションランダム取得
        clipInfo = animator.GetCurrentAnimatorClipInfo(0);
    }

    //キャラクタータッチ時メソッド
    public void onClickBust()
    {
        //立ち状態時
        if(clipInfo[0].clip.name == "Idle" || clipInfo[0].clip.name == "Idle1" || clipInfo[0].clip.name == "IdleMove1")
        {
            animator.Play("IdleToSurprise");
        }

        //腕組時
        if (clipInfo[0].clip.name == "CrossIdle" || clipInfo[0].clip.name == "CrossIdle1" || clipInfo[0].clip.name == "CrossMove")
        {
            animator.Play("CrossToSurprise");
        }

    }
}
