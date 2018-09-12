// using UnityEngine;
// using UnityEngine.TestTools;
// using NUnit.Framework;
// using System.Collections;
// using UnityEditor;
// using Aptoide.AppcoinsUnity;

// public class PrefabStabilityTest {

//     [Test]
//     public void TestPrefabExists() {
//         //Try to get the AppcoinsUnity prefab
//         string prefabPath = "Assets/AppcoinsUnity/Prefabs/AppcoinsUnity.prefab";
//         AssetImporter assetImporter = AssetImporter.GetAtPath(prefabPath);
//         Assert.That(assetImporter != null);
//     }

//     [Test]
//     public void TestPrefabHasAppCoinsUnity()
//     {
//         //Try to get the AppcoinsUnity prefab
//         string prefabPath = "Assets/AppcoinsUnity/Prefabs/AppcoinsUnity.prefab";
//         AssetImporter asset = AssetImporter.GetAtPath(prefabPath);
//         GameObject prefab = AssetDatabase.LoadAssetAtPath(asset.assetPath, typeof(GameObject)) as GameObject;
//         GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
//         AppcoinsUnity appcoinsUnity = instance.GetComponent<AppcoinsUnity>();

//         Assert.That(appcoinsUnity != null);
//     }

//     [Test]
//     public void TestPrefabHasDefaultWalletAddress()
//     {
//         //Try to get the AppcoinsUnity prefab
//         string prefabPath = "Assets/AppcoinsUnity/Prefabs/AppcoinsUnity.prefab";
//         AssetImporter asset = AssetImporter.GetAtPath(prefabPath);
//         GameObject prefab = AssetDatabase.LoadAssetAtPath(asset.assetPath, typeof(GameObject)) as GameObject;
//         GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
//         AppcoinsUnity appcoinsUnity = instance.GetComponent<AppcoinsUnity>();

//         Assert.That(appcoinsUnity.receivingAddress == "0xa43646ed0ece7595267ed7a2ff6f499f9f10f3c7");
//     }

//     // A UnityTest behaves like a coroutine in PlayMode
//     // and allows you to yield null to skip a frame in EditMode
//     //[UnityTest]
//     //public IEnumerator NewTestScriptWithEnumeratorPasses() {
//     //    // Use the Assert class to test conditions.
//     //    // yield to skip a frame
//     //    yield return null;
//     //}
// }
