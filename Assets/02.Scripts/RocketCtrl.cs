using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RocketCtrl : MonoBehaviour
{
    [SerializeField] private Touch_pad pad_c;
    private AudioSource source;
    public AudioClip clip;
    public GameObject Coin;
    public GameObject Effect;
    private Transform tr;
    public Transform FirePos;

    private float h = 0f, v = 0f;
    private string enemyTag = "ENEMY";

    void Start()
    {
        pad_c = GameObject.Find("Image_JoysticPad").GetComponent<Touch_pad>();
        tr = transform;
        source = GetComponent<AudioSource>();
    }
    public void OnStickPos(Vector3 joystickPos)
    {
        h = joystickPos.x;
        v = joystickPos.y;
    }
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            JoyStickControl();
        }

        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            JoyStickControl();
        }

        CameraOutLimit();
    }

    private void JoyStickControl()
    {
        if (GetComponent<Rigidbody2D>())
        {
            Vector2 speed = GetComponent<Rigidbody2D>().velocity; //���� ����
            speed.x = 4 * h;
            speed.y = 4 * v;
            GetComponent<Rigidbody2D>().velocity = speed;
        }
    }

    private void CameraOutLimit()
    {
        tr.position = new Vector2(Math.Clamp(tr.position.x, -8f, 8f), Math.Clamp(tr.position.y, -4.5f, 4.5f));
    }

    public void Fire()
    {
        Instantiate(Coin, FirePos.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(enemyTag))
        {
            other.gameObject.SetActive(false);

            // GameObject Damage_Eff = Instantiate(Effect, new Vector3(tr.position.x, tr.position.y, -3f), Quaternion.identity);
            // Destroy(Damage_Eff, 0.5f);

            StartCoroutine(ShowEffect());

            source.PlayOneShot(clip, 1.0f);
            GameManager.Instance.TurnOn();
        }
    }

    IEnumerator ShowEffect()
    {
        GameObject Damage_Eff = Objectpooling.poolingManager.GetEffectPool();
        Damage_Eff.SetActive(true);
        Damage_Eff.transform.position = new Vector3(tr.position.x, tr.position.y, -3f);

        yield return new WaitForSeconds(0.5f);
        Damage_Eff.SetActive(false);
    }
}
