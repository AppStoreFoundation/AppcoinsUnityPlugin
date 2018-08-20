#!/bin/sh
osascript -e 'activate application "/Applications/Utilities/Terminal.app"'
cd '/Users/aptoide/Desktop/builds/Appcoins Unity/Appcoins Unity'
if [ "$('/Users/aptoide/Library/Android/sdk/platform-tools/adb' get-state)" == "device" ]
then
'/Users/aptoide/Library/Android/sdk/platform-tools/adb' shell am start -n 'com.aptoide.appcoins/com.aptoide.appcoins.UnityPlayerActivity' 2>&1 2>'/Users/aptoide/Documents/GitHub/AppcoinsUnityPlugin/Appcoins Unity/Assets/AppcoinsUnity/Tools/ProcessError.out'
else
echo error: no usb device found > '/Users/aptoide/Documents/GitHub/AppcoinsUnityPlugin/Appcoins Unity/Assets/AppcoinsUnity/Tools/ProcessError.out'
fi
echo 'done' > '/Users/aptoide/Documents/GitHub/AppcoinsUnityPlugin/Appcoins Unity/Assets/AppcoinsUnity/Tools/ProcessCompleted.out'
exit
