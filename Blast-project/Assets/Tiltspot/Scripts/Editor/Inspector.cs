using UnityEditor;
using UnityEngine;
using System.Collections;
using System;


namespace Phonion.Tiltspot.Editor
{
    
    [CustomEditor(typeof(Tiltspot))]
    public class Inspector : UnityEditor.Editor
    {

        GUIStyle headerStyle = new GUIStyle();
        GUIStyle paddingTop = new GUIStyle();
        Texture logo;
        
        public void OnEnable()
        {
            logo = (Texture)Resources.Load("Tiltspot_header");
            headerStyle.normal.background = MakeTex(600, 1, new Color(0.14f, 0.14f, 0.24f, 1f)); ;
            headerStyle.margin.top = 5;
            headerStyle.margin.bottom = 5;

            paddingTop.margin.top = 15;
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginHorizontal(headerStyle, GUILayout.Height(30));
            GUILayout.Label(logo, GUILayout.Width(128), GUILayout.Height(30));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginDisabledGroup(Application.isPlaying);
            Tiltspot._.useTiltspotGameTester = EditorGUILayout.Toggle("Use Tiltspot Game Tester", Tiltspot._.useTiltspotGameTester);
            EditorGUI.BeginChangeCheck();
            Tiltspot._.tiltspotGameTesterPort = EditorGUILayout.IntField("Port", Tiltspot._.tiltspotGameTesterPort);
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }

        private Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];

            for (int i = 0; i < pix.Length; i++)
                pix[i] = col;

            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();

            return result;
        }

    }
}