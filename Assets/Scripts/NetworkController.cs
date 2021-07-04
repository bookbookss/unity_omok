using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class NetworkController : MonoBehaviour
{

    Socket m_Server, m_Client;

    IPInputField m_IPInputField;

    const int PORT = 2000;

    bool m_IsConnected = false, m_IsClientMode = false, m_IsServerMode = false;

    [HideInInspector]
    public GameState gameState=GameState.ReadyState;

    public bool IsConnected
    {
        get
        {
            return m_IsConnected;
        }
    }

    public bool IsClientMode
    {
        get
        {
            return m_IsClientMode;
        }
    }

    public bool IsServerMode
    {
        get
        {
            return m_IsServerMode;

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        m_IPInputField = GameObject.Find("IPInputField").GetComponent<IPInputField>();

    }

    // Update is called once per frame
    void Update()
    {
        if (m_Server != null && m_Client == null) 
            AcceptClient();


        if (m_IsConnected)
        {

        }
        else
        {
            InitializeServer();
            InitializeClient();
        }

    }


    public void ServerStart()
    {
        try
        {
            m_Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            m_Server.Bind(new IPEndPoint(IPAddress.Parse(m_IPInputField.IP_Address), PORT));
            m_Server.Listen(1);
        }
        catch( SocketException se)
        {
            Debug.Log(se.Message);
            m_Server.Close();
            m_Server = null;
        }

    }

    
    public void ClientStart()
    {
        try
        {
            m_Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            m_Client.NoDelay = true;
            m_Client.SendBufferSize = 0;
            m_Client.Connect(IPAddress.Parse(m_IPInputField.IP_Address), PORT);

        }
        catch(SocketException se)
        {
            Debug.Log(se.Message+"\n");
            m_Client.Close();
            m_Client = null;

        }
    }

    public void Send(byte[] buffer)
    {
        if (m_IsServerMode || m_IsClientMode)
        {
            NetSendRevController.SendData(buffer, m_Client, ref gameState);
        }

    }

       
     /// <summary>
     /// return true if it successfully receives a buffer.
     /// otherwise it returns false
     /// </summary>
     /// <param name="buffer"></param>
     /// <returns></returns>
    public bool Receive(byte[] buffer)
    {

        if (m_IsServerMode || m_IsClientMode)
        {
            return NetSendRevController.ReceiveData(buffer,m_Client, ref gameState);
        }
        else
        {
            return false;
        }

    }


    private void AcceptClient()
    {
        if (m_Server.Poll(0, SelectMode.SelectRead)) 
        {
            m_Client = m_Server.Accept();
        }
    }


    private void InitializeServer()
    {

        if (m_Server != null && m_Client != null)
        {

            if (m_Server.Available == 0 && m_Client.Available == 0)
            {

                m_IsConnected = true;
                m_IsServerMode = true;

                gameState = GameState.RandomTurnSettingState;
            }
        }

    }
    


    private void InitializeClient()
    {

        if (m_Client != null)
        if (m_Client.Available == 0 && !m_IsServerMode)
        {

            m_IsConnected = true;
            m_IsClientMode = true;
            gameState = GameState.RandomTurnSettingState;

        }

    }

   


}
