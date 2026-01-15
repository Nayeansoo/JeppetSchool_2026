using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController cc; //캐릭터 컨트롤러
    public float speed;
    private Vector3 lookTarget; //캐릭터가 바라볼 방향 시선
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
}
