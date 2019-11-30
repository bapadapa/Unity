using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmallEnemy : MonoBehaviour
{

    private Transform _transform;
    private Transform playerTransform;
    private NavMeshAgent nvAgent;

    // Start is called before the first frame update
    void Start()
    {
        _transform = this.gameObject.GetComponent<Transform>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        nvAgent.destination = playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {


    }
}
