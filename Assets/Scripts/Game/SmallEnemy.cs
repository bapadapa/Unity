using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmallEnemy : MonoBehaviour
{
    public enum Currentstate { idle, trace, attack, dead };
    public Currentstate curState = Currentstate.idle;

    private Transform _transform;
    private Transform playerTransform;
    private NavMeshAgent nvAgent;
    private Animator _animator;


    //추적 거리
    public float traceDist = 15.0f;
    //공격 거리
    public float attackDist = 3.2f;
    //사망 여부
    public bool isDead;



    // Start is called before the first frame update
    void Start()
    {
        _transform = this.gameObject.GetComponent<Transform>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        _animator = this.gameObject.GetComponent<Animator>();
        this.gameObject.tag = "smallMonster";


        isDead = false;

        //몹이 따라다니는 코드.
        //nvAgent.destination = playerTransform.position;
        StartCoroutine(this.CheckState());
        StartCoroutine(this.CheckStateForAction());


    }


    // Update is called once per frame
    void Update()
    {
      
    }

    // IEnumrator == GetEnumerator()함수를 구현하는데 사용..
    // GetEnumerator == 내부 데이터를 foreach 같은 것으로 열거할 수 있도록 해 준다는 것
    // 상태를 체크해줌.
    IEnumerator CheckState()
    {
        while (!isDead)
        {
            //yield == 루프 안에서 현재 상태를 기억하고, 값을 하나씩 반환시킴.
            yield return new WaitForSeconds(0.2f);

            float dist = Vector3.Distance(playerTransform.position, _transform.position);
            //attackDist 범위안에 Player가 있다면 공격.
            if (dist <= attackDist)
            {
                curState = Currentstate.attack;
            }
            //traceDist 범위안에 Player가 있다면 추적.
            else if (dist <= traceDist)
            {
                curState = Currentstate.trace;
            }
            else if(isDead)
            {
                Debug.Log(isDead);
                curState = Currentstate.dead;
               
            }
            //Player가 주변에 없을때 대기상태.
            else
            {
                curState = Currentstate.idle;
            }
        }
      
    }

    //상태에 따른 Action구현.
    IEnumerator CheckStateForAction()
    {
        while (!isDead)
        {
            switch (curState)
            {
                case Currentstate.idle:
                    //nvAgent.Stop();
                    _animator.SetBool("isTrace", false);
                    break;
                case Currentstate.trace:
                    nvAgent.SetDestination(playerTransform.position);
                    //nvAgent.destination = playerTransform.position;
                    //nvAgent.Resume();
                    _animator.SetBool("isTrace", true);
                    break;
                case Currentstate.attack:
                    _animator.SetBool("isAttack", true);
                    break;
                case Currentstate.dead:
                    //Destroy(this.gameObject);
                    _animator.SetBool("isDie", true);
                    _animator.SetBool("isTrace", false);
                    Destroy(this.gameObject,1.4f);  
                    break;

            }
            _animator.SetBool("isAttack", false);
            yield return null;

            

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("weapon"))
        {
            curState = Currentstate.dead;
        }
    }

}

