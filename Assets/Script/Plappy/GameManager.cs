using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬관리
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject _UI_Intro;
    public GameObject _UI_Play;
    public GameObject _UI_GameOver;
    public Rigidbody2D _birdRigid;
    public GameObject _bird;
    public PipeManager _pipeMgr;
    public Text _scoreNumberText;
    // 게임 인트로 상태
    public bool _isIntro = true;
    // 게임 오버상태
    public bool _isGameover = false;

    public int _score = 0;

    // Start is called before the first frame update
    void Start()
    {
        _UI_Intro?.SetActive(true);
        _UI_Play?.SetActive(false);
        _UI_GameOver?.SetActive(false);
        // 2. 중력 비활성화
        _birdRigid.simulated = false;

        //GameObject canvasUI = GameObject.Find("Canvas");
        //Transform playUITrans = canvasUI.transform.Find("1_UI_Play");
        //Transform scoreNumberTrans = playUITrans.Find("Txt_ScoreNum");

        //Transform scoreNumberTrans = _UI_Play.transform.Find("Txt_ScoreNum");
        //_scoreNumberText = scoreNumberTrans.gameObject.GetComponent<Text>();
        _scoreNumberText =
            FindObjectOfType<Canvas>().transform.Find("1_UI_Play").
            transform.Find("Txt_ScoreNum").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _scoreNumberText.text = _score.ToString();
    }

    // 버튼 이벤트 : 게임 시작
    public void OnClick_GameStart()
    {
        //Debug.Log("게임 시작 버튼 눌림!!");
        
        // 1. UI 처리
        _UI_Intro?.SetActive(false);
        _UI_Play?.SetActive(true);
        // 2. 중력 활성화
        _birdRigid.simulated = true;
        // 3. 유저입력 비활성화
        _isIntro = false;
        // 파이프 생성 시작
        _pipeMgr.Start_MakePipeSet();
    }

    public void OnClick_ReStart()
    {
        //// 1. UI 처리
        //_UI_Intro?.SetActive(false);
        //_UI_Play?.SetActive(true);
        //_UI_GameOver?.SetActive(false);
        //// 2. 중력 활성화
        //_birdRigid.simulated = true;
        SceneManager.LoadScene("MyFirstGame");
    }

    // 게임오버 이벤트 함수
    public void OnGameOver()
    {
        // 플레이 UI를 꺼주고
        _UI_Play?.SetActive(false);
        // 게임오버 UI를 켜주기
        _UI_GameOver?.SetActive(true);
        // 다시, 중력 비활성
        _birdRigid.simulated = false;
        _isGameover = true;
    }
}
