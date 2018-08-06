//created by Lukmon Agboola(Codeberg)
//Modified by Aptoide
//Note: do not change anything here as it may break the workings of the plugin else you're very sure of what you're doing.
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace Aptoide.AppcoinsUnity
{

    public class AppcoinsUnityTests : MonoBehaviour
    {
        AppcoinsUnity appcoinsUnity;

        // Use this for initialization
        void Start()
        {
            if (Application.isEditor) {
                appcoinsUnity = GetComponent<AppcoinsUnity>();

                appcoinsUnity.onStartPurchase.AddListener(makePurchase);

                AppCoinsChecks.CheckPoAActive(appcoinsUnity.enablePOA);

                AppCoinsChecks.CheckSKUsInEditorMode(appcoinsUnity.products);
                AppCoinsChecks.CheckForRepeatedSkuId(appcoinsUnity.products);
            } 
        }

        //method used in making purchase
        public void makePurchase(string skuid)
        {
            if (!appcoinsUnity.enableIAB)
            {
                Debug.LogWarning("Tried to make a purchase but enableIAB is false! Please set it to true on AppcoinsUnity object before using this functionality");
                return;
            }

            if (Application.isEditor) {
                AppCoinsChecks.TestPurchase(purchaseSuccess, purchaseFailure,skuid);    
            } 
        }

        //callback on successful purchases
        public void purchaseSuccess(string skuid)
        {
            appcoinsUnity.purchaseSuccess(skuid);
#if UNITY_EDITOR
            EditorUtility.DisplayDialog("AppCoins Unity Integration", "Purchase Success!", "OK");
#endif
        }

        //callback on failed purchases
        public void purchaseFailure(string skuid)
        {
            appcoinsUnity.purchaseFailure(skuid);
#if UNITY_EDITOR
            EditorUtility.DisplayDialog("AppCoins Unity Integration", "Purchase Failed!", "OK");
#endif
        }

    }
} //namespace Aptoide.AppcoinsUnity
