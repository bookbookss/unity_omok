using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3Helper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static Vector3 StringToVector3(string str)
    {
        str = str.Replace("(", " ").Replace(")", " ");
        string[] s = str.Split(',');
        Vector3 result = new Vector3(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]));

        return result;
    }
}
