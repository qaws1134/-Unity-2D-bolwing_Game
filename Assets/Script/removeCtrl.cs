using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//라운드 종료시 핀을 삭제하기 위한 Remover을 동작하기 위한 스크립트
public class removeCtrl : MonoBehaviour
{
    int initscore = 0; // 삭제 시 핀끼리 부딪히면 스코어가 카운트 되기 때문에 핀을 삭제하기 전 현재 스코어를 저장 

    void Start()
    {
        initscore = ScoreCtrl.Score;
    }

    private void Update()
    {
        this.gameObject.transform.position += (Vector3.up*Time.deltaTime*2f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "end")
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        ScoreCtrl.Score = initscore;
    }
}
