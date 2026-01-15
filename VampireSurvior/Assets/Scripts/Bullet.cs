using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //총알을 전방으로 bulletSpeed의 속도로 이동한다
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }
}
