using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleChecker : MonoBehaviour
{
    const int X = 17, Y = 17;

    NetworkController m_NetworkController;

    GameEndedImage m_GameEndedImage;

    WinState m_WinState = WinState.Null; 

    /// <summary>
    /// m_SelectedCircles is a variable for checking all the circles on omok_panel
    /// </summary>
    
    bool[,] m_ExistedCircles = new bool[X, Y];

    
    // these m_BlackCircles variables are currently facing on omok_panel

    bool[,] m_BlackCircles = new bool[X, Y];


    // these m_WhiteCircles variables are currently facing on omok_panel

    bool[,] m_WhiteCircles = new bool[X, Y];



    [HideInInspector]
    public float CurrentCircleXPos 
    {
        get;set;
    }

    [HideInInspector]
    public float CurrentCircleYPos
    {
        get;set;
    }

    [HideInInspector]
    public bool CurrentBlackTurn
    {
        get;set;
    }

    [HideInInspector]
    public bool CurrentWhiteTurn
    {
        get;set;
    }




    void Start()
    {
        Initialize(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(m_NetworkController.gameState==GameState.StartState)
        {
            CheckCircles();
            DetectBlackWin();
            DetectWhiteWin();
        }
        else
        {
            ResetExistedCircles();
            ResetBlackCircles();
            ResetWhiteCircles();
        }


    }


    void Initialize()
    {

        m_NetworkController = GameObject.Find("NetworkController").GetComponent<NetworkController>();
        m_GameEndedImage = GameObject.Find("GameEndedImage").GetComponent<GameEndedImage>();
        CurrentBlackTurn = false;
        CurrentWhiteTurn = false;

    }
    


    void ResetExistedCircles()
    {
        for (int i = 0; i < X; i++)
        {
            for (int i2 = 0; i2 < Y; i2++)
            {
                m_ExistedCircles[i, i2] = false;
            }
        }
    }


    void ResetBlackCircles()
    {
        for (int i = 0; i < X; i++)
        {
            for (int i2 = 0; i2 < Y; i2++)
            {
                m_BlackCircles[i, i2] = false;
            }
        }
    }



    void ResetWhiteCircles()
    {
        for (int i = 0; i < X; i++)
        {
            for (int i2 = 0; i2 < Y; i2++)
            {
                m_WhiteCircles[i, i2] = false;
            }
        }
    }

    /// <summary>
    /// if there are circles on opok_panel .  
    /// tell the variable "m_ExistedCircles" that 
    /// some specific Circles are already in use on omok_panel
    /// </summary>
    void CheckCircles()
    {
        int x = 0 ;
        int y = 0 ;

        switch (CurrentCircleXPos)
        {
            case 156:
                x = 0;
                break;
            case 203:
                x = 1;
                break;
            case 250:
                x = 2;
                break;
            case 298:
                x = 3;
                break;
            case 345:
                x = 4;
                break;
            case 392:
                x = 5;
                break;
            case 439:
                x = 6;
                break;
            case 486:
                x = 7;
                break;
            case 533:
                x = 8;
                break;
            case 580:
                x = 9;
                break;
            case 628:
                x = 10;
                break;
            case 675:
                x = 11;
                break;
            case 722:
                x = 12;
                break;
            case 769:
                x = 13;
                break;
            case 816:
                x = 14;
                break;
            case 863:
                x = 15;
                break;
            case 910:
                x = 16;
                break;
            default: return;
        }

        switch (CurrentCircleYPos)
        {
            case 739:
                y = 0;
                break;
            case 694:
                y = 1;
                break;
            case 650:
                y = 2;
                break;
            case 605:
                y = 3;
                break;
            case 560:
                y = 4;
                break;
            case 515:
                y = 5;
                break;
            case 470:
                y = 6;
                break;
            case 426:
                y = 7;
                break;
            case 381:
                y = 8;
                break;
            case 335:
                y = 9;
                break;
            case 290:
                y = 10;
                break;
            case 246:
                y = 11;
                break;
            case 200:
                y = 12;
                break;
            case 156:
                y = 13;
                break;
            case 111:
                y = 14;
                break;
            case 66:
                y = 15;
                break;
            case 21:
                y = 16;
                break;
            default:
                return;
        }

            m_ExistedCircles[x, y] = true;

            CheckBlackCircles(x, y);
            CheckWhiteCircles(x, y);

    }



    /// <summary>
    ///  detect black circles on omok_panel
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    
    void CheckBlackCircles(int x, int y)
    {
        if (CurrentBlackTurn) 
        {
            m_BlackCircles[x, y] = true;
        }
    }


    /// <summary>
    /// detect white circles on omok_panel
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    void CheckWhiteCircles(int x , int y)
    {
        if (CurrentWhiteTurn) 
        {
            m_WhiteCircles[x, y] = true;
        }
    }



    void DetectBlackWin()
    {

        int count = 0;

        // detect row

        for (int i = 0; i < X; i++)
        {
            for (int i2 = 0; i2 < Y; i2++)
            {
                if (m_BlackCircles[i, i2] == true) 
                {
                    count++;
                }
                else
                {
                    count = 0;
                }

                if (count == 5)
                {
                    BlackWin();
                }
            }

        }


        count = 0;

        // detect column

        for (int i2 = 0; i2 < Y; i2++)
        {
            for (int i = 0; i < X; i++)
            {
                if (m_BlackCircles[i, i2] == true) 
                {
                    count++;
                }
                else
                {
                    count = 0;
                }
                if (count == 5)
                {
                    BlackWin();
                }
            }

        }

        count = 0;


        // detect diagonal

        for (int i = 0; i < X; i++) 
        {
            for (int i2 = 0; i2 < Y; i2++)
            {

                if (i + i2 > 16)
                    break;

                if (m_BlackCircles[i + i2, i2] == true) 
                {
                    count++;
                }
                else
                {
                    count = 0;
                }
                if (count == 5)
                {
                    BlackWin();
                }

            }
        }





        count = 0;


        // detect diagonal

        for (int i2 = 0; i2 < Y; i2++) 
        {
            for (int i = 0; i < X; i++)
            {

                if (i + i2 > 16)
                    break;

                if (m_BlackCircles[i, i + i2] == true) 
                {
                    count++;
                }
                else
                {
                    count = 0;
                }
                if (count == 5)
                {
                    BlackWin();
                }

            }
        }

        count = 0;


        // detect diagonal

        for (int i2 = 16; i2 >= 0; i2--) 
        {
            for (int i = 0; i < X; i++)
            {

                if (i2 - i < 0)
                    break;

                if (m_BlackCircles[i, i2 - i]==true)
                {
                    count++;
                }
                else
                {
                    count = 0;
                }
                if (count == 5)
                {
                    BlackWin();
                }
            }
        }



        count = 0;


        // detect diagonal

        for (int i = 0; i < X; i++) 
        {
            for (int i2 = 0; i2 <Y; i2++) 
            {
                if (i + i2 > 16)
                    break;

                if (m_BlackCircles[i + i2, 16 - i2] == true)  
                {
                    count++;
                }
                else
                {
                    count = 0;
                }
                if (count == 5) 
                {
                    BlackWin();
                }
            }
        }



    }

    void DetectWhiteWin()
    {

        int count = 0;

        // detect row

        for (int i = 0; i < X; i++)
        {
            for (int i2 = 0; i2 < Y; i2++)
            {
                if (m_WhiteCircles[i, i2] == true)
                {
                    count++;
                }
                else
                {
                    count = 0;
                }

                if (count == 5)
                {
                    WhiteWin();
                }
            }

        }


        count = 0;

        // detect column

        for (int i2 = 0; i2 < Y; i2++)
        {
            for (int i = 0; i < X; i++)
            {
                if (m_WhiteCircles[i, i2] == true)
                {
                    count++;
                }
                else
                {
                    count = 0;
                }
                if (count == 5)
                {
                    WhiteWin();
                }
            }

        }



        count = 0;


        // detect diagonal

        for (int i = 0; i < X; i++)
        {
            for (int i2 = 0; i2 < Y; i2++)
            {

                if (i + i2 > 16)
                    break;

                if (m_WhiteCircles[i + i2, i2] == true)
                {
                    count++;
                }
                else
                {
                    count = 0;
                }
                if (count == 5)
                {
                    WhiteWin();
                }

            }
        }





        count = 0;


        // detect diagonal

        for (int i2 = 0; i2 < Y; i2++)
        {
            for (int i = 0; i < X; i++)
            {

                if (i + i2 > 16)
                    break;

                if (m_WhiteCircles[i, i + i2] == true)
                {
                    count++;
                }
                else
                {
                    count = 0;
                }
                if (count == 5)
                {
                    WhiteWin();
                }

            }
        }

        count = 0;


        // detect diagonal

        for (int i2 = 16; i2 >= 0; i2--)
        {
            for (int i = 0; i < X; i++)
            {

                if (i2 - i < 0)
                    break;

                if (m_WhiteCircles[i, i2 - i] == true)
                {
                    count++;
                }
                else
                {
                    count = 0;
                }
                if (count == 5)
                {
                    WhiteWin();
                }
            }
        }



        count = 0;


        // detect diagonal

        for (int i = 0; i < X; i++)
        {
            for (int i2 = 0; i2 < Y; i2++)
            {
                if (i + i2 > 16)
                    break;

                if (m_WhiteCircles[i + i2, 16 - i2] == true)
                {
                    count++;
                }
                else
                {
                    count = 0;
                }
                if (count == 5)
                {
                    WhiteWin();
                }
            }
        }


    }

    void BlackWin()
    {

        m_WinState = WinState.Black;

        m_NetworkController.gameState = GameState.GameOverState;
        m_GameEndedImage.ShowImage(m_WinState);


    }

    void WhiteWin()
    {

        m_WinState = WinState.White;

        m_NetworkController.gameState = GameState.GameOverState;
        m_GameEndedImage.ShowImage(m_WinState);


    }




    /// <returns>return true if there is the same Circle position on opok_panel.
    /// compare to parameter (Vector3 pos) </returns>

    public bool HasCircleAtPos(Vector3 pos)
    {
        int x = 0, y = 0;

        switch (pos.x)
        {
            case 156:
                x = 0;
                break;
            case 203:
                x = 1;
                break;
            case 250:
                x = 2;
                break;
            case 298:
                x = 3;
                break;
            case 345:
                x = 4;
                break;
            case 392:
                x = 5;
                break;
            case 439:
                x = 6;
                break;
            case 486:
                x = 7;
                break;
            case 533:
                x = 8;
                break;
            case 580:
                x = 9;
                break;
            case 628:
                x = 10;
                break;
            case 675:
                x = 11;
                break;
            case 722:
                x = 12;
                break;
            case 769:
                x = 13;
                break;
            case 816:
                x = 14;
                break;
            case 863:
                x = 15;
                break;
            case 910:
                x = 16;
                break;
            default: return false;
        }

        switch (pos.y)
        {
            case 739:
                y = 0;
                break;
            case 694:
                y = 1;
                break;
            case 650:
                y = 2;
                break;
            case 605:
                y = 3;
                break;
            case 560:
                y = 4;
                break;
            case 515:
                y = 5;
                break;
            case 470:
                y = 6;
                break;
            case 426:
                y = 7;
                break;
            case 381:
                y = 8;
                break;
            case 335:
                y = 9;
                break;
            case 290:
                y = 10;
                break;
            case 246:
                y = 11;
                break;
            case 200:
                y = 12;
                break;
            case 156:
                y = 13;
                break;
            case 111:
                y = 14;
                break;
            case 66:
                y = 15;
                break;
            case 21:
                y = 16;
                break;
            default: return false;
        }


        if (m_ExistedCircles[x, y] == true)
            return true;
        else
            return false;

      
    }


}
