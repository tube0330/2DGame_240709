using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch_pad : MonoBehaviour
{
    private RectTransform touch_rect;
    private Vector3 startPos = Vector3.zero;
    public float r = 120f;
    private int touch_ID = -1; //원안에서 터치 인지 아닌지 판단
    private bool BtnPress = false; //버튼을 누른 상태인지
    public Vector3 diff;
    [SerializeField] private RocketCtrl rocket_ctrl;
    void Start()
    {
        rocket_ctrl = GameObject.FindWithTag("Player").GetComponent<RocketCtrl>();
        touch_rect = GetComponent<RectTransform>();
        startPos = touch_rect.position;
    }
    public void ButtonDown() { BtnPress = true; }
    public void ButtonUp() { BtnPress = false; }

    private void FixedUpdate()
    {
        if (Application.platform == RuntimePlatform.Android)
            HandleTouchInput();

        if (Application.platform == RuntimePlatform.WindowsEditor)
            HandleInput(Input.mousePosition);
    }
    void HandleTouchInput()
    {
        int i = 0;

        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                i++;

                Vector2 touchPos = new Vector2(touch.position.x, touch.position.y);

                if (touch.phase == TouchPhase.Began)
                {
                    if (touch.position.x <= (startPos.x + r))
                        touch_ID = i;

                    if (touch.position.y <= (startPos.y + r))
                        touch_ID = i;
                }

                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    if (touch_ID == i)
                        HandleInput(touchPos);
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    if (touch_ID == i)
                        touch_ID = -1;
                }
            }
        }
    }
    void HandleInput(Vector3 input)
    {
        if (BtnPress)
        {
            Vector3 diffVector = (input - startPos);

            if (diffVector.sqrMagnitude > r * r)
            {
                diffVector.Normalize();
                touch_rect.position = startPos + diffVector * r;
            }
            else
            {
                touch_rect.position = input;
            }

        }
        else
        {
            touch_rect.position = startPos;
        }

        diff = touch_rect.position - startPos;

        Vector2 normalDiff = new Vector2(diff.x / r, diff.y / r);

        if (rocket_ctrl != null)
            rocket_ctrl.OnStickPos(normalDiff);

    }

}
