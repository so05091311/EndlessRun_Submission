using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
	public int MAX_JUMP_COUNT = 2;   // ジャンプできる回数
	private float force = 18.0f;

	public int jumpCount = 0;

	public float jumpTime;
	private float jumpTimeCounter;

	public bool isGrounded;
	public LayerMask whatIsGround;
	public Transform groundCheck;
	public float groundCheckRadius;

	bool isJumpPush;
	bool isFall;

	Rigidbody2D rb;
	Animator animator;

	PlayerController playerController;

	private void Start()
	{
		MAX_JUMP_COUNT = PlayerPrefs.GetInt("MaxJumpCount", 2);

		playerController = this.GetComponent<PlayerController>();

		rb = this.GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		jumpTimeCounter = jumpTime;
	}

	void Update()
	{
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

		// ジャンプMAX以下でクリックジャンプ　エディター上
		//if (jumpCount < MAX_JUMP_COUNT && (isJumpPush == true || Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)))
		if (jumpCount < MAX_JUMP_COUNT && (isJumpPush == true))
		{
			if (jumpTimeCounter > 0)
			{
				rb.velocity = new Vector2(rb.velocity.x, force);
				jumpTimeCounter -= Time.deltaTime;
			}
		}

		//接地中にフラグ変更
		this.animator.SetBool("isGround", isGrounded);

		//接地時
		if (isGrounded)
		{
			isFall = false;
			jumpCount = 0;
			jumpTimeCounter = jumpTime;
			if (Input.GetKeyDown(KeyCode.X))
			{
				animator.Play("RunningAttack2");
			}
		}
		else  //空中時
		{
			if (rb.velocity.y < 0)
			{
				isFall = true;
				this.animator.SetBool("isFall", isFall);
			}

			if (Input.GetKeyDown(KeyCode.X))
			{
				animator.Play("JumpingAttack");
			}
		}
	}

	//スマホでアタックボタンが押下時
	public void ButtonAttack()
	{
		if(playerController.playerMiss == false)
		{
			if (isGrounded)
			{
				animator.Play("RunningAttack2");
			}
			else
			{
				animator.Play("JumpingAttack");
			}
		}
	}

	//スマホでジャンプボタンが押下時
	public void PushJumpButton()
	{
		if (playerController.playerMiss == false)
		{
			isJumpPush = true;
		}
	}

	//ジャンプボタンがリリース時
	public void LeaveJumpButton()
	{
		isJumpPush = false;
		jumpTimeCounter = jumpTime;
		jumpCount++;
	}
}
