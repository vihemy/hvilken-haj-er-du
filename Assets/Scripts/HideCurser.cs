using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideCurser : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = false;
    }
}
