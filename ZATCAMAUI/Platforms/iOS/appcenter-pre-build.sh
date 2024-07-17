#!/usr/bin/env bash
#
# For Xamarin, change some constants located in some class of the app.
# In this sample, suppose we have an AppConstant.cs class in shared folder with follow content:
#
# namespace Core
# {
#     public class AppConstant
#     {
#         public const string ApiUrl = "https://CMS_MyApp-Eur01.com/api";
#     }
# }
# 
# Suppose in our project exists two branches: master and develop. 
# We can release app for production API in master branch and app for test API in develop branch. 
# We just need configure this behaviour with environment variable in each branch :)
# 
# The same thing can be perform with any class of the app.
#
# AN IMPORTANT THING: FOR THIS SAMPLE YOU NEED DECLARE API_URL ENVIRONMENT VARIABLE IN APP CENTER BUILD CONFIGURATION.

echo "EXECUTING APPCENTER_PRE_BUILD SCRIPT"

# Updating  Info.plist ref URL : "https://montemagno.com/vs-app-center-custom-build-scripts-for-production-apps/"

PLIST_PATH=$APPCENTER_SOURCE_DIRECTORY/ZATCAMAUI/ZATCAMAUI/Platforms/iOS/Info.plist

# Print out file before any change
cat $PLIST_PATH

/usr/libexec/PlistBuddy -c "Set :CFBundleShortVersionString 5.0.${APPCENTER_BUILD_ID}" $PLIST_PATH

# Print out file for reference
cat $PLIST_PATH

echo "Updated info.plist!"

if [ -z "$Target_Environment" ]
then
    echo "You need define the environment variable in App Center"
    exit
fi
echo "Getting file path"

APP_CONSTANT_FILE=$APPCENTER_SOURCE_DIRECTORY/ZATCAMAUI/ZATCAMAUI/Core/AppConfigurations/PageSettings.cs

echo "Updating file path to $APP_CONSTANT_FILE"

if [ -e "$APP_CONSTANT_FILE" ]
echo "Enter PageSettings"
then
    echo "Updating Target_Environment to $Target_Environment in PageSettings.cs"
    sed -i '' 's#Target_Environment = "[-A-Za-z0-9:_./]*"#Target_Environment = "'$Target_Environment'"#' $APP_CONSTANT_FILE

    echo "File content:"
    cat $APP_CONSTANT_FILE
fi
