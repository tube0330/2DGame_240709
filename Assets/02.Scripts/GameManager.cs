using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject AsteroidPre;
    private float TimePrev;
    private float randomPosY;
    private float randomScale;

    public Vector3 PosCam;
    private float HitBeginTime;
    private bool isHit = false;


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
            SpawnAsteroid();
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
        // 모바일에서 뒤로가기를 누르면 게임이 종료되도록 한다
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    void SpawnAsteroid()
    {
        randomPosY = Random.Range(-4f, 4f);
        randomScale = Random.Range(1f, 3f);

        AsteroidPre.transform.localScale = Vector3.one * randomScale;
        Instantiate(AsteroidPre, new Vector3(10f, randomPosY, AsteroidPre.transform.position.z), Quaternion.identity);
    }


    public void TurnOn()
    {
        isHit = true;
        PosCam = Camera.main.transform.position;
        HitBeginTime = Time.time;
    }
}
