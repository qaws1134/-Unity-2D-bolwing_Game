using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//전체 게임 스코어 처리, UI 표시를 담당하는 스크립트
public class ScoreCtrl : MonoBehaviour
{
    //볼륨 잼
    public Text scoreText;
    public Image small;
    public Image mid;
    public Image large;
    public Image large2;

    
    public Text[] game; //토탈점수
    public Text[] singlegame; //라운드 점수

    public static int Score= 0;
    public static int GameCount = 0; //한라운드에 2번 게임을 함
    int totalscore = 0;

    //라운드 종료 판단 
    public static bool roundEnd = false;
    public static bool singleEnd = false;

    //라운드 인덱스
    int round = 0;
    int singleround = 0;

    //스코어
    int[] roundScore = new int[2];
    int roundendScore = 0;
    
    void Update()
    {
        //스코어 처리
        if (singleEnd)
        {
           

            //라운드 스코어 처리
            if (GameCount == 1)
            {     
                //현재 싱글 라운드 스코어 = 토탈 스코어 - 전판까지의 스코어
                roundScore[0] = Score - roundendScore;
                totalscore += roundScore[0];
                if (roundScore[0] >= 10) //스트라이크 판단
                {
                    singlegame[singleround].text = " ▶◀ ";
                    singleround++; //인덱스를 건너뜀
                }
                else
                {
                    singlegame[singleround].text = roundScore[0].ToString();
                }
            }
            else
            {
                roundScore[1] = Score - roundendScore;
                totalscore+= roundScore[1];

                if (roundScore[0] + roundScore[1] >= 10) //스페어 
                {
                    singlegame[singleround].text = " / ";
                }
                else
                {
                    singlegame[singleround].text = roundScore[1].ToString();
                }

            }

            //라운드 종료시 토탈 스코어 처리
            if (roundEnd)
            {  
                game[round].text = Score.ToString();
                
                round++;
                roundEnd = false;
            }
            scoreText.text = totalscore.ToString();
            //싱글 라운드 인덱스와 다음 싱글라운드 실행
            singleround++;
            singleEnd = false;
            //끝난 스코어 저장
            roundendScore = Score;
        }

        //볼륨 잼 
        
        if(Score > 15)
        {
            small.gameObject.SetActive(true);
        }
        if (Score > 40)
        {
            mid.gameObject.SetActive(true);
        }
        if (Score > 65)
        {
            large.gameObject.SetActive(true);
        }
        if (Score > 90)
        {
            large2.gameObject.SetActive(true);
        }

    }
}
