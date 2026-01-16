using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("플레이어")]
    private CharacterController cc; //캐릭터 컨트롤러
    public Animator anim; //애니메이터
    public float speed;
    private Vector3 lookTarget; //캐릭터가 바라볼 방향 시선

    public float jumpPower = 10; //점프 파워
    public float gravity = -20; //중력 가속
    public float yVelocity = 0;//높이가속
    private bool isGrounded = true; //지면 신호

    [Header("공격")]
    public Transform firePos; //총알 발사 위치
    public GameObject bulletPrefab; //총알 프리팹
    public float bulletOffset; //총알간격
    private float bulletTime; //격발시간 
    public float setBulletTime; //설정할 격발 시간
    public Camera_Move cam; //카메라

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cc = GetComponent<CharacterController>(); //캐릭터 컨트롤러 초기화
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerRotate();
        PlayerFire();
    }

    //플레이어 이동 함수
    void PlayerMove()
    {
        float h = Input.GetAxis("Horizontal"); //가로입력
        float v = Input.GetAxis("Vertical"); //세로입력

        Vector3 pos = new Vector3(-v, 0, h) * speed;

        //캐릭터 컨트롤러가 바닥에 닿고 있다면
        if (cc.isGrounded)
        {
            yVelocity = 0; //강화속도를 0으로 초기화
            isGrounded = true; //지면 신호를 true로 변경
        }

        //점프버튼을 누른다면
        if (Input.GetButtonDown("Jump"))
        {
            //지면 신호가 true라면
            if (isGrounded)
            {
                yVelocity = jumpPower; //점프
                isGrounded= false; //점프 중복을 방지
            }
        }

        yVelocity += gravity * Time.deltaTime; //중력 적용
        pos.y = yVelocity;
        cc.Move(pos *  Time.deltaTime); //캐릭터 이동

        //cc.SimpleMove(pos * speed); //심플 무브로 이동한다 (높낮이 인식)

        //가로 입력과 세로 입력의 합의 값에 따라 달리고 멈춘다
        anim.SetFloat("Move", Mathf.Abs(h) + Mathf.Abs(v));
    }

    //플레이어 회전 함수
    void PlayerRotate()
    {
        RaycastHit hit; //충돌 정보
        //메인카메라에서 마우스 커서의 위치에 레이를 발사하여 충돌이 일어난다면 
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit))
        {
            lookTarget = hit.point; //충동한 레이의 위치를 lookTarget에 담는다 

            //캐릭터가 looktarget을 바라보게 한다
            Vector3 relation = new Vector3(lookTarget.x, 0, lookTarget.z) - new Vector3(transform.position.x, 0, transform.position.z);

            Quaternion rotation = Quaternion.LookRotation(relation);
            transform.rotation = rotation;
        }
    }

    //플레이어 격발 함수
    void PlayerFire()
    {
        //왼쪽 마우스 버튼을 누른다면
        if (Input.GetButtonDown("Fire1"))
        {
            //i가 0에서 시작해서 3보다 같거나 클때까지 반복하는 함수 -> 0, 1, 2
            for( int i = 0; i < 3; i++)
            {
                CreateBullet(); //총알 생성 함수 호출
            }
            bulletTime = 0; //총알 격발 시간 초기화

            anim.SetTrigger("Fire"); //격발 애니메이션 실행
        }

        //왼쪽 마우스 버튼을 누르고 있다면
        if (Input.GetButton("Fire1"))
        {
            bulletTime += Time.deltaTime; //격발 시간 재생
            //격발 시간이 설정한 격발시간보다 크거나 같을 경우
            if(bulletTime >= setBulletTime)
            {
                //i가 0에서 시작해서 3보다 같거나 클때까지 반복하는 함수 -> 0, 1, 2
                for (int i = 0; i < 3; i++)
                {
                    CreateBullet(); //총알 생성 함수 호출
                }
                bulletTime = 0;
            }

            cam.CameraShakeOn(); //카메라 진동
        }

        //왼쪽마우스 버튼을 떈다면
        if (Input.GetButtonUp("Fire1"))
        {
            bulletTime = 0; //총알 격발 시간 초기화
            cam.CameraShakeOff(); //카메라 진동 종료
            anim.SetTrigger("Fire"); //격발 애니메이션 실행
        }
    }

    //총알 생성함수
    void CreateBullet()
    {
        //bulletoffset 반경만큼의 구체 범위와 격발 위치를 더한 범위에서 무작위로 Vector3값을 추출한다
        Vector3 offset = Random.insideUnitSphere * bulletOffset + firePos.position;
        //총알을 생성한다 //생성자(원본,초기위치,초기회전값)
        GameObject bullet = Instantiate(bulletPrefab, offset, firePos.rotation);

        Destroy(bullet, 1.0f); //1총 뒤에 총알 삭제
    }

   
}
