using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    private void CameraLook()
    {
        // mouseDelta.y는 마우스의 y축 변화량을 의미, 이 값에 lookSensitivity를 곱해 회전 속도 조절
        camCurXRot += mouseDelta.y * lookSensitivity;

        // Mathf.Clamp 함수는 첫 번째 인자가 두 번째와 세번쨰 사이에 위치하도록 함. camCurXRot 값을 제한
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);

        // cameraContainer의 오일러 각 설정 새로운 Vector3를 만들어서 X축 회전을 -camCurXRot로 설정
        // 마우스 움직임이 위로가면 음수 값을 반환해서 x축 회전에 음수를 취함
        // = 카메라의 상하 회전만을 담당하도록 함
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        // 오브젝트 자체의 y축 회전을 처리. 마우스 x축 변화량에 lookSensitivity를 곱해서 오브젝트의 y축 오일러 각도에 더해줌
        // = 마우스 움직임에 따라서 플레이어가 좌우로 회전한다.
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    private void Move()
    {
        // dir은 이동 방향을 저장하는 Vector3 타입의 벡터. transform.forward는 오브젝트 앞쪽 방향벡터를 반환, transform.right는 오브젝트 오른쪽 방향 벡터 반환
        // curMovementInput은 사용자의 입력을 나타내는 벡터 y축은 앞뒤 움직임, x축은 좌우 움직임을 나타냄.
        // = 두 벡터를 입력값에 맞춰 곱한 후 더해 이동 방향 결정 후 moveSpeed를 곱해서 이동속도 설정
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;

        // dir의 y축값을 현재 rigidbody의 y축 속도로 설정 . . 왜?
        // 중력같은 물리적 요인에 y축 속도가 변하지 않도록 하기 위함. ex) 점프 중 이동할 때 y축 속도가 고정되게
        dir.y = rb.velocity.y;

        // rigidbody의 속도를 dir로 설정한다.
        // = 물리적으로 오브젝트를 움직이게 된다.
        rb.velocity = dir;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
       // Performed는 입력이 완료된 상태를 의미 ( 유지 )
       if(context.phase == InputActionPhase.Performed)
       {
            curMovementInput = context.ReadValue<Vector2>();
       }
       // Canceled는 입력이 중지된 상태를 의미 즉, 입력이 취소되었을 때 Vector2.zero로 인해서 이동이 멈추게 됨
       else if(context.phase == InputActionPhase.Canceled)
       {
            curMovementInput = Vector2.zero;
       }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // Started는 입력이 시작된 상태를 의미 즉, 점프 버튼을 눌렀을 때 호출
        if(context.phase == InputActionPhase.Started && IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        // 네 모서리에서 아래쪽으로 발사하는 네 개의 Ray를 배열로 생성
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),  // 오브젝트의 앞쪽 + 약간 위에서 아래로
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down), // 오브젝트의 뒤쪽 + 약간 위에서 아래로
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),    // 오브젝트의 오른쪽 + 약간 위에서 아래로
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)     // 오브젝트의 왼쪽 + 약간 위에서 아래로
        };

        for (int i = 0; i < rays.Length; i++)
        {
            // 각 Ray가 길이 0.1f 이내에서 바닥에 닿았는지 검사, groudLayerMask는 오브젝트가 지면으로 인식할 레이어 지정, 지면에 닿으면 true반환
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }
}