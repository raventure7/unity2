using UnityEngine;
using System.Collections;

public class NeJikoController : MonoBehaviour {

    const int MinLane = -2;
    const int MaxLane = 2;
    const float LaneWidth = 1.0f;
    const int DefaultLife = 3;
    const float StunDuration = 0.5f;
    

    CharacterController controller;
    Animator animator;

    Vector3 moveDirection = Vector3.zero;
    int targetLane;

    int life = DefaultLife;
    float recoverTime = 0.0f;


    public float gravity;
    public float speedZ;
    public float speedX;

    public float speedJump;
    public float accelerationZ;

    public int Life()
    {
        return life;
    }
    public bool IsStan()
    {
        return recoverTime > 0.0f || life <= 0;
    }
	// Use this for initialization
	void Start () {
        //컴포넌트 취득
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("left")) MoveToLeft();
        if (Input.GetKeyDown("right")) MoveToRight();
        if (Input.GetKeyDown("space")) Jump();

        if(IsStan())
        {
            moveDirection.x = 0.0f;
            moveDirection.y = 0.0f;
            recoverTime -= Time.deltaTime;
        }
        else
        {
            //서서히 가속하여 Z 방향으로 전진시킨다.
            float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
            moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);

            // X 방향은 목표의 포지션 까지 차등 비율로 속도를 계산
            float rationX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
            moveDirection.x = rationX * speedX;
        }



        // 중력만큼 힘을 매 프레임에 추가.
        moveDirection.y -= gravity * Time.deltaTime;

        //이동 실행
        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        controller.Move(globalDirection * Time.deltaTime);
        //이동 후 접지하면 Y 방향 속도 리셋
        if (controller.isGrounded) moveDirection.y = 0;
        // 속도가 0 이상이면 달리고 있는 플래그를 TRUE
        animator.SetBool("run", moveDirection.z > 0.0f);
    }
    // 왼쪽으로
    public void MoveToLeft()
    {
        if (IsStan()) return;
        if (controller.isGrounded && targetLane > MinLane) targetLane--;
    }
    // 오른쪽으로
    public void MoveToRight()
    {
        if (IsStan()) return;
        if (controller.isGrounded && targetLane < MaxLane) targetLane++;
    }
    // 점프
    public void Jump()
    {
        if (IsStan()) return;
        if (controller.isGrounded)
        {
            moveDirection.y = speedJump;
            animator.SetTrigger("jump");
        }
    }

    void OnControllerColliderHit (ControllerColliderHit hit)
    {
        if (IsStan()) return;
        if(hit.gameObject.tag == "Robo")
        {
            life--;
            recoverTime = StunDuration;

            animator.SetTrigger("damage");

            Destroy(hit.gameObject);
        }
    }
}
