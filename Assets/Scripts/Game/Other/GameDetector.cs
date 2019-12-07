using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameDetector : MonoBehaviour
{
    public PlayerInfo playerInfo;
    

    // Start is called before the first frame update
    void Start()
    {
        playerInfo = FindObjectOfType<PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInfo.current_HP < 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("EndScenes");
        }
    }
}
