using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonExit : ButtonUi
{
    public override void OnClick()
    {
        base.OnClick();
        Application.Quit();
    }
}
