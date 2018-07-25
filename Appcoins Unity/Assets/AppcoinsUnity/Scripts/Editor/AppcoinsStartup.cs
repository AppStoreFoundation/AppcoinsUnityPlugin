using UnityEngine;
using UnityEditor;
using System;
using System.IO;

[InitializeOnLoad]
public class Startup
{
    private static string appcoinsMainTemplate = UnityEngine.Application.dataPath + "/AppcoinsUnity/Plugins/Android/mainTemplate.gradle";
    private static string currentMainTemplate = UnityEngine.Application.dataPath + "/Plugins/Android/mainTemplate.gradle";
    private static string oldMainTemplate =  UnityEngine.Application.dataPath + "/Plugins/Android/oldMainTemplate.gradle";

    public const string DEFAULT_UNITY_PACKAGE_IDENTIFIER = "com.Company.ProductName";

    static Startup()
    {
        //Check if the active platform is Android. If it isn't change it
        if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
            #if UNITY_5_6_OR_NEWER
                EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);

            #else
                EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);

            #endif

        //Check if min sdk version is lower than 21. If it is, set it to 21
        if (PlayerSettings.Android.minSdkVersion < AndroidSdkVersions.AndroidApiLevel21)
            PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel21;

        //Check if the bunde id is the default one and change it if it to avoid that error        
        #if UNITY_5_6_OR_NEWER
            if (PlayerSettings.applicationIdentifier.Equals(DEFAULT_UNITY_PACKAGE_IDENTIFIER))
                PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, "com.aptoide.appcoins");

        #else
            if (PlayerSettings.bundleIdentifier.Equals(DEFAULT_UNITY_PACKAGE_IDENTIFIER))
                PlayerSettings.bundleIdentifier = "com.aptoide.appcoins";
        
        #endif

        //Make sure that gradle is the selected build system
        if (EditorUserBuildSettings.androidBuildSystem != AndroidBuildSystem.Gradle)
            EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;      


        CheckMainTemplateGradle();
        Debug.Log("Successfully integrated Appcoins Unity plugin!");
    }

    private static void CheckMainTemplateGradle()
    {
        if(File.Exists(currentMainTemplate))
        {
            File.Copy(currentMainTemplate, oldMainTemplate, true);

            Tree<string> tCurrent = Tree<string>.CreateTreeFromFile(currentMainTemplate, FileParser.BUILD_GRADLE);
            Tree<string> tAppcoins = Tree<string>.CreateTreeFromFile(appcoinsMainTemplate, FileParser.BUILD_GRADLE);

            tCurrent.MergeTrees(tAppcoins);
            Tree<string>.CreateFileFromTree(tCurrent, UnityEngine.Application.dataPath + "/Plugins/Android/mainTemplate.gradle" , false, FileParser.BUILD_GRADLE);
        }

        else
        {
            File.Copy(appcoinsMainTemplate, currentMainTemplate, true);
        }
    }
}