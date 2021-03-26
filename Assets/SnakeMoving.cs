using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class SnakeMoving : MonoBehaviour
{
    public GameObject player;
    public GameObject button;

    public UnityEngine.Animations.Rigging.Rig rig;
    private bool flag = false;
    Vector3 clickSnakePosition;
    Vector3 clickPlayerPosition;
    Vector3 clickSnakeNextPosition;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player");
        if (rig == null)
            rig = GetComponentInChildren<UnityEngine.Animations.Rigging.Rig>();
        button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y > 7f)//蛇身离开场景后 重新开始的按钮出现
        {
            GameManager.GM.buttonState = true;
            //button.SetActive(true);
        }
        if (GameManager.GM.PunishTime > 0f)//惩罚倒计时
            GameManager.GM.PunishTime -= Time.deltaTime * 0.01f;

#if UNITY_STANDALONE_WIN
        MouseManage();
#endif

#if UNITY_ANDROID
        TouchManage();
#endif
    }

#if UNITY_STANDALONE_WIN

    public void MouseManage()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0) || GameManager.GM.PunishTime > 0f)//鼠标松开时 或 被惩罚时，蛇身保持此时的状态
        {
            flag = false;
            rig.weight = 0f;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))//鼠标按住蛇头 的状态
        {
            clickSnakePosition = player.transform.position;
            clickPlayerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (clickSnakeNextPosition == null)
                clickSnakeNextPosition = clickSnakePosition;
            if (clickSnakePosition.x - 0.3 < clickPlayerPosition.x &&
            clickPlayerPosition.x < clickSnakePosition.x + 0.3 &&
            clickSnakePosition.y - 0.3 < clickPlayerPosition.y &&
            clickPlayerPosition.y < clickSnakePosition.y + 0.3)
            {
                flag = true;
            }
            rig.weight = 1f;
        }
        if (GameManager.GM.PunishTime <= 0f && flag)//没有被惩罚 且 鼠标按住时 的状态
        {
            player.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10f;//控制蛇头移动，z轴要补个10，因为摄像机的z是-10
            if (clickSnakeNextPosition != clickSnakePosition)
                player.transform.rotation = Quaternion.Euler(0, 0, (float)(Math.Atan2((clickSnakePosition.y - clickSnakeNextPosition.y), (clickSnakePosition.x - clickSnakeNextPosition.x)) * 180 / Math.PI));
        }
        else
        {
            transform.Translate(Vector2.up * GameManager.GM.Speed * Time.deltaTime);//蛇不受控制时，会和场景一起移动
            //transform.Translate(0, 0, Time.deltaTime * 10); //物体沿着自身Z轴方向，每秒移动物体10米运动
        }
        clickSnakeNextPosition = clickSnakePosition;

    }

#endif


#if UNITY_ANDROID
    public void TouchManage()
    {
        //Touch touch = new Touch();

        if (Input.touchCount == 0 || GameManager.GM.PunishTime > 0f)//没有触碰时 或 被惩罚时，蛇身保持此时的状态
        {
            flag = false;
            rig.weight = 0f;
        }
        if (Input.touchCount == 1)//触碰蛇头 的状态（只需要一个触碰点）
        {
            clickSnakePosition = player.transform.position;
            clickPlayerPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            if (clickSnakeNextPosition == null)
                clickSnakeNextPosition = clickSnakePosition;
            if (clickSnakePosition.x - 0.3 < clickPlayerPosition.x &&
            clickPlayerPosition.x < clickSnakePosition.x + 0.3 &&
            clickSnakePosition.y - 0.3 < clickPlayerPosition.y &&
            clickPlayerPosition.y < clickSnakePosition.y + 0.3)
            {
                flag = true;
            }
            rig.weight = 1f;
        }
        if (GameManager.GM.PunishTime <= 0f && flag)//没有被惩罚 且 鼠标按住时 的状态
        {
            player.transform.position = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) + Vector3.forward * 10f;//控制蛇头移动，z轴要补个10，因为摄像机的z是-10
            if (clickSnakeNextPosition != clickSnakePosition)
                player.transform.rotation = Quaternion.Euler(0, 0, (float)(Math.Atan2((clickSnakePosition.y - clickSnakeNextPosition.y), (clickSnakePosition.x - clickSnakeNextPosition.x)) * 180 / Math.PI));
        }
        else
        {
            transform.Translate(Vector2.up * GameManager.GM.Speed * Time.deltaTime);//蛇不受控制时，会和场景一起移动
            //transform.Translate(0, 0, Time.deltaTime * 10); //物体沿着自身Z轴方向，每秒移动物体10米运动
        }
        clickSnakeNextPosition = clickSnakePosition;

    }
#endif


}
