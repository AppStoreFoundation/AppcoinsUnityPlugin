//created by Lukmon Agboola(Codeberg)
//Modified by Aptoide
//Inherit this class to create your own purchaser class, see the example scene for more info

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aptoide.AppcoinsUnity{

	public class AppcoinsPurchaser : MonoBehaviour {

		AppcoinsUnity appcoinsUnity;

        public void Init(AppcoinsUnity appcoinsUnityRef){
            //get refference to AppcoinsUnity class
            appcoinsUnity = appcoinsUnityRef;
		}

        public virtual void purchaseSuccess(string skuid)
        {
        }

        public virtual void purchaseFailure(string skuid)
        {
        }

		public void makePurchase(string skuid){
			appcoinsUnity.makePurchase (skuid);
		}

	}
} //namespace Aptoide.AppcoinsUnity
