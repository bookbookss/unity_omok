using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;



[HideInInspector]
public enum CircleType
{
    Black,
    White,
    Null
}
public class CircleGenerator : MonoBehaviour
{
    MouseDetector m_MouseDetector;
    NetworkController m_NetworkController;
    SelectingTurnImage m_SelectingTurnImage;
    GameObject m_BlackCircle, m_WhiteCircle;
    RandomTurnController m_RandomTurnController;
    CircleChecker m_CircleChecker;

    int m_BlackCircleCount = 0, m_WhiteCircleCount = 0;
    bool m_BlackTurn = true, m_WhiteTurn = false;
    CircleType m_MyCircle = CircleType.Null;
    Vector3 m_MyCirPos = new Vector3(0, 0, 0);
    Vector3 m_OpponentCirPos = new Vector3(0, 0, 0);

    byte[] m_Buffer = new byte[64];



    #region DebugVariables
    //---------------------------------
    // --------- start ----------------
    //
    //
    // these variables are only used for debugging 
    // in Vec3PosText.cs
    // to check m_MyCirPos , m_OpponentCirPos , CircleType ,  BlackTurn  ,  WhiteTurn....

    public Vector3 MyCirPos
    {
        get
        {
            return m_MyCirPos;
        }
    }
    public Vector3 OpponentCirPos
    {
        get
        {
            return m_OpponentCirPos;
        }
    }

    public int _CircleType
    {
        get
        {
            return (int)m_MyCircle;
        }
    }

    public bool BlackTurn
    {
        get
        {
            return m_BlackTurn;
        }
    }

    public bool WhiteTurn
    {
        get
        {
            return m_WhiteTurn;
        }
    }


    //-------------- end ------------
    //-------------------------------
    #endregion





    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {

        if (m_NetworkController.gameState == GameState.StartState)
        {
            // Initialize CircleType between Black and White
            if (m_MyCircle == CircleType.Null)
            {
                InitializeCircle();
            }
            // Receive Circle Position from Server or Client
            else
            {
                ReceiveCirclePosition();
            }




            // Make Circles every turn  

            if(!m_SelectingTurnImage.IsOn)
            if (m_MouseDetector.IsMouseClicked() && m_MouseDetector.IsInRange())
            {
                InstantiateCircle();
            }
            


        }
    }


    void InitializeCircle()
    {


        if(m_NetworkController.IsServerMode)
        {
            switch (m_RandomTurnController._TurnState)
            {

                case TurnState.ServerFirstState:
                    m_MyCircle = CircleType.Black;
                    break;

                case TurnState.ClientFirstState:
                    m_MyCircle = CircleType.White;
                    break;
                
                default:
                    // do nothing
                    break;
            }
        }
        if(m_NetworkController.IsClientMode)
        {
            switch (m_RandomTurnController._TurnState)
            {

                case TurnState.ServerFirstState:
                    m_MyCircle = CircleType.White;
                    break;
                
                case TurnState.ClientFirstState:
                    m_MyCircle = CircleType.Black;
                    break;

                default:
                    // do nothing
                    break;
            }
        }



    }

    void InstantiateCircle()
    {
        if(m_NetworkController.IsServerMode)
        {
            if (m_BlackTurn && m_MyCircle == CircleType.Black)
            {
                if (Convert_m_MyCirclePos())
                {
                    InstantiateBlackCircle();
                    SendCirclePosition();
                }
            }
            if (m_WhiteTurn && m_MyCircle == CircleType.White)
            {
                if (Convert_m_MyCirclePos())
                {
                    InstantiateWhiteCircle();
                    SendCirclePosition();
                }
            }
        }
        if (m_NetworkController.IsClientMode)
        {
            if (m_BlackTurn && m_MyCircle == CircleType.Black)
            {
                if (Convert_m_MyCirclePos())
                {
                    InstantiateBlackCircle();
                    SendCirclePosition();
                }
            }
            if (m_WhiteTurn && m_MyCircle == CircleType.White)
            {
                if (Convert_m_MyCirclePos())
                {
                    InstantiateWhiteCircle();
                    SendCirclePosition();
                }
            }
        }

    }

