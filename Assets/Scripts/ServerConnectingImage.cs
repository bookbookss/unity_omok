using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerConnectingImage : MonoBehaviour
{

    NetworkController m_NetworkController;
    RawImage m_ServerConnectingMessageRawImage;
    
    public void ShowImage(bool enable)
    {
        if (m_ServerConnectingMessageRawImage != null)
        {
            m_ServerConnectingMessageRawImage.enabled = enable;
        }
    }
    void Start()
    {
        m_ServerConnectingMessageRawImage = GetComponent<RawImage>();
        m_NetworkController = GameObject.Find("NetworkController").GetComponent<NetworkController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_NetworkController.IsConnected)
        {
            gameObject.SetActive(false);
        }
    }
}
