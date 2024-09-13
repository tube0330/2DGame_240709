using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    //public GameObject AsteroidPre;    #1
    float TimePrev;
    float randomPosY;
    float randomScale;

    public Vector3 PosCam;
    float HitBeginTime;
    bool isHit = false;


    void Start()
    {
        TimePrev = Time.time;
        Instance = this;
    }

    void Update()
    {
        if (Time.time - TimePrev > 2f)
        {
            TimePrev = Time.time;
            //SpawnAsteroid();  #1
        }

        if (isHit)
        {
            float x = Random.Range(-0.05f, 0.05f);
            float y = Random.Range(-0.05f, 0.05f);

            Camera.main.transform.position += new Vector3(x, y, 0f);

            if (Time.time - HitBeginTime > 0.3f)
            {
                isHit = false;
                Camera.main.transform.position = PosCam;
            }
        }
    }

    void QuitApp()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    // #1. ObjectPooling으로 변경
    // void SpawnAsteroid()
    // {
    //     randomPosY = Random.Range(-4f, 4f);
    //     randomScale = Random.Range(1f, 3f);

    //     AsteroidPre.transform.localScale = Vector3.one * randomScale;
    //     Instantiate(AsteroidPre, new Vector3(10f, randomPosY, AsteroidPre.transform.position.z), Quaternion.identity);
    // }


    public void TurnOn()
    {
        isHit = true;
        PosCam = Camera.main.transform.position;
        HitBeginTime = Time.time;
    }
}
