using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTurnController : MonoBehaviour
{

    NetworkController m_NetworkController;
    const int Min = 0, Max = 2;

    bool m_FinishRandomSetting = false;
    byte[] m_Buffer = new byte[1];

    TurnState m_TurnState;

    public TurnState _TurnState
    {
        get
        {
            return m_TurnState;
        }
    }


    void Start()
    {
        m_NetworkController = GameObject.Find("NetworkController").GetComponent<NetworkController>();
        StartCoroutine(SetRandomTurn());
     
    }

    // Update is called once per frame
    void Update()
    {

    }


    // Set Random Turn First and Last Randomly for Server and Client.
    // if it's Done it wont call again 
    IEnumerator SetRandomTurn()
    {


        while (!m_FinishRandomSetting)
        {

            if (m_NetworkController.gameState == GameState.RandomTurnSettingState)
            {


                if (m_NetworkController.IsServerMode)
                {

                    yield return new WaitForSeconds(5.0f);

                    m_TurnState = (TurnState)Random.Range(Min, Max);
                    m_Buffer[0] = (byte)m_TurnState;

                    m_NetworkController.Send(m_Buffer);
                    m_FinishRandomSetting = true;


                }



                if (m_NetworkController.IsClientMode)
                {

                    yield return new WaitForSeconds(0.0f);

                    if(m_NetworkController.Receive(m_Buffer))
                    { 

                    // Success Receiving Buffer From Server 

                        m_TurnState = (TurnState)m_Buffer[0];
                        m_FinishRandomSetting = true;

                    }

                }
            }



            yield return new WaitForSeconds(0.0f);
        }
    
    }

}
