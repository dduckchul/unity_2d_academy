using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UiGlitter : MonoBehaviour
{
    // Ui Glitter 스크립트는 어두워졌다가 밝았다를 반복할 대상에게 줄것이다.
    // 매 업데이트마다 숫자가 0에서 1까지 0.01 증가하게 만든다.
    // 만약 1을 초과하면 역으로 0 까지 감소하게 만든다

    public Color tempColor; // RGBA가 모두 내장된 자료형
    private Text textComponent; // 텍스트 컴포넌트 담을 껍데기
    private bool increase = false;
    private float glitterSpeed = 0.005f;
    
    // 선언부
    void Start()
    {
        tempColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        textComponent = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // 업데이트 부분
        var rand = Random.Range(0.01f, 0.99f);
        
        if (textComponent.color.a <= 0)
        {
            increase = true;
            Debug.Log("지금 0");
        }
        
        if (textComponent.color.a >= 1)
        {
            increase = false;
            Debug.Log("지금 1");
        }

        if (increase)
        {
            tempColor.a += glitterSpeed;
        }
        else
        {
            tempColor.a -= glitterSpeed;
        }
        
        tempColor.r = Random.Range(0.01f, 0.99f);
        tempColor.g = Random.Range(0.01f, 0.99f);
        tempColor.b = Random.Range(0.01f, 0.99f);        
        
        textComponent.color = tempColor;
        
        // 반영 부분
    }
}
