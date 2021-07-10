using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHelper : MonoBehaviour
{
    public float size = 1;

    public float transparency = .2f;

    bool reset = false;
    bool running = false;

    public void Hit()
    {
        if (running)
        {
            reset = true;
        }
        else
        {
            StartCoroutine(Animate());
        }
    }

    void Start()
    {
        transform.localScale = new Vector3(1, 1, 1) * size;
        SetOpacity(0);
    }

    void Update()
    {
        
    }

    IEnumerator Animate()
    {
        while (running)
        {
            yield return null;
        }

        running = true;
        SetOpacity(transparency);
        for (int i = 10 - 1; i >= 0; i--)
        {
            if (reset)
            {
                reset = false;
                running = false;
                StartCoroutine(Animate());
                yield break;
            }

            SetOpacity(transparency / 10 * i);

            yield return i;
        }
        SetOpacity(0);
        running = false;
    }

    void SetOpacity(float num)
    {
        //Debug.Log(num);
        var color = GetComponent<MeshRenderer>().material.color;
        color.a = num;
        GetComponent<MeshRenderer>().material.color = color;
    }
}
