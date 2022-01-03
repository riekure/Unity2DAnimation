using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Animator))]
public class PlayerController : MonoBehaviour {

	[SerializeField] private Rigidbody2D rb = null;
	[SerializeField] private Animator animator = null;
	[SerializeField] private LayerMask platformLayer = 1;

	// 移動速度(地上)
	[SerializeField, Range (0f, 40f)] private float moveSpeed = 30f;

	// 移動速度(空中)
	[SerializeField, Range (0f, 20f)] private float jumpMoveSpeed = 20f;

	// ジャンプ力
	[SerializeField, Range (0f, 100f)] private float jumpPower = 100f;

	// 重力倍率
	[SerializeField, Range (0f, 10f)] private float gravityRate = 5f;

	// ジャンプの減衰力
	[SerializeField, Range (0f, 10f)] private float jumpDampingPower = 6f;

	private Vector2 localScale = default;
	private bool isGrounded = true;
	private bool isJumping = false;
	private bool isJumpingCheck = true;
	private float _jumpPower = 0f;

	enum State {
		IDLE,
		RUN,
		JUMP,
		FALL,
	}

	State state = State.IDLE;

	void Awake () {
		this.localScale = this.transform.localScale;
	}

	void Start () {
		if (this.rb == null) {
			this.rb = GetComponent<Rigidbody2D> ();
		}
		if (this.animator == null) {
			this.animator = GetComponent<Animator> ();
		}
	}

	private float moveKey = 0;
	private int jumpKey = 0;

	void Update () {
		// 入力を取得
		this.GetInputKey ();
		// 着地判定
		this.CheckGround ();
		// Animatorのパラメーターを変更する
		this.ChangeAnimation ();
	}

	void GetInputKey () {
		// 移動
		this.moveKey = Input.GetAxisRaw ("Horizontal");
		// ジャンプ
		if (Input.GetButtonDown ("Jump")) {
			this.jumpKey = 1;
		} else if (Input.GetButton ("Jump")) {
			this.jumpKey = 2;
		} else if (Input.GetButtonUp ("Jump")) {
			this.jumpKey = 0;
		}
	}

	void CheckGround () {
		this.isGrounded = Physics2D.Linecast (this.transform.position - this.transform.up * 0.4f, this.transform.position - this.transform.up * 0.6f, this.platformLayer);
	}

	void FixedUpdate () {

		if (this.moveKey != 0) {
			// 向きを変える
			this.localScale.x = this.moveKey;
			this.transform.localScale = this.localScale;
		}

		if (this.isGrounded) {
			this.rb.velocity = new Vector2 (this.moveKey * this.moveSpeed, this.rb.velocity.y);
			if (this.isJumpingCheck && this.jumpKey != 0) {
				this.isJumpingCheck = false;
				this.isJumping = true;
				this._jumpPower = this.jumpPower;
			}
		} else {
			if (this.isJumpingCheck && this.jumpKey == 0) {
				this.isJumping = false;
			}
			if (!this.isJumping) {
				this.rb.velocity = new Vector2 (this.moveKey * this.jumpMoveSpeed, Physics.gravity.y * this.gravityRate);
			}
		}

		if (this.isJumping) {
			if (this.jumpKey == 2) {
				this._jumpPower -= this.jumpDampingPower;
				this.rb.velocity = new Vector2 (this.moveKey * this.jumpMoveSpeed, this._jumpPower);
			}
			if (this._jumpPower < 0) {
				this.isJumping = false;
			}
		}

		if (this.jumpKey == 0) {
			isJumpingCheck = true;
		}
	}

	void ChangeAnimation () {
		if (this.isJumping) {
			SetState (State.JUMP);
		} else {
			if (this.isGrounded) {
				if (this.moveKey != 0) {
					SetState (State.RUN);
				} else {
					SetState (State.IDLE);
				}
			} else {
				SetState (State.FALL);
			}
		}
	}

	void SetState (State newState) {
		var oldState = this.state;
		this.state = newState;
		if (this.state != oldState) {
			SetParameters ();
		}
	}

	void SetParameters () {
		this.animator.SetBool ("isIdle", this.state == State.IDLE);
		this.animator.SetBool ("isFall", this.state == State.FALL);
		this.animator.SetBool ("isRun", this.state == State.RUN);
		this.animator.SetBool ("isJump", this.state == State.JUMP);
	}
}