﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterContorller : MonoBehaviour
{
    Rigidbody rigid;
    Animator animator;
    AnimatorClipInfo[] currentClipInfo;  

    public int MoveSpeed;
    public int jumpPower = 10;
    public int rotationSpeed = 10;

    public float playerHP;
    public float playerMP;

    public MeshCollider sword;


    float horizontalMove;
    float verticalMove;
    Vector3 movement;

    public int jumpCnt; //아이템 먹으면 다중점프하게 만들려고 함.
    public bool IsJumping;

    public bool isAttacking;
    public bool isDefencing; 

    public PlayerInfo playerInfo;

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

        playerInfo = FindObjectOfType<PlayerInfo>();
        sword.GetComponent<MeshCollider>();
        currentClipInfo = this.animator.GetCurrentAnimatorClipInfo(0);
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

        if (Input.GetKeyDown(KeyCode.LeftShift)) //쉬프트 클릭시
            isDefencing = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            isDefencing = false;
    }
    //물리적 처리
    void FixedUpdate()
    {
        Move();
        Jump();
        AnimationUpdate();      
    }



    //이동
    public void Move()
    {
        movement.Set(horizontalMove, 0, verticalMove);
        movement = movement.normalized * MoveSpeed * Time.deltaTime;
        rigid.MovePosition(transform.position + movement);
        Turn();
    }


    void AnimationUpdate()
    {
        //멈추면
        if (horizontalMove == 0 && verticalMove == 0)
        {
            animator.SetBool("isRunning", false);
            //멈추면 MP 대폭 회복.
            if (playerInfo.current_MP < playerInfo.Max_MP)
                playerInfo.UIUpdate("Recover", "MP", 0.5f);
            if (playerInfo.current_HP < playerInfo.Max_HP)
                playerInfo.UIUpdate("Recover", "HP", 0.2f);
        }
        else
        {
            animator.SetBool("isRunning", true);
            //움직이면 MP 소폭 회복.
            if (playerInfo.current_MP < playerInfo.Max_MP)
                playerInfo.UIUpdate("Recover", "MP", 0.2f);
            if (playerInfo.current_HP < playerInfo.Max_HP)
                playerInfo.UIUpdate("Recover", "HP", 0.1f);
        }

        //공격 애니메이션 작동시에만 트리거 활성화
        if (Animator.StringToHash("Base Layer.Attack") == animator.GetCurrentAnimatorStateInfo(0).nameHash)
        {
            sword.isTrigger = true;
        }
        else
            sword.isTrigger = false;

        if (isAttacking)
        {
            if (playerInfo.current_MP >= 10)
            {
                //떄릴때마다 마나 10씩 소모
                playerInfo.UIUpdate("Damage", "MP", 10f);
                animator.SetBool("isAttacking", true);
            }
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
    //점프
    public void Jump()
    {

        if (IsJumping && jumpCnt > 0)
        {
            animator.SetBool("isJumping", true);
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            IsJumping = false;
            jumpCnt -= 1;
            animator.SetBool("isJumping", false);
        }
        else
            return;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //다중점프 방지.
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCnt = 2;
        }
        if (collision.gameObject.CompareTag("End"))
        {
            Debug.Log("End");
            UnityEngine.SceneManagement.SceneManager.LoadScene("EndScenes");

        }
    }
  
}
