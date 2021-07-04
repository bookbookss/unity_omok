using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OmokPanel : MonoBehaviour
{
    NetworkController m_NetworkController;
    RawImage m_OmokPanelImage;
    void Start()
    {
        m_NetworkController = GameObject.Find("NetworkController").GetComponent<NetworkController>();
        m_OmokPanelImage = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {

        if (m_NetworkController.IsConnected)
        {
            m_OmokPanelImage.enabled = true;
        }


    }
}