    void InstantiateBlackCircle()
    {
        GenerateBlackCircle();
        Instantiate(m_BlackCircle, this.transform);
        SetBlackCirclePos();
        m_BlackTurn = false;
        m_WhiteTurn = true;
    }

    void InstantiateWhiteCircle()
    {
        GenerateWhiteCircle();
        Instantiate(m_WhiteCircle, this.transform);
        SetWhiteCirclePos();
        m_BlackTurn = true;
        m_WhiteTurn = false;
    }


    void SendCirclePosition()
    {
        if (m_MyCirPos.x != 0 && m_MyCirPos.y != 0)
        {
            m_Buffer = System.Text.Encoding.UTF8.GetBytes(m_MyCirPos.ToString());
            m_NetworkController.Send(m_Buffer);
        }
    }


    void ReceiveCirclePosition()
    {
        if(m_NetworkController.Receive(m_Buffer))
        {

            string str=System.Text.Encoding.UTF8.GetString(m_Buffer);
            m_OpponentCirPos = Vector3Helper.StringToVector3(str);

            SendTurnStateToCircleChecker(m_BlackTurn, m_WhiteTurn);
            SendPosToCircleChecker(m_OpponentCirPos.x, m_OpponentCirPos.y);


            switch (m_MyCircle)
            {
                    
                case CircleType.Black:

                    InstantiateWhiteCircle();

                    break;

                case CircleType.White:

                    InstantiateBlackCircle();

                    break;

            }

        }
        
    }


    void GenerateBlackCircle()
    {
        m_BlackCircle = new GameObject("blackCircle" + (++m_BlackCircleCount).ToString());
        m_BlackCircle.AddComponent<RawImage>();
        m_BlackCircle.GetComponent<RawImage>().texture = Resources.Load<Texture>("Materials/black_circle");
        m_BlackCircle.GetComponent<RectTransform>().localScale = new Vector3(0.4f, 0.4f, 0.4f);
    }


    void GenerateWhiteCircle()
    {
        m_WhiteCircle = new GameObject("whiteCircle" + (++m_WhiteCircleCount).ToString());
        m_WhiteCircle.AddComponent<RawImage>();
        m_WhiteCircle.GetComponent<RawImage>().texture = Resources.Load<Texture>("Materials/white_circle");
        m_WhiteCircle.GetComponent<RectTransform>().localScale = new Vector3(0.4f, 0.4f, 0.4f);
    }

    void SetBlackCirclePos()
    {
        GameObject blackCircleClone=GameObject.Find("blackCircle" + m_BlackCircleCount.ToString() + "(Clone)");

        if (m_BlackTurn && m_MyCircle == CircleType.Black)
        {

            blackCircleClone.transform.position = m_MyCirPos;

        }
        else
        {
            blackCircleClone.transform.position = m_OpponentCirPos;
        }

    }

    void SetWhiteCirclePos()
    {
        GameObject whiteCircleClone = GameObject.Find("whiteCircle" + m_WhiteCircleCount.ToString() + "(Clone)");

        if (m_WhiteTurn && m_MyCircle == CircleType.White)
        {

            whiteCircleClone.transform.position = m_MyCirPos;

        }
        else
        {
            whiteCircleClone.transform.position = m_OpponentCirPos;
        }

    }

    void Initialize()
    {
        m_MouseDetector = GameObject.Find("MouseDetector").GetComponent<MouseDetector>();
        m_NetworkController = GameObject.Find("NetworkController").GetComponent<NetworkController>();
        m_SelectingTurnImage = GameObject.Find("SelectingTurnImage").GetComponent<SelectingTurnImage>();
        m_RandomTurnController = GameObject.Find("RandomTurnController").GetComponent<RandomTurnController>();
        m_CircleChecker = GameObject.Find("CircleChecker").GetComponent<CircleChecker>();
    }



