using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEditor;
using Aptoide.AppcoinsUnity;

public class ProductStabilityTest {

    [Test]
    public void TestTestProductsExist()
    {
        //Try to get the AppcoinsUnity prefab
        string productPath = "Assets/AppcoinsUnity/Products/Chocolate.asset";
        AssetImporter asset = AssetImporter.GetAtPath(productPath);
        Assert.That(asset != null);

        productPath = "Assets/AppcoinsUnity/Products/Dodo.asset";
        asset = AssetImporter.GetAtPath(productPath);
        Assert.That(asset != null);

        productPath = "Assets/AppcoinsUnity/Products/Monster Drink.asset";
        asset = AssetImporter.GetAtPath(productPath);
        Assert.That(asset != null);
    }

    [Test]
    public void TestTestProductsHaveAppcoinsSKU()
    {
        //Try to get the AppcoinsUnity prefab
        string productPath = "Assets/AppcoinsUnity/Products/Chocolate.asset";
        AssetImporter asset = AssetImporter.GetAtPath(productPath);
        AppcoinsSku product = AssetDatabase.LoadAssetAtPath(asset.assetPath, typeof(AppcoinsSku)) as AppcoinsSku;

        Assert.That(product != null);

        productPath = "Assets/AppcoinsUnity/Products/Dodo.asset";
        asset = AssetImporter.GetAtPath(productPath);
        product = AssetDatabase.LoadAssetAtPath(asset.assetPath, typeof(AppcoinsSku)) as AppcoinsSku;

        Assert.That(product != null);

        productPath = "Assets/AppcoinsUnity/Products/Monster Drink.asset";
        asset = AssetImporter.GetAtPath(productPath);
        product = AssetDatabase.LoadAssetAtPath(asset.assetPath, typeof(AppcoinsSku)) as AppcoinsSku;

        Assert.That(product != null);
    }
    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    //[UnityTest]
    //public IEnumerator NewTestScriptWithEnumeratorPasses() {
    //    // Use the Assert class to test conditions.
    //    // yield to skip a frame
    //    yield return null;
    //}
}
