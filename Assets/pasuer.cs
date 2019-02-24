using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class pasuer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            VuforiaRenderer.Instance.Pause(true);
        }
        if (Input.GetKey(KeyCode.M))
        {
            VuforiaRenderer.Instance.Pause(false);
        }

    }
}
