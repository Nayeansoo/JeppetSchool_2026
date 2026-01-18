using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("경험치")]
    private float playerExp; //플레이어 경험치
    private float maxExp = 20; //현재 레벨의 총 경험치
    private float expPer; //정규화된 경험치

    public Slider expSlider; //경험치 슬라이더
    public float expSpeed; //경험치 상승 속도
    public int level; //플레이어 레벨
    public Text levelText; //레벨 텍스트

    public enum ExpState
    {
        None, ExpUp
    }
    public ExpState expState = ExpState.None; //경험치 상태

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerExp();
    }

    void PlayerExp()
    {
        switch (expState)
        {
            case ExpState.ExpUp:
                {
                    //플레이어의 경험치를 목표량까지 채운다
                    expSlider.value = Mathf.MoveTowards(expSlider.value, expPer, expSpeed * Time.deltaTime);
                    if(expSlider.value == 1.0f) //경험치가 다 찾을 경우
                    {
                        level++; //레벨상승
                        levelText.text = level.ToString(); //레벨을 텍스트로 표현
                        playerExp = 0; //플레이어 경험치 0으로 초기화
                        maxExp = maxExp * 2.0f; //총 경험치를 두배로 늘림
                        expSlider.value = 0;//exp슬라이더 0으로 초기화
                        expState = ExpState.None;

                    }
                    else
                    {
                        if(expSlider.value == 0.0f)
                        {
                            expState = ExpState.None;
                        }
                    }
                        break;
                }
        }
    }

    public void PlayerExpUp(float exp)
    {
        playerExp += exp; //플레이어 경험치 증가
        expPer = playerExp / maxExp; //플레이어 경험치를 전체 경험치로 나눈다
        expState = ExpState.ExpUp; //경험치 증가 상태로 변경
    }
}
