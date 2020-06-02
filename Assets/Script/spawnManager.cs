using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//마우스 클릭 위치에 공 생성, 현재 공이 없을 때 핀 생성을 담당하는 스크립트
public class spawnManager : MonoBehaviour
{

    public static bool summon = false;
    public GameObject ball;
    public GameObject pin;
    public GameObject[] pos; //pin 생성 위치 배열
    public Vector3 mouseInput;

    void Update()
    { 
        if (Input.GetMouseButtonDown(0))
        {
            //영역설정
            if (EventSystem.current.IsPointerOverGameObject())
            {
                if (!summon)
                {
                    if (ScoreCtrl.GameCount == 0)
                    { 
                        //핀 생성
                        for (int i = 0; i < pos.Length; i++)
                        {
                            Instantiate(pin, pos[i].transform.position, pos[i].transform.rotation);
                        }
                    }
                    //마우스 클릭 위치에 공 생성
                    GameObject obj;
                    mouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    obj = Instantiate(ball);
                    obj.transform.position = mouseInput;
                    obj.transform.position += Vector3.forward;
                    summon = true;
                }
            }
        }
    }
}
