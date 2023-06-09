using System.Collections.Generic;
using UnityEngine;
using SA.Android.Manifest;
using SA.Foundation.Editor;
using SA.Foundation.Utility;
using StansAssets.Plugins.Editor;

namespace SA.Android.Editor
{
    abstract class AN_ServiceSettingsUI : SA_ServiceLayout
    {
        protected override IEnumerable<string> SupportedPlatforms => new List<string>() { "Android", "Android TV", "Android Wear" };

        protected override void DrawServiceRequirements()
        {
            var resolver = (AN_APIResolver)Resolver;

            if (resolver.BuildRequirements.IsEmpty) return;

            using (new SA_WindowBlockWithSpace(new GUIContent("Requirements")))
            {
                DrawRequirementsUI(resolver.BuildRequirements);
            }
        }

        public static void DrawRequirementsUI(AN_AndroidBuildRequirements buildRequirements)
        {
            if (buildRequirements.Activities.Count > 0)
                using (new SA_H2WindowBlockWithSpace(new GUIContent("ACTIVITIES")))
                {
                    foreach (var activity in buildRequirements.Activities)
                    {
                        var name = SA_PathUtil.GetExtension(activity.Name);
                        name = name.Substring(1, name.Length - 1);
                        var icon = AN_Skin.GetIcon("requirements_activity.png");
                        IMGUILayout.SelectableLabel(new GUIContent("activity: " + name, icon));
                    }
                }

            if (buildRequirements.ApplicationProperties.Count > 0)
                using (new SA_H2WindowBlockWithSpace(new GUIContent("APP PROPERTIES")))
                {
                    foreach (var property in buildRequirements.ApplicationProperties)
                    {
                        var icon = AN_Skin.GetIcon("requirements_activity.png");
                        var name = SA_PathUtil.GetExtension(property.Name);
                        name = name.Substring(1, name.Length - 1);

                        IMGUILayout.SelectableLabel(new GUIContent(property.Tag + ": " + name, icon));
                    }
                }

            if (buildRequirements.ManifestProperties.Count > 0)
                using (new SA_H2WindowBlockWithSpace(new GUIContent("MANIFEST PROPERTIES")))
                {
                    foreach (var property in buildRequirements.ManifestProperties)
                    {
                        var icon = AN_Skin.GetIcon("requirements_activity.png");

                        var info = string.Empty;
                        foreach (var pair in property.Values) info += " " + pair.Key + " : " + pair.Value;

                        IMGUILayout.SelectableLabel(new GUIContent(property.Tag + info, icon));
                    }
                }

            if (buildRequirements.Permissions.Count > 0)
                using (new SA_H2WindowBlockWithSpace(new GUIContent("PERMISSIONS")))
                {
                    foreach (var permission in buildRequirements.Permissions)
                    {
                        var icon = AN_Skin.GetIcon("requirements_permission.png");
                        IMGUILayout.SelectableLabel(new GUIContent(permission.GetFullName(), icon));
                    }
                }

            if (buildRequirements.BinaryDependencies.Count > 0)
                using (new SA_H2WindowBlockWithSpace(new GUIContent("BINARY DEPENDENCIES")))
                {
                    foreach (var dependency in buildRequirements.BinaryDependencies)
                    {
                        var icon = AN_Skin.GetIcon("requirements_lib.png");
                        IMGUILayout.SelectableLabel(new GUIContent(dependency.GetRemoteRepositoryName(), icon));
                    }
                }
        }
    }
}
