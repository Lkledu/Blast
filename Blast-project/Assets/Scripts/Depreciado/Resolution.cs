using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Resolution : MonoBehaviour
{
    private void Update()
    {
        Screen.SetResolution(1320, 1280, true);

    }
}