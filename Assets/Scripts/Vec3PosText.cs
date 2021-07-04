using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vec3PosText : MonoBehaviour
{
    // this class is useful for checking vector3 position 
    // of black and white shape 

    NetworkController m_NetworkController;
    CircleGenerator m_CircleGenerator;
    Text m_Text;
    bool m_BlackTurn, m_WhiteTurn;
    CircleType m_CircleType;
    Vector3 m_pos = new Vector3(0, 0, 0);
    string m_CircleName;
    

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_NetworkController.gameState==GameState.StartState)
        {
            m_Text.enabled = true;
            m_BlackTurn = m_CircleGenerator.BlackTurn;
            m_WhiteTurn = m_CircleGenerator.WhiteTurn;
            m_CircleType = (CircleType)m_CircleGenerator._CircleType;



            if (m_NetworkController.IsServerMode) 
            {

                DebugForServer();

            }
            if (m_NetworkController.IsClientMode)
            {

                DebugForClient();

            }

            ResetText();
        }
        else
        {
            m_Text.enabled = false;
        }
    }

    void Initialize()
    {
        m_NetworkController = GameObject.Find("NetworkController").GetComponent<NetworkController>();
        m_CircleGenerator = GameObject.Find("CircleGenerator").GetComponent<CircleGenerator>();
        m_Text = GetComponent<Text>();
    }



    void DebugForServer()
    {
        switch (m_CircleType)
        {

            case CircleType.Black:

                if (m_BlackTurn)
                {
                    m_pos = m_CircleGenerator.OpponentCirPos;
                    m_CircleName = "White";

                }
                if (m_WhiteTurn)
                {
                    m_pos = m_CircleGenerator.MyCirPos;
                    m_CircleName = "Black";

                }

                break;

            case CircleType.White:

                if (m_BlackTurn)
                {
                    m_pos = m_CircleGenerator.MyCirPos;
                    m_CircleName = "White";

                }
                if (m_WhiteTurn)
                {
                    m_pos = m_CircleGenerator.OpponentCirPos;
                    m_CircleName = "Black";

                }

                break;

        }


    }



    void DebugForClient()
    {


        switch (m_CircleType)
        {

            case CircleType.Black:

                if (m_BlackTurn)
                {
                    m_pos = m_CircleGenerator.OpponentCirPos;
                    m_CircleName = "White";

                }
                if (m_WhiteTurn)
                {
                    m_pos = m_CircleGenerator.MyCirPos;
                    m_CircleName = "Black";
                }

                break;

            case CircleType.White:

                if (m_BlackTurn)
                {
                    m_pos = m_CircleGenerator.MyCirPos;
                    m_CircleName = "White";
                }
                if (m_WhiteTurn)
                {
                    m_pos = m_CircleGenerator.OpponentCirPos;
                    m_CircleName = "Black";
                }

                break;

        }


    }

    void ResetText()
    {
        float x = m_pos.x;
        float y = m_pos.y;
        float z = m_pos.z;


        // return if it fails to get x and y position

        if (x == 0 || y == 0)
        {
            return;
        }


        // show text
        m_Text.text = "Debug \n " +
              "(" + m_CircleName + ")" + "\n " +
              "x: " + x + "\n" +
              "y: " + y + "\n" +
              "z: " + z + "\n";
    }




}
