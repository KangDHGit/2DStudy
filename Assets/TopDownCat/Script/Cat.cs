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
            float delay = Random.Range(3.0f, 5.0f);
            Invoke("PlayIdle", delay);
        }

        void PlayIdle() // 대기 애니메이션 랜덤 재생
        {
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

            float delay = Random.Range(3.0f, 10.0f);
            Invoke("PlayIdle", delay);
        }
        
        // Update is called once per frame
        void Update()
        {
        }
        private void FixedUpdate()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Move(h, v);
            MoveAnim(h, v);
            //Flip(h);
        }
        void Move(float h, float v)
        {
            //Debug.Log("h: " + h);
            //Debug.Log(string.Format("v: {0}", v));

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

            //_anima.SetFloat("velocity", _rigid.velocity.magnitude);
            
        }
        void Flip(float h)
        {
            if (h < 0)
            {
                _renderer.flipX = false;
            }
            else if (h > 0)
                _renderer.flipX = true;
        }
        void MoveAnima(float h, float v)
        {
            if(h == 0 && v == 0)
                _anima.SetBool("ismovingLR", false);
            else
                _anima.SetBool("ismovingLR", true);
        }
        void MoveAnim(float h, float v)
        {
            if (h != 0 && v == 0)   // 좌우키
            {
                _anima.SetBool("ismovingLR", true);
                _anima.SetBool("ismovingUp", false);
                _anima.SetBool("ismovingDown", false);
                Flip(h);
            }
            else if (h == 0 && v > 0)   // 상키
            {
                _anima.SetBool("ismovingUp", true);
                _anima.SetBool("ismovingLR", false);
                _anima.SetBool("ismovingDown", false);
            }
            else if (h == 0 && v < 0)   // 하키
            {
                _anima.SetBool("ismovingDown", true);
                _anima.SetBool("ismovingLR", false);
                _anima.SetBool("ismovingUp", false);
            }

            else if (h != 0 && v > 0)    // 좌우 + 상키
            {
                _anima.SetBool("ismovingUp", true);
                _anima.SetBool("ismovingLR", true);
                _anima.SetBool("ismovingDown", false);
                Flip(h);
            }
            else if (h != 0 && v < 0)   // 좌우 + 하키
            {
                _anima.SetBool("ismovingDown", true);
                _anima.SetBool("ismovingLR", true);
                _anima.SetBool("ismovingUp", false);
                Flip(h);
            }
            else
            {
                _anima.SetBool("ismovingLR", false);
                _anima.SetBool("ismovingUp", false);
                _anima.SetBool("ismovingDown", false);
            }
        }
        void SetAnimSpeed()
        {
            int tagHash =_anima.GetCurrentAnimatorStateInfo(0).tagHash;
            
        }
        void EatingAnimEnable()
        {
            _anima.SetBool("isEating", false);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.name == "Cat_Dish")
            {
                Debug.Log("밥 충돌");
                _anima.SetBool("isEating", true);
                Invoke("EatingAnimEnable", 5.0f);
                EatEmoticon();
            }
        }

        void EatEmoticon()
        {
            _emoteSweat.SetActive(true);
            Invoke("StopEat", 5.0f);
            Debug.Log("Eat");
        }
        void StopEat()
        {
            _emoteSweat.SetActive(false);
            //PlayEmoticon("Happy", 3.0f);
            _emoteHappy.SetActive(true);
            Invoke("StopHappy", 3.0f);
            Debug.Log("StopEat");
        }
        void StopHappy()
        {
            _emoteHappy.SetActive(false);
            Debug.Log("StopEat and Happy");
        }
    }
}
