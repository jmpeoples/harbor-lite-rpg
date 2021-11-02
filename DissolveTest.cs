using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveTest : MonoBehaviour
{
    // Start is called before the first frame update
    Renderer rend;

    public float currentDissolveValue = 0f;
    public float dissolveMax = 1.2f;
    public float dissolveOutTime;


    void Start()
    {
        rend = gameObject.GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(dissolve());
        }
    }

    public IEnumerator dissolve()
    {

        while (currentDissolveValue < dissolveMax)
        {
            currentDissolveValue += Time.deltaTime / dissolveOutTime;
            //Debug.Log(currentDissolveValue);
            rend.material.SetFloat("_dissolveTime", currentDissolveValue);
            yield return null;

        }

    }
}
