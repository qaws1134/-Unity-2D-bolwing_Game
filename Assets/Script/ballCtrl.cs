using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//생성된 공의 방향, 힘을 컨트롤,공 제거 시 라운드 카운트와 종료시 남은 핀을 삭제 해줄 프리팹을 생성
public class ballCtrl : MonoBehaviour
{
    public GameObject ball;
    //애로우 조정 변수
    float ArrowSpeed; //애로우 속도
    Vector3 AroundMove;   //볼의 백터값을 가져올 변수
    Vector3 GoBall; //애로우 선택방향, 공이 나아갈 방향

    //애로우 관련 오브잭트
    public GameObject arrow;
    public GameObject forward;
    public GameObject back;

    //핀 삭제 리무버
    public GameObject Remover;
    Vector3 Removerpos; 

    int endScore = 0; //치고 난 뒤 스코어
    int initScore = 0; //초기 스코어
   
    int clickcount = 0; //클릭 카운트
    float power = 0; //파워 저장

    //파워게이지 UI
    public Image powerGage;
    public GameObject powerbackground; 

    void Start()
    {
        initScore = ScoreCtrl.Score;
        Removerpos = new Vector3(-4.2f, -1f,0);
        ArrowSpeed = 150f;
        powerGage.fillAmount= 0;
        arrow.SetActive(false);
    }
    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            clickcount++;     
        }
        //방향을 정할 애로우 생성 및 공 주변 회전
        if (clickcount ==1 )
        {
            AroundMove = arrow.GetComponent<ArrowCtrl>().ball;
            arrow.SetActive(true);
            arrow.transform.RotateAround(ball.transform.position, AroundMove, ArrowSpeed * Time.deltaTime);
        }
        // 파워게이지 생성와 애로우 방향값 저장
        if (clickcount == 2)
        {
            GoBall = arrow.transform.localRotation * Vector3.up;
            arrow.SetActive(false);
            forward.SetActive(false);
            back.SetActive(false);
            powerbackground.SetActive(true);
            powerGage.fillAmount += (float)1*Time.deltaTime;
            if(powerGage.fillAmount==1)
            {
                powerGage.fillAmount = 0;
            }
        }
        //선택한 파워와 방향으로 공을 보냄
        if(clickcount == 3)
        {
            power = powerGage.fillAmount * 10;
            powerbackground.SetActive(false);
            ball.transform.Translate(GoBall * Time.deltaTime* power);
        }
    }

    //거터 판단
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "wall")
        {
            GoBall = new Vector3(0,1,0);
            
        }
    }
    //공 제거
    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "end")
        {
            spawnManager.summon = false;
            Destroy(gameObject,2f);
            
        }
    }
    //공 제거 시 점수와 라운드 처리
    private void OnDestroy()
    {
        endScore = ScoreCtrl.Score;      
        ScoreCtrl.GameCount += 1;
        ScoreCtrl.singleEnd = true;

        //스트라이크나 라운드가2개 종료됐을 때 리무버 생성
        if ((endScore - initScore) == 10 || ScoreCtrl.GameCount == 2)
        {
            Instantiate(Remover, Removerpos, transform.rotation);
            ScoreCtrl.roundEnd = true;
            ScoreCtrl.GameCount = 0;
        }
    }

}
