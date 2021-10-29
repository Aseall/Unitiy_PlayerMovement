using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private	KeyCode				jumpKeyCode = KeyCode.Space;
	[SerializeField]
	private	CameraController	cameraController;
	private	Movement3D			movement3D;

	public bool IsRun;

	private void Awake()
	{
		movement3D = GetComponent<Movement3D>();

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void Update()
	{
		float x = Input.GetAxis("Horizontal");	// 방향키 좌/우 움직임
		float z = Input.GetAxis("Vertical");     // 방향키 위/아래 움직임
		// Camera 시선과 Player 이동 방향 일치 및 Player 정면 회전 독립
		movement3D.MatchSightToCamera(x, z);
		// x, z축 방향으로 이동
		if (Input.GetKey(KeyCode.LeftShift))
		{
			IsRun = true;
		}
		else
		{
			IsRun = false;
		}
		movement3D.MoveTo(new Vector3(x, 0, z), IsRun);

		// y축 방향으로 뛰어오름
		if (Input.GetKey(jumpKeyCode))
		{
			movement3D.JumpTo();
		}

		cameraController.Zoom();

		// 카메라 x, y축 회전
		float mouseX = Input.GetAxis("Mouse X");	// 마우스 좌/우 움직임
		float mouseY = Input.GetAxis("Mouse Y");	// 마우스 위/아래 움직임

		cameraController.RotateTo(mouseX, mouseY);
	}
}

