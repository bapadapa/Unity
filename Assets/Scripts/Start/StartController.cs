using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartController : MonoBehaviour {



    // Use this for initialization
    void Start () {
}
	
	// Update is called once per frame
	void Update () {

    }

    public void ChangeToGameScene()
    {
        //LoadScene에 에러가 나서 이렇게 함.
        Debug.Log("Start");
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScenes");
        
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
