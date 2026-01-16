using UnityEngine;
using UnityEngine.AI; //네비게이션 AI 사용

public class Zombie : MonoBehaviour
{
    public enum LiveState
    {
        Live, Dead
    }
    public LiveState liveState = LiveState.Live; //생존 상태

    public enum ActionState
    {
        Idle, Move, Attack
    }
    public ActionState actionState = ActionState.Idle; //행동상태
    private GameObject Player; //플레이어
    private NavMeshAgent agent; //내브매쉬 에이전트
    public Animator anim; //애니메이터
    public float attackRange; //공격 반경

    public enum AttackState
    {
        None, Attack, Delay
    }
    public AttackState attackState = AttackState.None; //공격상태
    private float attackTime; //공격시간
    private float delayTime; //지연시간
    public AnimationClip attackClip; //공격 애니메이션 클립

    //기즈모를 그리는 함수
    private void OnDrawGizmos()
    {
        //공격반경만큼 와이어 스피어를 그린다
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player"); //플레이어 초기화
        agent = GetComponent<NavMeshAgent>(); //내브매쉬 에이전트 초기화
    }

    // Update is called once per frame
    void Update()
    {
        Action(); //행동함수 호출
    }

    //행동함수
    void Action()
    {
        switch (actionState) //액션 상태
        {
            case ActionState.Idle: //대기상태
                {
                    if (Player)
                    {
                        agent.isStopped = false; //이동중지 해제
                        AnimOn(1); //이동 애니메이션 실행
                        actionState = ActionState.Move; //이동상태로 변경
                    }
                    break;
                }
            case ActionState.Move: //이동상태
                {
                    if (Player)
                    {
                        agent.SetDestination(Player.transform.position); //플레이어를 향해 이동한다

                        //플레이어와 좀비 사이에 거리를 Float로 반환한다
                        float dist = Vector3.Distance(transform.position, Player.transform.position);

                        //공격 반경 안에 플레이어가 들어왔을때
                        if(dist <= attackRange)
                        {
                            agent.isStopped = true; //이동중지
                            AnimOn(2); //공격 애니메이션 실행
                            attackState = AttackState.Attack; //공격중 상태로 변환
                            actionState = ActionState.Attack; //공격 상태로 변환
                        }
                    }
                    break;
                }
                case ActionState.Attack: //공격상태
                {
                    switch (attackState) //공격중 상태
                    {
                        case AttackState.Attack: //공격중
                            {
                                attackTime += Time.deltaTime; //공격시간재생
                                //공격 시간이 공격 클립의 25%를 넘어설 경우
                                if(attackTime >= attackClip.length * 0.25f)
                                {
                                    AnimOn(0); //대기동작
                                    attackTime = 0;
                                    delayTime = 0;
                                    attackState = AttackState.Delay;
                                }
                                break;
                            }
                        case AttackState.Delay: //지연중
                            {
                                attackTime += Time.deltaTime; //공격시간재생
                                //지연 시간이 공격클립의 75%를 넘어설 경우
                                if (attackTime >= attackClip.length * 0.75f * 2.0f)
                                {
                                    AnimOn(2); //대기동작
                                    attackTime = 0;
                                    delayTime = 0;
                                    attackState = AttackState.Attack;
                                }
                                break;
                            }
                    }
                    break;
                }
        }
    }

    void AnimOn(int n)
    {
        anim.SetInteger("ZombieState", n); //n번 애니메이션 실행
    }
}
