//created by Lukmon Agboola(Codeberg)
//Modified by Aptoide
//Note: do not change anything here as it may break the workings of the plugin else you're very sure of what you're doing.
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aptoide.AppcoinsUnity
{

    public class AppcoinsUnity : MonoBehaviour
    {
        public static string POA = "POA";
        public static string DEBUG = "DEBUG";
        public static string APPCOINS_PREFAB = "APPCOINS_PREFAB";

        [Header("Your wallet address for receiving Appcoins")]
        public string receivingAddress;
        [Header("Uncheck to disable Appcoins IAB")]
        public bool enableIAB = true;
        [Header("Uncheck to disable Appcoins ADS(Proof of attention)")]
        public bool enablePOA = true;
        [Header("Enable debug to use testnets e.g Ropsten")]
        public bool enableDebug = true;
        [Header("Add all your products here")]
        public AppcoinsSku[] products;
        [Header("Add your purchaser object here")]
        public AppcoinsPurchaser purchaserObject;

        AndroidJavaClass _class;
        AndroidJavaObject instance { get { return _class.GetStatic<AndroidJavaObject>("instance"); } }

        private void Awake()
        {
            purchaserObject.Init(this);
        }

        // Use this for initialization
        void Start()
        {
#if UNITY_EDITOR
            if (enablePOA)
                EditorUtility.DisplayDialog("AppCoins Unity Integration", "PoA is enabled and should have started now", "OK");
#else

            //get refference to java class
            _class = new AndroidJavaClass("com.aptoide.appcoinsunity.UnityAppcoins");

            //setup wallet address
            _class.CallStatic("setAddress", receivingAddress);

            //Enable or disable In App Billing
            _class.CallStatic("enableIAB", enableIAB);

            //add all your skus here
            addAllSKUs();

            //start sdk
            _class.CallStatic("start");
#endif
        }

        // This function is called when this script is loaded or some variable changes its value.
        void OnValidate()
        {
            // Put new value of enablePOA in mainTemplate.gradle to enable it or disable it.
            updateVarOnMainTemplateGradle(POA, enablePOA.ToString());
            updateVarOnMainTemplateGradle(DEBUG, enableDebug.ToString());
        }

        //called to add all skus specified in the inpector window.
        private void addAllSKUs()
        {
#if UNITY_EDITOR
            bool failed = false;
#endif
            for (int i = 0; i < products.Length; i++)
            {
                AppcoinsSku product = products[i];
                if (product != null)
                    _class.CallStatic("addNewSku", product.Name, product.SKUID, product.Price);
#if UNITY_EDITOR
                else
                    failed = true;
#endif
            }

#if UNITY_EDITOR
                if (failed)
                    EditorUtility.DisplayDialog("AppCoins Unity Integration", "Warning: You have null products on AppCoinsUnity objects products list", "OK");
#endif

        }

        //method used in making purchase
        public void makePurchase(string skuid)
        {
            if (!enableIAB)
            {
                Debug.LogWarning("Tried to make a purchase but enableIAB is false! Please set it to true on AppcoinsUnity object before using this functionality");
                return;
            }

#if UNITY_EDITOR
            if (EditorUtility.DisplayDialog("AppCoins Unity Integration", "AppCoins IAB Successfully integrated", "Test success", "Test failure"))
            {
                purchaseSuccess(skuid);
            }
            else
            {
                purchaseFailure(skuid);
            }
#else
            _class.CallStatic("makePurchase",skuid);
#endif

        }

        //callback on successful purchases
        public void purchaseSuccess(string skuid)
        {
            if (purchaserObject != null)
            {
                Debug.Log("Going to call purchaseSuccess on purchaserObject skuid " + skuid);
                purchaserObject.purchaseSuccess(skuid);
            }
            else
            {
                Debug.Log("purchaserObject is null");
            }
        }

        //callback on failed purchases
        public void purchaseFailure(string skuid)
        {
            if (purchaserObject != null)
            {
                Debug.Log("Going to call purchaseFailure on purchaserObject skuid " + skuid);
                purchaserObject.purchaseFailure(skuid);
            }
            else
            {
                Debug.Log("purchaserObject is null");
            }
        }

        // Change the mainTemplate.gradle's ENABLE_POA var to its new value
        private void updateVarOnMainTemplateGradle(string varName, string varToCheck)
        {
            string pathToMainTemplate = Application.dataPath + "/Plugins/Android/mainTemplate.gradle"; // Path to mainTemplate.gradle
            string line;
            string contentToChange = null;
            string contentInTemplate = null;
            ArrayList linesToChange = new ArrayList();
            int counter = 0;
            int numberOfSpaces = 0;
            ArrayList fileLines = new ArrayList();

            //Line to change inside test container
            if(varName.Equals(POA))
            {
                contentToChange = "resValue \"bool\", \"APPCOINS_ENABLE_POA\", \"" + varToCheck.ToLower() + "\"";
                contentInTemplate = "resValue \"bool\", \"APPCOINS_ENABLE_POA\", \"" + ((varToCheck.ToLower()).Equals("true") ? "false" : "true") + "\"";
            }

            else if(varName.Equals(DEBUG))
            {
                contentToChange = "resValue \"bool\", \"APPCOINS_ENABLE_DEBUG\", \"" + varToCheck.ToLower() + "\"";
                contentInTemplate = "resValue \"bool\", \"APPCOINS_ENABLE_DEBUG\", \"" + ((varToCheck.ToLower()).Equals("true") ? "false" : "true") + "\"";
            }

            System.IO.StreamReader fileReader = new System.IO.StreamReader(pathToMainTemplate);

            //Read all lines and get the line numer to be changed
            while ((line = fileReader.ReadLine()) != null)
            {
                fileLines.Add(line);

                //Get the new line and number of spaces erased.
                ArrayList a = RemoveFirstsWhiteSpaces(line);
                line = (string)a[0];

                //Debug.Log(line);

                if (line.Length == contentInTemplate.Length && line.Substring(0, contentInTemplate.Length).Equals(contentInTemplate))
                {
                    linesToChange.Add(counter);
                    numberOfSpaces = (int)a[1];
                }

                counter++;
            }

            fileReader.Close();

            foreach(int lineToChange in linesToChange)
            {
                if (lineToChange > -1)
                {
                    string change = contentToChange;

                    for (int i = 0; i < numberOfSpaces; i++)
                    {
                        change = string.Concat(" ", change);
                    }

                    fileLines[lineToChange] = change;
                }
            }

            System.IO.StreamWriter fileWriter = new System.IO.StreamWriter(pathToMainTemplate);

            foreach (string newLine in fileLines)
            {
                fileWriter.WriteLine(newLine);
            }

            fileWriter.Close();
        }

        private static ArrayList RemoveFirstsWhiteSpaces(string line)
        {
            int lettersToRemove = 0;

            foreach (char letter in line)
            {
                if (char.IsWhiteSpace(letter))
                {
                    lettersToRemove++;
                }

                else
                {
                    break;
                }
            }

            if (lettersToRemove > 0)
            {
                line = line.Substring(lettersToRemove);
            }

            ArrayList a = new ArrayList();
            a.Add(line);
            a.Add(lettersToRemove);

            return a;
        }
    }
} //namespace Aptoide.AppcoinsUnity
