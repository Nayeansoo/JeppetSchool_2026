using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;

    public float hitRange; //충돌반경

    public float bulletDamage; //총알대미지

    private void OnDrawGizmos()
    {
        //충돌반경만큼 와이어스피어를 그린다
        Gizmos.DrawWireSphere(transform.position, hitRange);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //총알을 전방으로 bulletSpeed의 속도로 이동한다
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);

        HitTarget();
    }

    //명중함수
    void HitTarget()
    {
        int layerMask = 1 << 6; //좀비 레이어 마스크
        //스피어 캐스트를 그린다 (위치, 반경, 방향, 거리, 레이어 마스크)
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, hitRange, Vector3.up, 0, layerMask);

        //감지되는 대상이 하나도 없다면
        if(hits.Length > 0)
        {
            foreach(RaycastHit hit in hits) //hit로 재정의
            {
                //좀비에게 대미지를 입힌다
                hit.transform.GetComponent<Zombie>().ZombieDamageOn(bulletDamage);
                Destroy(gameObject); //총알삭제
            }
        }
    }
}
