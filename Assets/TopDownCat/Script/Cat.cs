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
        public Resource _resource;
        public Transform _worldTrans;
        AudioSource _meowAudio;


        bool _isIdlePlay = false;   // 대기 애니매이션 랜덤재생중인가
        float delay;
        //이모티콘 모음
        GameObject _emoteSweat;
        GameObject _emoteHappy;

        // 랜덤이동 관련
        public GameObject _randomMoveObj;


        // Start is called before the first frame update
        void Start()
        {
            _rigid = GetComponent<Rigidbody2D>();
            _renderer = GetComponent<SpriteRenderer>();
            _anima = GetComponent<Animator>();
            _meowAudio = GetComponent<AudioSource>();

            _emoteSweat = transform.Find("Emote_Sweat").gameObject;
            _emoteSweat.SetActive(false);
            _emoteHappy = transform.Find("Emote_Happy").gameObject;
            _emoteHappy.SetActive(false);

            _randomMoveObj.SetActive(false);

            // idle2, idle3 애니매이션 랜덤 재생 시작
            //float delay = Random.Range(5.0f, 10.0f);
            //Invoke("PlayRandomIdle", delay);

            Invoke("MakeRandomMoveObj", 5.0f);
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
            {
                _anima.SetTrigger("idle3");
                _meowAudio.Play();
            }
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
            CheckDistance();
        }
        private void FixedUpdate()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Move(h, v);
            MoveAnima(h, v);
            MoveTarget(_target);
            //MoveRandomPos();
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

            if (collision.gameObject.name.Contains("RandomMoveTarget"))
            {
                SetTarget(null);
                GameObject.Destroy(collision.gameObject);
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.name.Contains("Coin"))
            {
                _resource.CalculateCoin(1);
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.name.Contains("Heart"))
            {
                _resource.CalculateHeart(1);
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

        public void CheckDistance()
        {
            Transform jarTrans = _worldTrans.transform.Find("Item").Find("Jar");
            // 테스트로 책상과 고양이 사이의 거리를 체크
            // 거리가 충분히 가까우면 로그 출력
            Vector2 catPos = transform.position;
            Vector2 objPos = jarTrans.position;

            float distance = Vector2.Distance(catPos, objPos);
            if(distance <= 1.0f)
            {
                Debug.Log("항아리 근접");
            }
            //float xDist = Mathf.Abs(catPos.x - ObjPos.x);
            //float yDist = Mathf.Abs(catPos.y - ObjPos.y);

            //float distance = Mathf.Sqrt(xDist * xDist + yDist * yDist);
        }

        public void MakeRandomMoveObj()
        {
            // 랜덤으로 0, 1 뽑기
            int random = Random.Range(0, 2);

            // 움직임 범위
            float maxRadius = 2f;
            float minRadius = 1.5f;
            // 0이면 제자리, 1이면 움직임
            if (random == 1)
            {
                GameObject MoveTarget = Instantiate(_randomMoveObj);
                Vector2 circleRange = UnityEngine.Random.insideUnitCircle * maxRadius;
                Vector2 randomPos;
                if (circleRange.magnitude < minRadius)
                    randomPos = circleRange.normalized * minRadius;
                else
                    randomPos = circleRange;

                MoveTarget.transform.position = transform.position
                                                + new Vector3(randomPos.x, randomPos.y);
                MoveTarget.SetActive(true);

                SetTarget(MoveTarget);
            }
            Invoke("MakeRandomMoveObj", 5.0f);
        }

        public void MoveRandomPos()
        {

            //Vector3 movePos = transform.position + randomPos;
            //Vector3.MoveTowards(transform.position, movePos, 0.5f);
            //Vector3 direction = (movePos - transform.position).normalized;
            //MoveAnima(direction.x, direction.y);
        }
    }
}
