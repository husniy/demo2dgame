using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;//
using System.Reflection;

public class SnakeBodyCollider : MonoBehaviour
{
    private List<Transform> tf = new List<Transform>();
    GameObject snake;
    BoneRenderer br;
    DampedTransform dt;
    //int bodyLength;
    new AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        //snake = GameManager.GM.snake;
        //br = snake.GetComponent<BoneRenderer>();
        //for (int i = 0; i < br.transforms.Length; i++)
        //{
        //    tf.Add(br.transforms[i]);
        //}
        //bodyLength = 5;
        //buffOpen = false;
    }
    private void OnCollisionEnter2D(Collision2D other) {

        //component是gameobject的组件，component包括transform、animation、自己新建的脚本等等
        if (other.gameObject.CompareTag("coin")){//碰金币时
            audio.Play();
            Destroy(other.gameObject);
            GameManager.GM.Score++;
            //Destroy(gameObject); //这是摧毁蛇身自己
            //bodyLength++;
                                                                         
            //-----------蛇身加长-------------
            //复制蛇身
            //GameObject oldChildBody = GameManager.GM.childBody;
            //GameObject newChildBody = GameObject.Instantiate(oldChildBody,oldChildBody.transform,true);//复制蛇尾
            //newChildBody.transform.localPosition = new Vector3(0, -0.2f, 0);
            //GameManager.GM.childBody = newChildBody;

            ////绑定新的bone
            ////br.transforms[bodyLength] = newChildBody.transform;
            //tf.Add(newChildBody.transform);
            //br.transforms = new Transform[br.transforms.Length + 1];
            //for (int i = 0; i < br.transforms.Length; i++)
            //{
            //    br.transforms[i] = tf[i];
            //}

            ////绑定新的damped
            //GameObject oldDamped = GameManager.GM.damped;
            //GameObject newDamped = GameObject.Instantiate(oldDamped, GameManager.GM.rig1.transform);
            //GameManager.GM.damped = newDamped;

            //dt = newDamped.GetComponent<DampedTransform>();
            //dt.data.constrainedObject = newChildBody.transform;
            //dt.data.sourceObject = oldChildBody.transform;
            ////dt.UpdateJob(dt.);

        }
        if (other.gameObject.CompareTag("wall"))
        {//撞墙时
#if UNITY_STANDALONE_WIN
            GameManager.GM.PunishTime = Time.deltaTime * 0.05f; //蛇身会突然失控一下（增加难度

#endif

#if UNITY_ANDROID
            GameManager.GM.PunishTime = Time.deltaTime * 0.2f; //手机失控的时间比电脑的长一点会比较有挑战性
#endif
            other.gameObject.tag = "Untagged"; //更改墙的tag，以避免蛇身因为不断撞墙而持续失控

        }
        if (other.gameObject.CompareTag("buff"))
        {//撞buff时

            GameManager.GM.BuffTime = Time.deltaTime * 5f;
            Destroy(other.gameObject);
            //buffOpen = true;
        }
        //if (other.gameobject.comparetag("thorn")&&buffopen)
        //{//

        //    gamemanager.gm.bufftime -= time.deltatime * 5f;
        //    destroy(gameobject.findgameobjectswithtag(""));
        //    buffopen = false;
        //}
    }
}
