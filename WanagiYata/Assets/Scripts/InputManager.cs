using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
  
    public static Vector2 ControllerVector()
    {
        return new Vector2(ControllerHorizontalAxis(), ControllerVerticallAxis());
    }
    public static float ControllerHorizontalAxis()
    {
        float result = 0f;
        result += Input.GetAxis("KB_HORIZONTAL");
        result += Input.GetAxis("JP_HORIZONTAL");
        return result;

    }
    public static float ControllerVerticallAxis()
    {
        float result = 0f;
        result += Input.GetAxis("KB_VERTICAL");
        result += Input.GetAxis("JP_VERTICAL");
        return result;
    }
    public static bool AButton()
    {
        return Input.GetButtonDown("A_BUTTON");
    }
    public static bool BButton()
    {
        return Input.GetButtonDown("B_BUTTON");
    }
    public static bool BPressed()
    {
        return Input.GetButton("B_BUTTON");
    }
    public static bool XButton()
    {
        return Input.GetButtonDown("X_BUTTON");
    }
    public static bool YButton()
    {
        return Input.GetButtonDown("Y_BUTTON");
    }

}
