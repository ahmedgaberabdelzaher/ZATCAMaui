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

if [ -z "$Target_Environment" ]
then
    echo "You need define the environment variable in App Center"
    exit
fi
echo "Getting file path"

APP_CONSTANT_FILE=$APPCENTER_SOURCE_DIRECTORY/GAZT/GAZT/AppConfigurations/PageSettings.cs

echo "Updating file path to $APP_CONSTANT_FILE"

if [ -e "$APP_CONSTANT_FILE" ]
echo "Enter PageSettings"
then
    echo "Updating Target_Environment to $Target_Environment in PageSettings.cs"
    sed -i '' 's#Target_Environment = "[-A-Za-z0-9:_./]*"#Target_Environment = "'$Target_Environment'"#' $APP_CONSTANT_FILE

    echo "File content:"
    cat $APP_CONSTANT_FILE
fi
