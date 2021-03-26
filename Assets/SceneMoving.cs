using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMoving : MonoBehaviour
{
    public int[] index = new int[6 * 10];

    public GameObject coinPrefab;
    List<GameObject> coins = new List<GameObject>();//储存所有生成的coin
    int coinRandomAmount = 0;

    public GameObject wallPrefab;
    List<GameObject> walls = new List<GameObject>();//储存所有生成的wall
    int wallRandomAmount = 0;

    //public GameObject buffPrefab;

    void Start()
    {
        for (int i = 0; i < index.Length; i++)
            index[i] = i;
        randomIndex();//随机不重复值生成
        setGameObjects();//根据随机值生成金币和墙
    }

    void Update()
    {
        transform.Translate(Vector2.up * GameManager.GM.Speed * Time.deltaTime);

        if (transform.position.y > 9.6f){
            transform.position = new Vector3(0, -9.6f,1);
            clearGameObjects();//先清除已经生成的东西
            randomIndex();//随机不重复值生成
            setGameObjects();//根据随机值生成金币和墙
        }
    }

    void clearGameObjects(){
        coins.Clear();
        walls.Clear();
        //buff.Clear();
		for (int i = 0; i < transform.childCount; i++) {
            if(transform.GetChild (i).gameObject.tag!="background")
    			Destroy (transform.GetChild (i).gameObject); 
		}
    }
    void randomIndex(){
        for (int i = 0; i < index.Length; i++){
            int r = (int)Random.Range(i, index.Length);
            int temp = index[i];
            index[i] = index[r];
            index[r] = temp;
        }
    }
    void setGameObjects(){
        coinRandomAmount=Random.Range(1, 6);//同屏coin数量随机值
        wallRandomAmount=coinRandomAmount + Random.Range(3, 8);//同屏wall数量随机值
        addGameObjects(0,coinRandomAmount,coinPrefab,coins);
        addGameObjects(coinRandomAmount,wallRandomAmount,wallPrefab,walls);
        //addGameObjects(wallRandomAmount,buffRandomAmount,buffPrefab,buffs);

        //float x = Random.Range(-4f,4f);
        //GameObject OBJ = Instantiate(buffPrefab, transform);
        //OBJ.transform.position = new Vector2(x, 4.4f);
        //Debug.Log(OBJ);
    }
    void addGameObjects(int i,int RandomAmount,GameObject Prefab,List<GameObject> s){
        for(;i<RandomAmount;i++){
            float x=index[i]%6;
            float y=index[i]/6;
            x=0.6f*x-1.8f;
            y=0.8f*y-4f;
            Vector2 xy=new Vector2(x,y);
            GameObject OBJ = Instantiate(Prefab,transform);
            OBJ.transform.localPosition=xy;
            s.Add(OBJ);
        }
    }
}
