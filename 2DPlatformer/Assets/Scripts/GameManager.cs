using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool isGameOver = false;

    public float timer; // 시간 누적용
    public float gameCountDown;
    public float givenTime = 180f; // 주어진 시간
    
    // 싱글톤 아주 기초적인거만
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {   

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        gameCountDown = givenTime - timer;

        if (Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale = 0;
        }
        
        if (isGameOver)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Debug.Log("게임 오버");

        Time.timeScale = 0;
        // 물리 연산들 정지
        // update, fixedUpdate 호출 자체는 되지만, 연산을 수행하지 않음

        // 영향 받지 않는 것들
        // Time.unscaledTime; // 시간 정지 무시하고 수행 할 작업들
        // Time.unscaledDeltaTime; // TimeScale의 영향 받지 않는 델타 타임 측정, 진짜 현실 시간 기준 델타 타임
        
        // UI 시스템도 영향 안받음. 렌더링도 안받음
        // 애니메이션 영향 안받음
        // 오디오소스 영향 안받음
    }
}
