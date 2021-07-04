using System.Collections;
using System.Collections.Generic;
using System;
using System.Net.Sockets;
using UnityEngine;

public class NetSendRevController : MonoBehaviour
{
    static ServerDisconnectedImage m_ServerDisconnectedImage;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public static void SendData(byte[] buffer, Socket socket , ref GameState gameState)
    {
        
        switch (gameState)
        {
            case GameState.ReadyState:
                // do nothing
                break;


            case GameState.RandomTurnSettingState:

                // server is sending data to client 
                // in RandomTurnController.SetRandomTurn()

                if (socket != null && socket.Poll(0, SelectMode.SelectWrite))
                {
                    socket.Send(buffer, buffer.Length, SocketFlags.None);
                    gameState = GameState.StartState;
                }

                break;

            case GameState.StartState:

                if (socket != null && socket.Poll(0,SelectMode.SelectWrite))
                {
                    socket.Send(buffer, buffer.Length, SocketFlags.None);
                }

                break;

            case GameState.DisconnectState:
                // do nothing
                break;

            case GameState.GameOverState:
                // do nothing
                break;
                
        }


    }

    public static bool ReceiveData(byte[] buffer , Socket socket,ref GameState gameState)
    {

        switch (gameState)
        {
            case GameState.ReadyState:
                // do nothing
                break;

            case GameState.RandomTurnSettingState:




                if (socket != null && socket.Poll(0,SelectMode.SelectRead))
                {
                    //client receives data from server 
                    // in RandomTurnController.SetRandomTurn()

                    int recvSize = socket.Receive(buffer, buffer.Length, SocketFlags.None);

                    if (recvSize == 0)
                    {
                        DisconnectSocket(socket,ref gameState);
                        break;
                    }

                    if (recvSize > 0)
                    {
                        TurnState turnState = (TurnState)buffer[0];
                        Debug.Log("Turn State: " + turnState + "\n");
                        gameState = GameState.StartState;
                        return true;
                    }

                }

                break;

            case GameState.StartState:


                if (socket != null && socket.Poll(0,SelectMode.SelectRead))
                {
                    int recvSize = socket.Receive(buffer, buffer.Length, SocketFlags.None);

                    if (recvSize == 0)
                    {
                        DisconnectSocket(socket,ref gameState);
                        break;
                    }

                    if (recvSize > 0)
                    {
                        string str = System.Text.Encoding.UTF8.GetString(buffer);
                        Debug.Log("Vector3 state: " + str + "\n");

                        return true;
                    }

                }
                break;

            case GameState.DisconnectState:
                // do nothing
                break;

            case GameState.GameOverState:
                // do nothing
                break;
        }

        return false;


    }

    private static void DisconnectSocket(Socket socket,ref GameState gameState)
    {
    
        socket.Close();
        socket = null;

        m_ServerDisconnectedImage = GameObject.Find("ServerDisconnectedImage").GetComponent<ServerDisconnectedImage>();

        if (gameState != GameState.GameOverState)
        {
            m_ServerDisconnectedImage.ShowImage(true);
        }

        gameState = GameState.DisconnectState;
        Debug.Log("Server Disconnected");
    
    }

}
