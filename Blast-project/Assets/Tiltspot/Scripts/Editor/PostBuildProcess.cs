using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;
using UnityEditor.Build;

namespace Phonion.Tiltspot.Editor
{
    public class BuildHandler
    {
        [PostProcessBuildAttribute(1)]
        public static void OnPostProcessBuild(BuildTarget target, string pathToBuiltProject)
        {
            File.Move(pathToBuiltProject + "/Build/" + Path.GetFileName(pathToBuiltProject) + ".json", pathToBuiltProject + "/Build/build.json");
        }
    }
}