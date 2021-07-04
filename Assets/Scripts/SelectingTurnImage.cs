using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectingTurnImage : MonoBehaviour
{
    Texture m_MyTurnFirstImg, m_MyTurnLastImg, m_SelectTurnImg;
    RawImage m_ImageSelecter;
    NetworkController m_NetworkController;
    RandomTurnController m_RandomTurnController;


    void Start()
    {
        Initialize();
        StartCoroutine(ShowImage());
    }

    // Update is called once per frame
    void Update()
    {


    }

    /// <summary>
    /// return true if Image is currently on .
    /// otherwise it returns false
    /// </summary>
    public bool IsOn
    {
        get
        {
            return m_ImageSelecter.enabled;
        }
    }

    void Initialize()
    {
        m_MyTurnFirstImg = Resources.Load<Texture>("Materials/my_turn_first_image");
        m_MyTurnLastImg = Resources.Load<Texture>("Materials/my_turn_last_image");
        m_SelectTurnImg = Resources.Load<Texture>("Materials/selecting_turn_image");
        m_ImageSelecter = GetComponent<RawImage>();
        m_NetworkController = GameObject.Find("NetworkController").GetComponent<NetworkController>();
        m_RandomTurnController = GameObject.Find("RandomTurnController").GetComponent<RandomTurnController>();
    }




    IEnumerator ShowImage()
    {
        while(true)
        {
            if (m_NetworkController.gameState == GameState.RandomTurnSettingState)
            {
                m_ImageSelecter.texture = m_SelectTurnImg;
                m_ImageSelecter.enabled = true;
            }

            if (m_NetworkController.gameState == GameState.StartState)
            {

                if (m_NetworkController.IsServerMode)
                {

                    switch (m_RandomTurnController._TurnState)
                    {

                        case TurnState.ServerFirstState:
                            m_ImageSelecter.texture = m_MyTurnFirstImg;
                            break;

                        case TurnState.ClientFirstState:
                            m_ImageSelecter.texture = m_MyTurnLastImg;
                            break;

                        default:
                            break;
                    }

                    yield return new WaitForSeconds(3.0f);
                    m_ImageSelecter.enabled = false;
                    
                }

                if(m_NetworkController.IsClientMode)
                {

                    switch (m_RandomTurnController._TurnState)
                    {
                        case TurnState.ServerFirstState:
                            m_ImageSelecter.texture = m_MyTurnLastImg;
                            break;

                        case TurnState.ClientFirstState:
                            m_ImageSelecter.texture = m_MyTurnFirstImg;
                            break;

                        default:
                            break;

                    
                    }

                    yield return new WaitForSeconds(3.0f);
                    m_ImageSelecter.enabled = false;
                }


            }

            yield return new WaitForSeconds(0.0f);
        }
    }
}
