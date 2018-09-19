#!/bin/sh
osascript -e 'activate application "/Applications/Utilities/Terminal.app"'
cd '/Users/aptoide/Desktop/build/Appcoins Unity'
'/Applications/Android Studio.app/Contents/gradle/gradle-4.4/bin/gradle' assembleDebug 2>&1 2>'/Users/aptoide/Documents/GitHub/asf-unity-plugin/Appcoins Unity/Assets/AppcoinsUnity/Tools/ProcessError.out'
echo 'done' > '/Users/aptoide/Documents/GitHub/asf-unity-plugin/Appcoins Unity/Assets/AppcoinsUnity/Tools/ProcessCompleted.out'
exit
