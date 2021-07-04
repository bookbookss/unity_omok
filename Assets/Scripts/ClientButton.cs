using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientButton : MonoBehaviour
{
    NetworkController m_NetworkController;
    void Start()
    {
        m_NetworkController = GameObject.Find("NetworkController").GetComponent<NetworkController>();
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
        m_NetworkController.ClientStart();
    }
}
