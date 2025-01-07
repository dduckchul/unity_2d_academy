using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private AudioSource soundManagerAudioSource;
    public AudioClip explodeSFX;
    public ParticleSystem _explodeParticlePrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        // 어디에 코딩하냐에 따라 스타일
        // soundManagerAudioSource = GetComponent<AudioSource>(); // 여기선 불렛에 오디오 소스가 없으니까 안됨
        soundManagerAudioSource = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        // GameManager.Instance.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    // 두가지 문제, 하이어라키 창에서 안사라짐
    // 0,0 에서 나감    
    void Update()
    {
        transform.Translate(0, 0.1f, 0);

        // 원초적인 코딩 y가 얼마 이상이면 지워라
        // if (transform.position.y > 10)
        // {
        //     Destroy(gameObject);
        // }
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            soundManagerAudioSource.PlayOneShot(explodeSFX);
            Destroy(gameObject);
            Destroy(other.gameObject);
            Instantiate(_explodeParticlePrefab, transform.position, transform.rotation);
            // GameObject.Find("GameManager").GetComponent<GameManager>().AddScore();
            GameManager.Instance.AddScore();
        }

        // 동일한 코드
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
