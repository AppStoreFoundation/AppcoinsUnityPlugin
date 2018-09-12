using UnityEngine;
using UnityEngine.UI;

using Aptoide.AppcoinsUnity;

//Inherit from the AppcoinsPurchaser Class
public class Purchaser : AppcoinsPurchaser {

	public Text message;

	void Start()
	{
		message.text = "Welcome to cody snacks shop!";
	}

	public override void PurchaseSuccess (string skuid)
	{
		base.PurchaseSuccess (skuid);
		//purchase is successful release the product

		if(skuid.Equals("dodo"))
		{
		message.text="Thanks! You bought dodo";
		}

		else if(skuid.Equals("monster"))
		{
		message.text="Thanks! You bought monster drink";
		}

		else if(skuid.Equals("chocolate"))
		{
			message.text="Thanks! You bought chocolate";
		}
	}

	public override void PurchaseFailure (string skuid)
	{
		base.PurchaseFailure (skuid);
		//purchase failed perhaps show some error message

		if(skuid.Equals("dodo"))
		{
			message.text="Sorry! Purchase failed for dodo";
		}

		else if(skuid.Equals("monster"))
		{
			message.text="Sorry! Purchase failed for drink";
		}

		else if(skuid.Equals("chocolate"))
		{
			message.text="Sorry! Purchase failed for chocolate";
		}
	}

	public override void RegisterSKUs()
	{
		AddSKU(new AppcoinsSKU("Chocolate", "chocolate", 0.1));
		AddSKU(new AppcoinsSKU("Monster Drink", "monster", 0.1));
		AddSKU(new AppcoinsSKU("Dodo", "dodo", 0.1));
	}


	//methods starts the purchase flow when you click their respective buttons to purchase snacks
	public void buyDodo(){
		MakePurchase("dodo");
	}

	public void buyMonster(){
		MakePurchase("monster");
	}

	public void buyChocolate(){
		MakePurchase("chocolate");
	}
}
