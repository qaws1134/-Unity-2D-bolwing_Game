using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//핀 물리 작용과 스코어 카운트를 담당하는 스크립트
public class pinctrl : MonoBehaviour
{
    public Rigidbody2D pinrgd;
    bool getscore;
    Vector3 mousepos;
    
    void Start()
    {
        getscore = true;
        pinrgd = GetComponent<Rigidbody2D>();
       
    }

    

    private void OnCollisionEnter2D(Collision2D col)
    {
        mousepos = GameObject.Find("SpawnManager").GetComponent<spawnManager>().mouseInput;
        //공을 맞았을 때 반사각, 스코어
        if (col.collider.tag =="ball")
        {
            Vector3 ballpos = mousepos; //발사원점
            Vector3 reativePos = ballpos - col.transform.position;  //반사각 = 핀 위치 - 공발사위치
            reativePos *= -1;
            reativePos += new Vector3((float)Random.Range(-1f, 1f), (float)Random.Range(0, 1f), 0);
            //순간적 힘을 가함
            pinrgd.AddForce(reativePos,ForceMode2D.Impulse);  //반사각으로 힘을 가함
            if (getscore == true)
            {
                ScoreCtrl.Score += 1;
                getscore = false;
            }
        }
        //공에 맞은 핀에 맞았을 때 반사각, 스코어
        if(col.collider.tag=="pin")
        {
            Vector3 pinpos = col.gameObject.GetComponent<Transform>().position; //발사원점
            Vector3 reativePos = pinpos - col.transform.position;
            reativePos *= -1;
            reativePos += new Vector3((float)Random.Range(-1f, 1f), (float)Random.Range(0,1f),0);
            //순간적 힘을 가함
            pinrgd.AddForce(reativePos, ForceMode2D.Impulse);
            if (getscore == true)
            {
                ScoreCtrl.Score += 1;
                getscore = false;
            }
        }
   
    }
    //위로 날라간 핀 제거
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag=="end")
        {
            Destroy(gameObject);
        }
    }
    
}
