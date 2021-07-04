using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSizeController : MonoBehaviour
{

    const int Width = 1024, Height = 768;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Screen.width != Width || Screen.height != Height) 
        {
            Screen.SetResolution(Width, Height, false);
        }
    }
}
