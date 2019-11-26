using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterContorller : MonoBehaviour
{
    Rigidbody rigid;
    Animator animator;


    public int MoveSpeed = 10;
    public int jumpPower = 10;
    public int rotationSpeed = 10;

    float horizontalMove;
    float verticalMove;
    Vector3 movement;

    public int jumpCnt;

    private bool IsJumping;



    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        IsJumping = false;

        jumpCnt = 1;

    }

    // 키입력
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump"))
            IsJumping = true;



    }
    //물리적 처리
    void FixedUpdate()
    {
        Move();
        Jump();

    }



    //이동
    public void Move()
    {
        movement.Set(horizontalMove, 0, verticalMove);
        movement = movement.normalized * MoveSpeed * Time.deltaTime;
        rigid.MovePosition(transform.position + movement);
        Turn();
        AnimationUpdate();
    }

    //점프
    public void Jump()
    {

        if (IsJumping && jumpCnt-- > 0)
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            IsJumping = false;
        }
        else
            return;

    }

    void AnimationUpdate()
    {
        if (horizontalMove == 0 && verticalMove == 0)
        {
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isRunning", true);
        }
    }

    void Turn()
    {
        //이 조건문을 이용하여 Vector값이 0으로 바뀌지 않게 함.
        if (horizontalMove == 0 && verticalMove == 0)
            return;

        Quaternion newRotation = Quaternion.LookRotation(movement);
        //너무 휙휙 바뀌어서 다른걸사용.
        //rigid.MoveRotation(newRotation); 

        rigid.rotation = Quaternion.Slerp(rigid.rotation, newRotation, rotationSpeed * Time.deltaTime);


    }




    private void OnCollisionEnter(Collision collision)
    {
        //다중점프 방지.
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCnt = 1 ;
        }

    }

}