    /// <summary>
    /// Change m_MyCirPos Position To The Reasonable Position 
    /// </summary> 
    /// <returns>return true if m_MyCirclePos successfully get X and Y . </returns>
    bool Convert_m_MyCirclePos()
    {
        bool xOk = false;
        bool yOk = false;

        

        if (m_MouseDetector.Current_Position.x >= 152 && m_MouseDetector.Current_Position.x <= 160)
        {
            m_MyCirPos.x = 156;
            xOk = true;
        }
        if (m_MouseDetector.Current_Position.x >= 199 && m_MouseDetector.Current_Position.x <= 207) // 47
        {
            m_MyCirPos.x = 203;
            xOk = true;
        }
        if (m_MouseDetector.Current_Position.x >= 246 && m_MouseDetector.Current_Position.x <= 254) // 47
        {
            m_MyCirPos.x = 250;
            xOk = true;
        }
        if (m_MouseDetector.Current_Position.x >= 294 && m_MouseDetector.Current_Position.x <= 302) // 48
        {
            m_MyCirPos.x = 298;
            xOk = true;
        }
        if (m_MouseDetector.Current_Position.x >= 341 && m_MouseDetector.Current_Position.x <= 349) // 47
        {
            m_MyCirPos.x = 345;
            xOk = true;
        }
        if (m_MouseDetector.Current_Position.x >= 388 && m_MouseDetector.Current_Position.x <= 396) // 47
        {
            m_MyCirPos.x = 392;
            xOk = true;
        }
        if (m_MouseDetector.Current_Position.x >= 435 && m_MouseDetector.Current_Position.x <= 443) // 47
        {
            m_MyCirPos.x = 439;
            xOk = true;
        }
        if (m_MouseDetector.Current_Position.x >= 482 && m_MouseDetector.Current_Position.x <= 490) // 47
        {
            m_MyCirPos.x = 486;
            xOk = true;
        }
        if (m_MouseDetector.Current_Position.x >= 529 && m_MouseDetector.Current_Position.x <= 537) // 47
        {
            m_MyCirPos.x = 533;
            xOk = true;
        }
        if (m_MouseDetector.Current_Position.x >= 576 && m_MouseDetector.Current_Position.x <= 584) // 47
        {
            m_MyCirPos.x = 580;
            xOk = true;
        }
        if (m_MouseDetector.Current_Position.x >= 624 && m_MouseDetector.Current_Position.x <= 632) // 48
        {
            m_MyCirPos.x = 628;
            xOk = true;
        }
        if (m_MouseDetector.Current_Position.x >= 671 && m_MouseDetector.Current_Position.x <= 679) // 47
        {
            m_MyCirPos.x = 675;
            xOk = true;
        }
        if (m_MouseDetector.Current_Position.x >= 718 && m_MouseDetector.Current_Position.x <= 726) // 47
        {
            m_MyCirPos.x = 722;
            xOk = true;
        }
        if (m_MouseDetector.Current_Position.x >= 765 && m_MouseDetector.Current_Position.x <= 773) // 47
        {
            m_MyCirPos.x = 769;
            xOk = true;
        }
        if (m_MouseDetector.Current_Position.x >= 812 && m_MouseDetector.Current_Position.x <= 820) // 47
        {
            m_MyCirPos.x = 816;
            xOk = true;
        }
        if (m_MouseDetector.Current_Position.x >= 859 && m_MouseDetector.Current_Position.x <= 867) // 47
        {
            m_MyCirPos.x = 863;
            xOk = true;
        }
        if (m_MouseDetector.Current_Position.x >= 906 && m_MouseDetector.Current_Position.x <= 914) // 47
        {
            m_MyCirPos.x = 910;
            xOk = true;
        }






        if (m_MouseDetector.Current_Position.y >= 735 && m_MouseDetector.Current_Position.y <= 743)
        {
            m_MyCirPos.y = 739;
            yOk = true;
        }
        if (m_MouseDetector.Current_Position.y >= 690 && m_MouseDetector.Current_Position.y <= 698) // -45
        {
            m_MyCirPos.y = 694;
            yOk = true;
        }
        if (m_MouseDetector.Current_Position.y >= 646 && m_MouseDetector.Current_Position.y <= 654) // -44
        {
            m_MyCirPos.y = 650;
            yOk = true;
        }
        if (m_MouseDetector.Current_Position.y >= 601 && m_MouseDetector.Current_Position.y <= 609) // -45
        {
            m_MyCirPos.y = 605;
            yOk = true;
        }
        if (m_MouseDetector.Current_Position.y >= 556 && m_MouseDetector.Current_Position.y <= 564) // -45
        {
            m_MyCirPos.y = 560;
            yOk = true;
        }
        if (m_MouseDetector.Current_Position.y >= 511 && m_MouseDetector.Current_Position.y <= 519) //-45
        {
            m_MyCirPos.y = 515;
            yOk = true;
        }
        if (m_MouseDetector.Current_Position.y >= 466 && m_MouseDetector.Current_Position.y <= 474) // -45
        {
            m_MyCirPos.y = 470;
            yOk = true;
        }
        if (m_MouseDetector.Current_Position.y >= 422 && m_MouseDetector.Current_Position.y <= 430) // -44
        {
            m_MyCirPos.y = 426;
            yOk = true;
        }

        if (m_MouseDetector.Current_Position.y >= 377 && m_MouseDetector.Current_Position.y <= 385) // - 45
        {
            m_MyCirPos.y = 381;
            yOk = true;
        }
        if (m_MouseDetector.Current_Position.y >= 331 && m_MouseDetector.Current_Position.y <= 339) // - 46 
        {
            m_MyCirPos.y = 335;
            yOk = true;
        }

        if (m_MouseDetector.Current_Position.y >= 286 && m_MouseDetector.Current_Position.y <= 294) // - 45
        {
            m_MyCirPos.y = 290;
            yOk = true;
        }

        if (m_MouseDetector.Current_Position.y >= 242 && m_MouseDetector.Current_Position.y <= 250) // -44 
        {
            m_MyCirPos.y = 246;
            yOk = true;
        }
        if (m_MouseDetector.Current_Position.y >= 196 && m_MouseDetector.Current_Position.y <= 204) // - 46 
        {
            m_MyCirPos.y = 200;
            yOk = true;
        }
        if (m_MouseDetector.Current_Position.y >= 152 && m_MouseDetector.Current_Position.y <= 160) // - 44 
        {
            m_MyCirPos.y = 156;
            yOk = true;
        }

        if (m_MouseDetector.Current_Position.y >= 107 && m_MouseDetector.Current_Position.y <= 115) // -45
        {
            m_MyCirPos.y = 111;
            yOk = true;
        }

        if (m_MouseDetector.Current_Position.y >= 62 && m_MouseDetector.Current_Position.y <= 70) // -45
        { 
            m_MyCirPos.y = 66;
            yOk = true;
        }
        if (m_MouseDetector.Current_Position.y >= 17 && m_MouseDetector.Current_Position.y <= 25) // -45
        {
            m_MyCirPos.y = 21;
            yOk = true;
        }



        if (xOk && yOk)
        {
            if (m_CircleChecker.HasCircleAtPos(MyCirPos))
            {
                return false;
            }

            SendTurnStateToCircleChecker(m_BlackTurn, m_WhiteTurn);
            SendPosToCircleChecker(m_MyCirPos.x, m_MyCirPos.y);


            return true;

        }
        else
        {

            m_MyCirPos.x = 0;
            m_MyCirPos.y = 0;
            
            return false;
        }
    }


    /// <summary>
    /// send current position x and y to CircleChecker.cs 
    /// </summary>
    void SendPosToCircleChecker(float x, float y)
    {
        m_CircleChecker.CurrentCircleXPos = x;
        m_CircleChecker.CurrentCircleYPos = y;
    }



    /// <summary>
    /// send current turn state of BlackCircle and WhiteCircle to CircleChecker.cs
    /// </summary>
    void SendTurnStateToCircleChecker(bool blackTurn,bool whiteTurn)
    {
        m_CircleChecker.CurrentBlackTurn = blackTurn;
        m_CircleChecker.CurrentWhiteTurn = whiteTurn;
    }



}
