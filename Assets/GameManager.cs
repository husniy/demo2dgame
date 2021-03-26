using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    public float Speed = 1f;
    public float SpeedScale = 0.01f;//每秒增加的速度
    public float PunishTime = 0f;
    public float BuffTime = 0f;
    public int Score = 0;
    public bool buttonState = false;
    public Text textMiddle;
    public GameObject childBody;//记录蛇尾对象
    public GameObject damped;//记录链接蛇尾对象的damped
    public GameObject snake;//整条蛇的manager
    public GameObject rig1;
    public GameObject fence1;
    public GameObject fence2;
    public GUIStyle customGuiStyle;


    private void Awake()//脚本实例化
    {
        GM = this;
    }
    private void Start()
    {
        fence1.transform.position = Vector3.left * Screen.width / Screen.height * 5;
        fence2.transform.position = Vector3.right * Screen.width / Screen.height * 5;
        customGuiStyle.fontSize = Screen.width / 6;
    }


    // Update is called once per frame
    void Update()
    {
        Speed += Time.deltaTime * SpeedScale;//Speed每秒增加0.01f
        textMiddle.text = Score.ToString();

    }
    public void OnRestart()
    {
        //已弃用
    }

    private void OnGUI()
    {

        if (buttonState)
        {
            if (GUI.Button(new Rect(Screen.width * 0.2f, Screen.height * 0.4f, Screen.width * 0.6f, Screen.height * 0.1f), "Restart", customGuiStyle))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                buttonState = false;
            }
        }

    }
}