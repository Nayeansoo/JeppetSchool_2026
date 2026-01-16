using UnityEngine;

public class Camera_Move : MonoBehaviour
{
    public Transform target; //타겟

    public float shakeAmount; //진동 강도
    public Transform originPos; //카메라의 기존 위치

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position; //카메라의 위치를 타겟의 위치로 한다
        
    }

    //카메라 흔들기 시작함수
    public void CameraShakeOn()
    {
        //진동강도 만큼의구체 범위와 카메라의 기존 위치를 합한 구역에서 무작위로 Vector3값을 추출한다
        Vector3 vibration = Random.insideUnitSphere * shakeAmount + originPos.position;

        //카메라를 흔든다
        Camera.main.transform.position = vibration;
    }

    //카메라 흔들기 종류 함수
    public void CameraShakeOff()
    {
        //카메라의 위치를 기존 위치로 되돌린다
        Camera.main.transform.position = originPos.position;
    }
}
