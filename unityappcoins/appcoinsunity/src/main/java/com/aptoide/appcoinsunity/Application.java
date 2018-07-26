package com.aptoide.appcoinsunity;

/**
 * Created by codeberg on 3/29/2018.
 * Modified by Aptoide
 */

import android.app.Activity;
import android.util.Log;

import com.asf.appcoins.sdk.ads.AppCoinsAds;
import com.asf.appcoins.sdk.ads.AppCoinsAdsBuilder;
import com.asf.appcoins.sdk.iab.AppCoinsIab;
import com.asf.appcoins.sdk.iab.AppCoinsIabBuilder;
import com.asf.appcoins.sdk.iab.entity.SKU;

import java.math.BigDecimal;
import java.util.LinkedList;
import java.util.List;

public class Application extends android.app.Application {

    public static AppCoinsIab appCoinsSdk;
    private static AppCoinsAds adsSdk;
    static List<SKU> skus = new LinkedList<>();
    static boolean debugFlag=false;
    static boolean POAFlag=false;
    static boolean IABFlag=false;
    static android.app.Application application;

    @Override
    public void onCreate() {
        super.onCreate();
        application=this;

        //Needs to happen before anything else
        //This will fetch the debug flag value that all other calls depend on
        setupStoreEnvironment();

        setupAdsSDK();
        Log.d("AppcoinsUnityPlugin", "Aplication began.");
    }

    public void setupStoreEnvironment() {
        final boolean debugValue = getResources().getBoolean(R.bool.APPCOINS_ENABLE_DEBUG);
        Log.d("AppcoinsUnityPlugin", "Debug should be " + debugValue);

        debugFlag = debugValue;
    }

    public void setupAdsSDK() {
        final boolean poaValue = getResources().getBoolean(R.bool.APPCOINS_ENABLE_POA);
        Log.d("AppcoinsUnityPlugin", "POA should be " + poaValue);
        if(poaValue == true) {

            Log.d("AppcoinsUnityPlugin", "POA sdk initialized");
            adsSdk = new AppCoinsAdsBuilder().withDebug(debugFlag)
                    .createAdvertisementSdk(application);
            adsSdk.init(application);
        }
    }

    public static void setupSDK(String developerAddress){
        Log.d("AppcoinsUnityPlugin", "Set up SDK began.");

        if(IABFlag) {
            Log.d("AppcoinsUnityPlugin", "IAB sdk initialized");
            appCoinsSdk = new AppCoinsIabBuilder(developerAddress).withSkus(buildSkus())
                    .withDebug(debugFlag)
                    .createAppCoinsIab();
        }
    }

    private static List<SKU> buildSkus() {
        return skus;
    }

    public static void addSku(String name, String id, double price){
        skus.add(new SKU(name,id, BigDecimal.valueOf(price)));
    }

    public static void setIAB(boolean iabflag){
        IABFlag=iabflag;
    }

    public static boolean getIABFlag(){
       return IABFlag;
    }

}
