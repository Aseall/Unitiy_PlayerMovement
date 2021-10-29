using System.Collections;
using UnityEngine;

public class Movement3D : MonoBehaviour
{
	public bool IsCanJump;
	[SerializeField]
	private Transform playerTransform;

	public float Speed = 5f;
	public float RunSpeed = 10f;

	[SerializeField]
	private	float		FinalSpeed = 0f;	// 이동 속도
	[SerializeField]
	private	float		gravity = -9.81f;	// 중력 계수
	[SerializeField]
	private	float		jumpForce = 1.5f;	// 뛰어 오르는 힘
	private	Vector3		moveDirection;		// 이동 방향

	[SerializeField]
	private	Transform			cameraTransform;
	private	CharacterController	characterController;

	private void Awake()
	{
		characterController = GetComponent<CharacterController>();
	}

	private void Update()
	{
		// 플레이어가 땅을 밟고 있지 않다면
		// y축 이동방향에 gravity * Time.deltaTime을 더해준다
		if ( characterController.isGrounded == false )
		{
			moveDirection.y += gravity * Time.deltaTime;
		}
		characterController.Move(moveDirection * FinalSpeed * Time.deltaTime);
	}

	public void MoveTo(Vector3 direction, bool isRun)
	{
		FinalSpeed = (isRun) ? RunSpeed : Speed;
		// 카메라가 바라보고 있는 방향을 기준으로 방향키에 따라 이동할 수 있도록 함
		Vector3 movedis = (cameraTransform.rotation * direction).normalized;
		moveDirection	= new Vector3(movedis.x, moveDirection.y, movedis.z);
	}

	public bool JumpTo(bool isJumppress)
	{
		bool IsFallen = false;
		if(!IsFallen)
		{
			if(isJumppress)
			{
				if ( characterController.isGrounded == true )
				{
					IsCanJump = true;
					StartCoroutine(CountJumpDelay());
				}
				if (IsCanJump)
				{
					moveDirection.y = jumpForce;
				}
			}
			if(moveDirection.y < 1)
			{
				IsFallen = true;
			}
		}
		return IsFallen;
	}

	public void MatchSightToCamera(float x, float z)
    {
		Vector3 forward = new Vector3(cameraTransform.forward.x, 0f, cameraTransform.forward.z);
		Vector3 right = new Vector3(cameraTransform.right.x, 0f, cameraTransform.right.z);
		Vector3 moveDirection = forward * z + right * x;
		if (moveDirection != Vector3.zero)
		{
			transform.forward = moveDirection;
		}
	}

	IEnumerator CountJumpDelay()
	{
		yield return new WaitForSecondsRealtime(0.5f);
		IsCanJump = false;
	}
}

