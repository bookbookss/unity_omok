using UnityEngine;

public class MouseDetector : MonoBehaviour
{
    public Vector3 Current_Position

    { 
        get 
        { 
            return Input.mousePosition;
        } 

    }

    const int Min_X = 152, Max_X = 914;


    const int Min_Y = 17, Max_Y = 743;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// return true if left mouse button clicked .
    /// otherwise it returns false
    /// </summary>
    /// <returns>Input.mousePosition</returns>

    public bool IsMouseClicked()
    {
        if(Input.GetMouseButtonDown(0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// return true if seletected mouse pos is not out of range from omok_panel .
    /// otherwise it returns false
    ///
    /// </summary>
    /// <returns></returns>
    public bool IsInRange()
    {

         if (Current_Position.x >= Min_X && Current_Position.x <= Max_X)
         {
             if (Current_Position.y >= Min_Y && Current_Position.y <= Max_Y)
             {
                 return true;
             }
             else
             {
                 return false;
             }
         }
         else
         {
             return false;
         }


    }
}

