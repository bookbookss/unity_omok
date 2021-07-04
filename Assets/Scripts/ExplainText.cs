using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplainText : MonoBehaviour
{
    // Start is called before the first frame update

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
}
