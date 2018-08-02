using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEditor;
using Aptoide.AppcoinsUnity;

public class ProductStabilityTest {

    AssetImporter GetAssetAtPath(string productPath) {
        AssetImporter asset = AssetImporter.GetAtPath(productPath);
        return asset;
    }

    AppcoinsSku GetProductAtPath(string productPath)
    {
        AssetImporter asset = GetAssetAtPath(productPath);
        AppcoinsSku product = AssetDatabase.LoadAssetAtPath(asset.assetPath, typeof(AppcoinsSku)) as AppcoinsSku;
        return product;
    }

    private void TestProductExists(string productPath) {
        AssetImporter asset = GetAssetAtPath(productPath);
        Assert.That(asset != null);
    }

    private void TestProductHasAppcoinsSKU(string productPath) {
        AppcoinsSku product = GetProductAtPath(productPath);
        Assert.That(product != null);   
    }

    private void TestProductCorruption(string productPath)
    {
        AppcoinsSku product = GetProductAtPath(productPath);
        Assert.That(product.Name != "");
        Assert.That(product.SKUID != "");
        //Assert.That(product.Price > 0f);
    }

    [Test]
    public void TestTestProductsExist()
    {
        //Try to get the AppcoinsUnity prefab
        string productPath = "Assets/AppcoinsUnity/Products/Chocolate.asset";
        TestProductExists(productPath);

        productPath = "Assets/AppcoinsUnity/Products/Dodo.asset";
        TestProductExists(productPath);

        productPath = "Assets/AppcoinsUnity/Products/Monster Drink.asset";
        TestProductExists(productPath);
    }

    [Test]
    public void TestTestProductsHaveAppcoinsSKU()
    {
        //Try to get the AppcoinsUnity prefab
        string productPath = "Assets/AppcoinsUnity/Products/Chocolate.asset";
        TestProductHasAppcoinsSKU(productPath);

        productPath = "Assets/AppcoinsUnity/Products/Dodo.asset";
        TestProductHasAppcoinsSKU(productPath);

        productPath = "Assets/AppcoinsUnity/Products/Monster Drink.asset";
        TestProductHasAppcoinsSKU(productPath);
    }

    [Test]
    public void TestTestProductsArentCorrupted()
    {
        //Try to get the AppcoinsUnity prefab
        string productPath = "Assets/AppcoinsUnity/Products/Chocolate.asset";
        TestProductCorruption(productPath);

        productPath = "Assets/AppcoinsUnity/Products/Dodo.asset";
        TestProductCorruption(productPath);

        productPath = "Assets/AppcoinsUnity/Products/Monster Drink.asset";
        TestProductCorruption(productPath);
    }

    [Test]
    public void TestAddNewProduct() {
        bool result = EditorApplication.ExecuteMenuItem("Assets/Create/AppCoins Product");
        Assert.That(result == true);

        //Remove the created test product
        AssetDatabase.DeleteAsset("Assets/AppcoinsUnity/Products/AppCoinsProduct_Appcoins.asset");
    }

    [Test]
    public void TestAddNewProductToList()
    {
        EditorApplication.ExecuteMenuItem("Assets/Create/AppCoins Product");
        string productPath = "Assets/AppcoinsUnity/Products/AppCoinsProduct_Appcoins.asset";
        AppcoinsSku product = GetProductAtPath(productPath);

        //product.

        //Remove the created test product
        AssetDatabase.DeleteAsset("Assets/AppcoinsUnity/Products/AppCoinsProduct_Appcoins.asset");
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
