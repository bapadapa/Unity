using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerInfo : MonoBehaviour
{
    public GameObject[] Effect;
    public Image[] barImage;

    public float Max_HP;
    public float Max_MP;
    public float current_HP;
    public float current_MP;

    public Text hpText;
    public Text mpText;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UIUpdate(string _Type, string _InfoType, float _Value)
    {
        float Type = 0; // 현재 수치
        float MAXType = 0; //최대 수치
        int Index = 0;  //MP or HP

        switch (_InfoType)
        {
            case "HP":
                {
                    Index = 0;
                    Type = current_HP;
                    MAXType = Max_HP;

                    if (_Type == "Recover")
                        current_HP += _Value;
                    else
                        current_HP -= _Value;
                    break;
                }
            case "MP":
                {
                    Index = 1;
                    Type = current_MP;
                    MAXType = Max_MP;

                    if (_Type == "Recover")
                        current_MP += _Value;
                    else
                        current_MP -= _Value;
                    break;
                }
        }
        barImage[Index].fillAmount = Type / MAXType;
        hpText.text = string.Format((int)current_HP + " / " + Max_HP);
        mpText.text = string.Format((int)current_MP + " / " + Max_MP);


    }
}
