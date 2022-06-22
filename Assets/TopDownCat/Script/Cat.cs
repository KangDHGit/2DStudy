using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCat
{
    public class Cat : MonoBehaviour
    {
        public float _speed = 100.0f;
        Rigidbody2D _rigid;
        SpriteRenderer _renderer;
        Animator _anima;
        GameObject _target;
        public GameManager _gameMgr;
        

        bool _isIdlePlay = false;   // 대기 애니매이션 랜덤재생중인가
        float delay;
        //이모티콘 모음
        GameObject _emoteSweat;
        GameObject _emoteHappy;

        // Start is called before the first frame update
        void Start()
        {
            _rigid = GetComponent<Rigidbody2D>();
            _renderer = GetComponent<SpriteRenderer>();
            _anima = GetComponent<Animator>();

            _emoteSweat = transform.Find("Emote_Sweat").gameObject;
            _emoteSweat.SetActive(false);
            _emoteHappy = transform.Find("Emote_Happy").gameObject;
            _emoteHappy.SetActive(false);

            // idle2, idle3 애니매이션 랜덤 재생 시작
            //float delay = Random.Range(5.0f, 10.0f);
            //Invoke("PlayRandomIdle", delay);
        }

        void PlayRandomIdle() // 대기 애니메이션 랜덤 재생
        {
            // 움직이거나 먹는중이면 정지후 Bool 값 수정
            if (_anima.GetBool("isMoving") || _anima.GetBool("isEating"))
            {
                _isIdlePlay = false;
                return;
            }

            int random = Random.Range(2, 4); // 2~3 랜덤
            if (random == 2)
                _anima.SetTrigger("idle2");
            if (random == 3)
                _anima.SetTrigger("idle3");
            //// 삼항연산자 활용
            //string temp = (random == 2) ? "idle2" : "idle3";
            //_anima.SetTrigger("temp");

            // 단항연산자 unary operator
            // 이항연산자 binary operator
            // 삼상연산자

            float delay = Random.Range(5.0f, 10.0f);
            Invoke("PlayRandomIdle", delay);
            _isIdlePlay = true;
        }
        
        void PlayIdleCheck() // 업데이트 함수에서 사용
        {
            // 대기 애니메이션 랜덤 재생중일시 리턴
            if (_isIdlePlay == true)
                return;
            else if(_isIdlePlay == false)// 아니면 재생 후 Bool 값 수정
            {
                float delay = Random.Range(5.0f, 10.0f);
                Invoke("PlayRandomIdle", delay);
                _isIdlePlay = true;
            }
        }

        void Update()
        {
            PlayIdleCheck();
        }
        private void FixedUpdate()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Move(h, v);
            MoveAnima(h, v);
            MoveTarget(_target);
        }
        void Move(float h, float v)
        {
            //Debug.Log("h: " + h);
            //Debug.Log(string.Format("v: {0}", v));

            if (_anima.GetBool("isEating"))
                return;

            float runSpeed = 1.0f;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                runSpeed = 2.0f;
                _anima.speed = 2;
            }
            else
                _anima.speed = 1;

            float fixedDeltaTime = Time.fixedDeltaTime;

            _rigid.velocity = new Vector2(h, v).normalized * _speed * fixedDeltaTime * runSpeed;
        }
        void Flip(float h)
        {
            if(_anima.GetBool("isEating"))
                return;
            if (h < 0)
            {
                _renderer.flipX = false;
            }
            else if (h > 0)
                _renderer.flipX = true;
        }
        void MoveAnima(float h, float v)
        {
            _anima.SetFloat("Horizontal", h);
            _anima.SetFloat("Vertical", v);
            if(h == 0 && v == 0)
                _anima.SetBool("isMoving", false);
            else
                _anima.SetBool("isMoving", true);
            Flip(h);
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // 밥그릇 충돌시 밥는 애니메이션 및 이모티콘
            if (collision.gameObject.name == "Cat_Dish")
            {
                Debug.Log("밥 충돌");
                _anima.SetBool("isEating", true);
                EatEmoticon();
                // 충돌시 타겟을 null로
                SetTarget(null);
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.name.Contains("Coin"))
            {
                _gameMgr.AddCoin(1);
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.name.Contains("Heart"))
            {
                _gameMgr.AddHeart(1);
                Destroy(collision.gameObject);
                
            }
        }
        // 밥 주기 관련--------------------------------
        void EatEmoticon() 
        {
            _emoteSweat.SetActive(true);
            Invoke("StopEat", 5.0f);
            Debug.Log("Eat");
        }
        void StopEat()
        {
            _anima.SetBool("isEating", false);
            _emoteSweat.SetActive(false);
            _emoteHappy.SetActive(true);
            _gameMgr.OnFinish_Eat();
            _gameMgr.DropItem(_gameMgr._heartTemplate);

            Invoke("StopHappy", 3.0f);
            Debug.Log("StopEat");
        }
        void StopHappy()
        {
            _emoteHappy.SetActive(false);
            Debug.Log("StopEat and Happy");
        }
        // 밥 주기 관련--------------------------------

        // 타겟 이동 관련------------------------------
        public void SetTarget(GameObject target)
        {
            _target = target;
        }
        public void MoveTarget(GameObject target)
        {
            if (target == null)
                return;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.1f);
            Vector2 direction = (target.transform.position - transform.position).normalized;
            MoveAnima(direction.x, direction.y);
        }
        // 타겟 이동 관련------------------------------
    }
}
