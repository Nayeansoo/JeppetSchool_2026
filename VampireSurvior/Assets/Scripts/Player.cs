using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("플레이어")]
    private CharacterController cc; //캐릭터 컨트롤러
    public float speed;
    private Vector3 lookTarget; //캐릭터가 바라볼 방향 시선

    [Header("공격")]
    public Transform firePos; //총알 발사 위치
    public GameObject bulletPrefab; //총알 프리팹
    public float bulletOffset; //총알간격

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

        Vector3 pos = new Vector3(-v, 0, h);
        cc.SimpleMove(pos * speed); //심플 무브로 이동한다 (높낮이 인식)
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

        }
    }

    //총알 생성함수
    void CreateBullet()
    {
        //bulletoffset 반경만큼의 구체 범위와 격발 위치를 더한 범위에서 무작위로 Vector3값을 추출한다
        Vector3 offset = Random.insideUnitSphere * bulletOffset + firePos.position;
        //총알을 생성한다 //생성자(원본,초기위치,초기회전값)
        GameObject bullet = Instantiate(bulletPrefab, offset, firePos.rotation);

        Destroy(bullet, 3.0f); //3총 뒤에 총알 삭제
    }
}
