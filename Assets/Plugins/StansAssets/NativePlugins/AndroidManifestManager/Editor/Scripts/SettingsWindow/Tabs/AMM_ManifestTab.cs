using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SA.Foundation.Editor;
using StansAssets.Plugins.Editor;

namespace SA.Android.Manifest
{
    public class AMM_ManifestTab : IMGUILayoutElement
    {
        public override void OnGUI()
        {
            
            
            var manifest = AMM_Manager.GetManifest();
            using (new IMGUIWindowBlockWithIndent(new GUIContent("Values")))
            {
                foreach (var key in manifest.Values.Keys)
                {
                    using (new IMGUIBeginHorizontal())
                    {
                        EditorGUILayout.LabelField(key);
                        if (key.Equals("xmlns:android") ||
                            key.Equals("android:installLocation") ||
                            key.Equals("package") ||
                            key.Equals("android:versionName") ||
                            key.Equals("android:versionCode") ||
                            key.Equals("android:theme"))
                        {
                            GUI.enabled = false;
                            EditorGUILayout.TextField(AMM_Manager.GetManifest().Values[key]);
                        }
                        else
                        {
                            GUI.enabled = true;

                            var input = AMM_Manager.GetManifest().Values[key];
                            EditorGUI.BeginChangeCheck();
                            input = GUILayout.TextField(AMM_Manager.GetManifest().Values[key]);
                            if (EditorGUI.EndChangeCheck())
                            {
                                AMM_Manager.GetManifest().SetValue(key, input);
                                return;
                            }

                            if (GUILayout.Button("X", GUILayout.Width(20.0f)))
                            {
                                AMM_Manager.GetManifest().RemoveValue(key);
                                return;
                            }
                        }
                    }

                    GUI.enabled = true;
                }

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Space();
                if (GUILayout.Button("Add Value", GUILayout.Width(100.0f))) AMM_SettingsWindow.AddValueDialog(AMM_Manager.GetManifest());
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
            }

            using (new IMGUIWindowBlockWithIndent(new GUIContent("Properties")))
            {
                
                AMM_SettingsWindow.DrawProperties(AMM_Manager.GetManifest());
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Space();
                if (GUILayout.Button("Add Property", GUILayout.Width(100.0f))) AMM_SettingsWindow.AddPropertyDialog(AMM_Manager.GetManifest());
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
            }

            EditorGUILayout.Space();
        }
    }
}
