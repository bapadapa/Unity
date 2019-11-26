using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;

    public float offsetX = 0f;
    public float offsetY = 5f;
    public float offsetZ = -10f;
    public float followSpeed = 100f;   
    Vector3 cameraPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //다른 Update문을 실행하고 마지막에 실행되는 Update사이클
    void LateUpdate()
    {
        cameraPosition.x = player.transform.position.x + offsetX;
        cameraPosition.y = player.transform.position.y + offsetY;
        cameraPosition.z = player.transform.position.z + offsetZ;


        transform.position = cameraPosition;
        //위 함수를 조금 더 부드럽게 만들기.
        //transform.position = Vector3.Lerp(transform.position, cameraPosition, followSpeed * Time.deltaTime);
    }
}
