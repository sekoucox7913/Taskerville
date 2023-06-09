using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SA.Foundation.Editor;
using SA.Android.Manifest;
using StansAssets.Plugins.Editor;

namespace SA.Android.Editor
{
    [Serializable]
    class AN_ManifestTab : IMGUILayoutElement
    {
        [SerializeField]
        IMGUIHyperToolbar m_menuToolbar;
        [SerializeField]
        List<IMGUILayoutElement> m_tabsLayout = new List<IMGUILayoutElement>();

        public override void OnAwake()
        {
            m_tabsLayout = new List<IMGUILayoutElement>();
            m_menuToolbar = new IMGUIHyperToolbar();

            AddMenuItem("MANIFEST", CreateInstance<AMM_ManifestTab>());
            AddMenuItem("APPLICATION", CreateInstance<AMM_ApplicationTab>());
            AddMenuItem("PRMISSIONS", CreateInstance<AMM_PermissionsTab>());
        }

        public override void OnLayoutEnable()
        {
            foreach (var tab in m_tabsLayout) tab.OnLayoutEnable();
        }

        void AddMenuItem(string itemName, IMGUILayoutElement layout)
        {
            var button = new IMGUIHyperLabel(new GUIContent(itemName), EditorStyles.boldLabel);
            button.SetMouseOverColor(SettingsWindowStyles.SelectedElementColor);
            m_menuToolbar.AddButtons(button);

            m_tabsLayout.Add(layout);
        }

        public override void OnGUI()
        {
            GUILayout.Space(2);
            var index = m_menuToolbar.Draw();
            GUILayout.Space(4);
            EditorGUILayout.BeginVertical(SettingsWindowStyles.SeparationStyle);
            GUILayout.Space(5);
            EditorGUILayout.EndVertical();

            var manifest = AMM_Manager.GetManifest();
            if (manifest == null)
            {
                EditorGUILayout.HelpBox("The selected build platform DOESN'T support AndroidManifest.xml file", MessageType.Info);
                return;
            }

            m_tabsLayout[index].OnGUI();

            EditorGUILayout.Space();
            if (GUILayout.Button("Save Manifest", GUILayout.Height(22.0f))) AMM_Manager.SaveManifest();
        }
    }
}
