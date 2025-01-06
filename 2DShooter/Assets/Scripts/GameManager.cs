using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance // 프로퍼티로 만들었다, 싱글톤 패턴
    {
        get; private set;
    }

    // 최초 실행
    private void Awake()
    {
        if (Instance == null) // 맨 처음 호출이라 인스턴스가 비어있을 땐?
        {
            Instance = this; // 이 스크립트 컴포넌트를 instance 껍데기에 담아라
            DontDestroyOnLoad(gameObject); // 그리고 이 오브젝트 파괴하지 말아주세용
        } 
        else if (Instance != this) // 안전망
        {
            Destroy(gameObject); // 그렇지 않다면 파괴
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
