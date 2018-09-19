using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;

using Aptoide.AppcoinsUnity;

//Inherit from the AppcoinsPurchaser Class
public class Purchaser : AppcoinsPurchaser {

	public Text message;
	public List<AppcoinsSKU> skus;

	void Start()
	{
		message.text = "Welcome to cody snacks shop!";
	}

	public override void PurchaseSuccess (AppcoinsSKU sku)
	{
		string skuid = sku.GetSKUId();
		base.PurchaseSuccess (sku);
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

	public override void PurchaseFailure (AppcoinsSKU sku)
	{
		string skuid = sku.GetSKUId();
		base.PurchaseFailure (sku);
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
		skus = new List<AppcoinsSKU>();
		skus.Add(new AppcoinsSKU("Chocolate", "chocolate", 0.1));
		skus.Add(new AppcoinsSKU("Monster Drink", "monster", 0.1));
		skus.Add(new AppcoinsSKU("Dodo", "dodo", 0.1));

		foreach (AppcoinsSKU sku in skus)
		{
			AddSKU(sku);
		}
	}


	//methods starts the purchase flow when you click their respective buttons to purchase snacks
	public void buyDodo(){
		MakePurchase(skus.Find(sku => sku.GetSKUId().Equals("dodo")));
	}

	public void buyMonster(){
		MakePurchase(skus.Find(sku => sku.GetSKUId().Equals("monster")));
	}

	public void buyChocolate(){
		MakePurchase(skus.Find(sku => sku.GetSKUId().Equals("chocolate")));
	}
}
