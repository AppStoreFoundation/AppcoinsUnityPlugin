package com.aptoide.appcoinsunity;
import com.asf.appcoins.sdk.iab.payment.PaymentStatus;
import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.MainThread;
import android.util.Log;

import io.reactivex.android.schedulers.AndroidSchedulers;

import static com.aptoide.appcoinsunity.Application.appCoinsSdk;

/**
 * Created by Aptoide on 6/28/2018.
 */
public class PurchaseActivity extends Activity {

    public static String SKUID_TAG = "skuid";

    private static String TAG = "PurchaseActivity";

    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        // print debug message to logcat
        Log.d(TAG, "Activity began.");

        String skuid = getIntent().getStringExtra(SKUID_TAG);

        Log.d(TAG,"the activity is " + this);
        appCoinsSdk.buy(skuid, this).observeOn(AndroidSchedulers.mainThread()).subscribe(() -> {
            // In this case the buy process was triggered as expected.
            Log.d(TAG, "*********************************success**************************");
        }, throwable -> {
            // There was an error triggering the buy process.
            Log.d(TAG, "******************************fail****************************+");
            throwable.printStackTrace();
        });
    }

    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        String appcoinsPrefabName = getResources().getString(R.string.APPCOINS_PREFAB);

        Log.d(TAG, "onActivityResult called");
        Log.d(TAG, appcoinsPrefabName);

        if (Application.appCoinsSdk.onActivityResult(requestCode, requestCode, data)) {
            Application.appCoinsSdk.getCurrentPayment()
                    .subscribe(paymentDetails -> UnityPlayer.currentActivity.runOnUiThread(() -> {
                        if ((paymentDetails.getPaymentStatus() == PaymentStatus.PENDING) ||
                                (paymentDetails.getPaymentStatus() == PaymentStatus.SUCCESS)) {
                            String skuId = paymentDetails.getSkuId();
                            // Now we tell the sdk to consume the skuId.
                            Application.appCoinsSdk.consume(skuId);
                            // Purchase successfully done. Release the prize.

                            Log.d(TAG, "AppcoinsUnity::purchaseSuccess! skuid " + skuId);

                            UnityPlayer.UnitySendMessage(appcoinsPrefabName,"purchaseSuccess",skuId);
                        }
                        else{
                            String skuId = paymentDetails.getSkuId();

                            Log.d(TAG, "AppcoinsUnity::purchaseFailure! skuid " + skuId);

                            UnityPlayer.UnitySendMessage(appcoinsPrefabName,"purchaseFailure",skuId);
                        }
                        setResult(resultCode,data);
                        finish();
                    }));
        }
    }
}