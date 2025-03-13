using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

class Counter
{
    private int _counter = 0;

    public int Increase()
    {
        return _counter++;
    }
}

public class ObjPool : MonoBehaviour
{
    private ObjectPool<GameObject> myObjPool;
    Func<int> getValue; // 반환값이 int 형태 메서드를 담을 수 있는 델리게이트

    public delegate void MyDel(); // 델리게이트 설계도 만든다
    public MyDel OnRunning; // 이걸로 델리게이트 변수 만든다

    public GameObject toInstantiate; // 인스턴스화 시키고자 하는 프리팹
    
    // 만들자마자 하나 만들어줄수도 있음
    private Func<int> MakeClosure()
    {
        int counter = 0;
        return () => counter++;
    }

    private List<Action> actions = new List<Action>();
    
    // 함수를 간단하게 표현할 때 람다식 사용
    // 클로저와 캡쳐
    // 델리게이트 클로져와 캡쳐
    
    // 캡쳐 : 람다식에서 외부 범위에 선언된 변수 (필드등을 사용하는 것을 의미)
    // 클로져 : 캡쳐된 변수와 해당 캡쳐 변수를 사용하는 식을 묶어서 저장하는 것
    // 클로저 덕분에 함수가 외부 범위 변수를 "생명주기 무시" 하고 사용 가능하다
    
    // 명명된 인수 (이름이 지어진)
    public void PokemonInfoScreen(int att, int def, int hp, int lvl = 1, int type = 3)
    {
        // 이런 저런것들 예쁘게 띄워줄것
    }

    int GetNumber()
    {
        return 42;
    }

    void Print(int x)
    {
        Debug.Log(x);
    }

    int MakeCounter()
    {
        int counter = 0;
        return counter++;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // 어느 순서에 무엇을 호출할지 헷갈린다.
        // 또 하나 문제, 팀원들이 읽을때 결국 하나 하나 클릭해 봐야 안다.
        PokemonInfoScreen(1,2,3,4);
        
        // 이럴 때 좋은 것이 명명된 인수
        PokemonInfoScreen(def:12, att:23, hp:2323, lvl:12);
        
        // 명확성 : 매개변수가 많을 때, 혹은 순서가 헷갈릴 때
        // 선언된 순서를 무시함
        
        // 선택적 인수, 선언 안하고 기본 값으로 넣고 넘어갈 수 있음.
        // 선택적 매개변수와 함께 사용 가능하다.
        PokemonInfoScreen(1, 200, lvl:300, hp:5);
        
        // 람다식
        // 함수를 간단하게 표현할 때 람다식 사용
        // (입력값) => {함수의 동작}
        
        // myObjPool = new ObjectPool<GameObject>();
        int value = 42; // start 내부에서 value 만들었다. 42
        getValue = () => value++; // value -> 람다식 내부에는 없지만 외부에 선언된 value를 사용하는것, value를 캡쳐한다.
        
        // 람다식에서 캡쳐를 하면, 생명 주기가 변하게 됨. 생명 주기와 상관 없이 계속 사용 가능해진다.
        // 클로져란? 캡쳐된 변수와 해당 변수를 사용하는 함수의 묶음
        // 클로져 덕분에 함수가 외부 범위의 변수를 생명 주기와 상관없이 사용 가능케 한다.

        // 0 0 두번 프린트 왜냐? 초기화 내부변수로 시켜줬기 때문에
        Debug.Log(MakeCounter());
        Debug.Log(MakeCounter());

        // 1씩 오르긴 할건데, 클래스 하나 더 만들고 변수선언하고.. 귀찮은 작업들 많아짐.
        Counter myC = new Counter();
        Debug.Log(myC.Increase());

        // 중괄호가 끝나고 나서도 살아있다!?
        var closure = MakeClosure();
        Debug.Log(closure());
        Debug.Log(closure());
        Debug.Log(closure());

        int a = 0;
        OnRunning = () =>
        {
            a++;
            Debug.Log("풀어서?! : " + a);
        };
        Debug.Log("풀어서?!2 : " + a);

        // 알랑말랑 하다...
        // i는 i++ 해서 끝에는 3을 담아뒀고, add 할때 캡쳐를 Debug.Log(i) 자체를 진짜 담아뒀다고 밖에 할수 없군.
        for (int i = 0; i < 3; i++)
        {
            actions.Add(() => Debug.Log("action :" + i));
        }

        foreach (var action in actions)
        {
            action();
        }

        // 람다식으로 리팩토링
        // myObjPool = new ObjectPool<GameObject>(CreateNewObject, ActivateObject, DeActivateObject, DestroyObject);
        myObjPool = new ObjectPool<GameObject>
        (
            createFunc: () => Instantiate(toInstantiate),
            actionOnGet: obj => { obj.SetActive(true); },
            actionOnRelease: obj => obj.SetActive(false),
            actionOnDestroy: obj => Destroy(obj)
        );
    }

    GameObject CreateNewObject()
    {
        return Instantiate(toInstantiate);
    }

    void ActivateObject(GameObject obj)
    {
        obj.SetActive(true);
    }

    void DeActivateObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    void DestroyObject(GameObject obj)
    {
        Destroy(obj);
    }
    
    // Update is called once per frame
    void Update()
    {
        // myObjPool.Get();
        // 계속 1씩 증가한다~ 외부에 있음에도 불구하고.. ㄷㄷ
        // OnRunning.Invoke();
        // Debug.Log(getValue());
    }
}
