using UnityEngine;

public class Camera_Move : MonoBehaviour
{
    public Transform target; //타겟

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position; //카메라의 위치를 타겟의 위치로 한다
        
    }
}
