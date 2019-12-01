using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMonsterGenerator : MonoBehaviour
{

    public GameObject FieryPrefab;
    public GameObject CyclopesPrefab;
    float span = 1.0f;
    float delta = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;
            // Instantiate(applePrefab);
            int randNum = Random.Range(0, 10);
            GameObject smallMonster;
            if (randNum % 2 == 0)
            {
                smallMonster = Instantiate(FieryPrefab) as GameObject;
            }
            else
            {
                smallMonster = Instantiate(CyclopesPrefab) as GameObject;

            }
            smallMonster.gameObject.tag = "smallMonster";
            float x = Random.Range(250, 270);
            float z = Random.Range(250, 270);
            smallMonster.transform.position = new Vector3(x, 0, z);
        }
    }
}
