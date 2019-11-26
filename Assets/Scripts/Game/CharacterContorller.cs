using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterContorller : MonoBehaviour
{
    Rigidbody rigid;
    Animator animator;


    public int MoveSpeed;
    public int jumpPower = 10;
    public int rotationSpeed = 10;



    float horizontalMove;
    float verticalMove;
    Vector3 movement;

    public int jumpCnt; //아이템 먹으면 다중점프하게 만들려고 함.
    private bool IsJumping;

    private bool isAttacking;
    private bool isDefencing;



    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        IsJumping = false;
        isAttacking = false;
        isDefencing = false;

        MoveSpeed = 10;


        jumpCnt = 2;

    }

    // 키입력
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump"))
            IsJumping = true;
        if (Input.GetMouseButtonDown(0)) //좌클릭시
            isAttacking = true;
        if (Input.GetMouseButtonDown(1)) //우클릭시
            isDefencing = true;


    }
    //물리적 처리
    void FixedUpdate()
    {
        Move();
        Jump();
        AnimationUpdate();
        AttackNDefence();
    }



    //이동
    public void Move()
    {
        movement.Set(horizontalMove, 0, verticalMove);
        movement = movement.normalized * MoveSpeed * Time.deltaTime;
        rigid.MovePosition(transform.position + movement);
        
        Turn();
    }

    //점프
    public void Jump()
    {

        if (IsJumping && jumpCnt > 0)
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            IsJumping = false;
            jumpCnt -= 1;
        }
        else
            return;
    }

    void AnimationUpdate()
    {
        //멈추면
        if (horizontalMove == 0 && verticalMove == 0)
        {
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isRunning", true);
        }

        if (isAttacking)
        {
            animator.SetBool("isAttacking", true);
            isAttacking = false;

        }
        else
        {
            animator.SetBool("isAttacking", false);            
        }

        if (isDefencing)
        {
            MoveSpeed = 1;
            animator.SetBool("isDefencing", true);
            isDefencing = false;
        }
        else
        {
            MoveSpeed = 10;
            animator.SetBool("isDefencing", false);
            
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

    void AttackNDefence()
    {
        


    }




    private void OnCollisionEnter(Collision collision)
    {
        //다중점프 방지.
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCnt = 2;
        }

    }

}
