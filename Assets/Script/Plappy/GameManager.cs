using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ������
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
    // ���� ��Ʈ�� ����
    public bool _isIntro = true;
    // ���� ��������
    public bool _isGameover = false;

    public int _score = 0;

    // Start is called before the first frame update
    void Start()
    {
        _UI_Intro?.SetActive(true);
        _UI_Play?.SetActive(false);
        _UI_GameOver?.SetActive(false);
        // 2. �߷� ��Ȱ��ȭ
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

    // ��ư �̺�Ʈ : ���� ����
    public void OnClick_GameStart()
    {
        //Debug.Log("���� ���� ��ư ����!!");
        
        // 1. UI ó��
        _UI_Intro?.SetActive(false);
        _UI_Play?.SetActive(true);
        // 2. �߷� Ȱ��ȭ
        _birdRigid.simulated = true;
        // 3. �����Է� ��Ȱ��ȭ
        _isIntro = false;
        // ������ ���� ����
        _pipeMgr.Start_MakePipeSet();
    }

    public void OnClick_ReStart()
    {
        //// 1. UI ó��
        //_UI_Intro?.SetActive(false);
        //_UI_Play?.SetActive(true);
        //_UI_GameOver?.SetActive(false);
        //// 2. �߷� Ȱ��ȭ
        //_birdRigid.simulated = true;
        SceneManager.LoadScene("MyFirstGame");
    }

    // ���ӿ��� �̺�Ʈ �Լ�
    public void OnGameOver()
    {
        // �÷��� UI�� ���ְ�
        _UI_Play?.SetActive(false);
        // ���ӿ��� UI�� ���ֱ�
        _UI_GameOver?.SetActive(true);
        // �ٽ�, �߷� ��Ȱ��
        _birdRigid.simulated = false;
        _isGameover = true;
    }
}
