using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimation : MonoBehaviour
{
    int times = 3;

    void Start()
    {
        transform.localScale = Vector3.one * 2;
    }
    void Update()
    {
        if (transform.localScale.x >= 1)
            transform.localScale -= Vector3.one * 2 * Time.deltaTime;
        else if (transform.localScale.x < 1)
        {
            transform.localPosition += new Vector3(-2, 1, 0) * Time.deltaTime;
        }
        if (transform.localPosition.x < 0)
        {
            transform.localScale = Vector3.one * 2;
            transform.localPosition = new Vector3(1.76f, -2.23f, 0);
            times--;
        }

#if UNITY_STANDALONE_WIN
        if (Input.GetKeyDown(KeyCode.Mouse0) || times < 0)
        {
#endif

#if UNITY_ANDROID
        if (Input.touchCount == 1 || times < 0)
        {
#endif
            Destroy(gameObject);
        }
    }
}
