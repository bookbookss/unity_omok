using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IPInputField : MonoBehaviour
{

    NetworkController m_NetworkController;
    public string IP_Address 
    {
        get
        {
            return GetComponent<InputField>().text;
        }
    }


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

}
