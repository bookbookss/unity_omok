using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ServerDisconnectedImage : MonoBehaviour
{

    RawImage m_ServerDisconnectedImg;

    // Start is called before the first frame update
    void Start()
    {
        m_ServerDisconnectedImg = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowImage(bool enable)
    {
        m_ServerDisconnectedImg.enabled = enable;
    }
}
