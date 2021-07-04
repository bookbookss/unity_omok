using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndedImage : MonoBehaviour
{

    RawImage m_GameEndedImg;

    void Start()
    {
        m_GameEndedImg = GetComponent<RawImage>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowImage(WinState winState)
    {

        switch (winState)
        {

            case WinState.Black:
                m_GameEndedImg.texture = Resources.Load<Texture>("Materials/black_circle_win");
                m_GameEndedImg.enabled = true;
                break;

            case WinState.White:
                m_GameEndedImg.texture = Resources.Load<Texture>("Materials/white_circle_win");
                m_GameEndedImg.enabled = true;
                break;

            case WinState.Null:
                //do nothing
                break;

            default:
                //do nothing
                break;

        }
        
    }
}
