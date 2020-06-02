using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//애로우의 왕복을 위한 collider 담당 스크립트
public class ArrowCtrl : MonoBehaviour
{

    public Vector3 ball;
    void Start()
    {
        ball = Vector3.back;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "forward")
            ball = Vector3.forward;
        else if (col.tag == "back")
            ball = Vector3.back;
    }
}
