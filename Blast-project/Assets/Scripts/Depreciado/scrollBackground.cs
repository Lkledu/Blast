using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollBackground : MonoBehaviour
{
    public Vector2 Scroll = new Vector2(0.05f, 0.05f);
    Vector2 Offset = new Vector2(0f, 0f);

    void Update()
    {
        Offset += Scroll * Time.deltaTime;
        //renderer.material.SetTextureOffset("_MainTex", Offset);
    }
}
