using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerButton : MonoBehaviour
{
    NetworkController m_NetworkController;
    ServerConnectingImage m_ServerConnectingImage;

    void Start()
    {
        m_NetworkController = GameObject.Find("NetworkController").GetComponent<NetworkController>();
        m_ServerConnectingImage = GameObject.Find("ServerConnectingImage").GetComponent<ServerConnectingImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_NetworkController.IsConnected)
        {
            gameObject.SetActive(false);
        }

    }

    public void OnClicked()
    {
        m_NetworkController.ServerStart();
        m_ServerConnectingImage.ShowImage(true);
    }
}
