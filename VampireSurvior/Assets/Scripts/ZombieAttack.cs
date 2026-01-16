using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    public enum AttackActivate
    {
        None, Activate
    }
    public AttackActivate attackActivate = AttackActivate.None; //공격 감지기 활성화 상태

    public float attackRange; //공격반경
    public float zombieDamage; //좀비공격

    private void OnDrawGizmos()
    {
        //공격반경만큼의 와이어 스피어를 그린다
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (attackActivate)
        {
            case AttackActivate.Activate:
                {
                    int layerMask = 1 << 7; //플래이어 레이어 마스크
                    //스피어 캐스트를 사용한다 (위치,반경,방향,거리,레이어마스크)
                    RaycastHit[] hits = Physics.SphereCastAll(transform.position, attackRange, Vector3.up, 0, layerMask);
                    
                    //감지되는 대상이 없다면
                    if(hits.Length > 0)
                    {
                        //hit로 재정의
                        foreach(RaycastHit hit in hits)
                        {
                            //플레이어 HP감소
                            hit.transform.GetComponent<Player>().PlayerDamageOn(zombieDamage);
                            attackActivate = AttackActivate.None; //공격 감지기 비활성화
                        }
                    }

                    break;
                }
        }
    }

    //좀비 공격 감지기 활성화 함수
    public void ZombieAttackOn()
    {
        attackActivate = AttackActivate.Activate; //공격 감지기 활성화
    }

}
