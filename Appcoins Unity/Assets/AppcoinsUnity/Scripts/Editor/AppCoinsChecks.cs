using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Aptoide.AppcoinsUnity;

public static class AppCoinsChecks
{

    public static void CheckSKUsInEditorMode(AppcoinsSku[] products)
    {
#if UNITY_EDITOR
        if (products.Length == 0)
        {
            EditorUtility.DisplayDialog(
                "AppCoins Unity Integration",
                "Warning: You have no products on AppCoinsUnity prefab products list",
                "OK"
            );
        }

        else
        {
            for (int i = 0; i < products.Length; i++)
            {
                if (products[i] == null)
                {
                    EditorUtility.DisplayDialog(
                        "AppCoins Unity Integration",
                        "Warning: You have null products on AppCoinsUnity prefab products list",
                        "OK"
                    );
                }
            }
        }
#else
#endif
    }

    public static bool CheckForRepeatedSkuId(AppcoinsSku[] products)
    {
        for (int i = 0; i < products.Length - 1; i++)
        {
            AppcoinsSku currentProduct = products[i];

            for (int j = i + 1; j < products.Length; j++)
            {
                AppcoinsSku compareProduct = products[j];

                if (currentProduct != null && currentProduct.SKUID.Length == compareProduct.SKUID.Length)
                {
                    if (currentProduct.SKUID.Equals(compareProduct.SKUID))
                    {
#if UNITY_EDITOR
                        EditorUtility.DisplayDialog(
                            "AppCoins Custom Build Error",
                            "AppcoinsUnity Prefab products list: element number " + i +
                                    " and element number " + j + " have the same SKU ID",
                            "OK"
                        );
#endif
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public static void CheckPoAActive(bool enablePOA)
    {
#if UNITY_EDITOR
        if (enablePOA)
            EditorUtility.DisplayDialog(
                "AppCoins Unity Integration",
                "PoA is enabled and should have started now",
                "OK"
            );
#endif
    }

    public static void TestPurchase(System.Action<string> purchaseSuccess, System.Action<string> purchaseFailure, string skuid)
    {
#if UNITY_EDITOR
        if (EditorUtility.DisplayDialog("AppCoins Unity Integration", "AppCoins IAB Successfully integrated", "Test success", "Test failure"))
        {
            purchaseSuccess(skuid);
        }
        else
        {
            purchaseFailure(skuid);
        }
#endif
    }
}
