using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    //public GameObject player;

    //public float offsetX = 0f;
    //public float offsetY = 5f;
    //public float offsetZ = -10f;
    //public float followSpeed = 100f;   
    //Vector3 cameraPosition;

    public Transform targetTransform; // Player의 Transform 컴포넌트.
    public float dist = 3.0f;  //거리
    public float height = 5.0f;  //높이
    public float dampTrace = 20.0f;  //추적 속도.

    private Transform _transform; // Camera Transform 컴포넌트

	// Use this for initialization
	void Start () {
        _transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //다른 Update문을 실행하고 마지막에 실행되는 Update사이클
    void LateUpdate()
    {
        //cameraPosition.x = player.transform.position.x + offsetX;
        //cameraPosition.y = player.transform.position.y + offsetY;
        //cameraPosition.z = player.transform.position.z + offsetZ;


        //transform.position = cameraPosition;
        //위 함수를 조금 더 부드럽게 만들기.
        //transform.position = Vector3.Lerp(transform.position, cameraPosition, followSpeed * Time.deltaTime);

        _transform.position = Vector3.Lerp(_transform.position, targetTransform.position - (targetTransform.forward * dist) + (Vector3.up * height), Time.deltaTime * dampTrace);
        
        _transform.LookAt(targetTransform.position);
    }
}
