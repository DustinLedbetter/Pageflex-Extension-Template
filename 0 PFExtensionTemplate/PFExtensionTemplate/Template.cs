/***********************************************************************************************************************************
*                                                 GOD First                                                                        *
* Author: Programmer(s) name(s)                                                                                                    *
* Release Date: 00-00-0000                                                                                                         *
* Version: 1.0                                                                                                                     *
* Purpose: Description of what the extension is for                                                                                *
************************************************************************************************************************************/

/*
 References: There are three dlls referenced by this template:
    1. PageflexServices.dll
    2. StorefrontExtension.dll
    3. SXI.dll
    4. PFWEB.dll (Not used here, but might need in some instances with Pageflex [such as when have a webconfig file])
 */
using Pageflex.Interfaces.Storefront;
using PageflexServices;
using System;
using System.IO;


namespace SeventhExtension
{

    public class Template : SXIExtension
    {

        #region |--Fields--|
        // This section holds variables for code used throughout the program for quick refactoring as needed

        // These fields need to be created for the new storefront
        private const string _UNIQUENAME = @"ExtensionName.ExtensionPurpose.YourSite.com";
        private const string _DISPLAYNAME = @"Services: Extension Name";

        // This field needs to be changed to match the storefront being used! 
        private static readonly string LOG_FILENAME1 = "D:\\Pageflex\\Deployments\\";
        private static readonly string LOG_FILENAME2 = "\\Logs\\YourExtensionName_Extension_Log_File_";

        // This variable does not need to be changed
        private const string _YourExetensionAbbreviation_DEBUGGING_MODE = @" YourExetensionAbbreviationDebuggingMode";

        #endregion


        #region |--Properties--|
        // At a minimum your extension must override the DisplayName and UniqueName properties.


        // The UniqueName is used to associate a module with any data that it provides to Storefront.
        public override string UniqueName
        {
            get
            {
                return _UNIQUENAME;
            }
        }

        // The DisplayName will be shown on the Extensions and Site Options pages of the Administrator site as the name of your module.
        public override string DisplayName
        {
            get
            {
                return _DISPLAYNAME;
            }
        }

        // Gets the parameter to determine if in debug mode or not. Can also be used to get more variables at one as well
        protected override string[] PARAMS_WE_WANT
        {
            get
            {
                return new string[1]
                {
                  _YourExetensionAbbreviation_DEBUGGING_MODE
                };
            }
        }

        // Used to access the storefront to retrieve variables
        ISINI SF { get { return Storefront; } }

        // This Method is used to write all of our logs to a txt file
        public void LogMessageToFile(string msg)
        {
            // Get the Date and time stamps as desired
            string currentLogDate = DateTime.Now.ToString("MMddyyyy");
            string currentLogTimeInsertMain = DateTime.Now.ToString("HH:mm:ss tt");

            // Get the storefront's name from storefront to send logs to correct folder
            string sfName = SF.GetValue(FieldType.SystemProperty, SystemProperty.STOREFRONT_NAME, null);

            // Setup Message to display in .txt file 
            msg = string.Format("Time: {0:G}:  Message: {1}{2}", currentLogTimeInsertMain, msg, Environment.NewLine);

            // Add message to the file 
            File.AppendAllText(LOG_FILENAME1 + sfName + LOG_FILENAME2 + currentLogDate + ".txt", msg);
        }

        #endregion


        #region |--This section setups up the extension config page on the storefront to takes input for variables from the user at setup to be used in our extension--|

        // This section sets up on the extension page on the storefront a check box for users to turn on or off debug mode and text fields to get logon info for DB and Avalara
        public override int GetConfigurationHtml(KeyValuePair[] parameters, out string HTML_configString)
        {
            // Load and check if we already have a parameter set
            LoadModuleDataFromParams(parameters);

            // If not then we setup one 
            if (parameters == null)
            {
                SConfigHTMLBuilder sconfigHtmlBuilder = new SConfigHTMLBuilder();
                sconfigHtmlBuilder.AddHeader();

                // Add checkbox to let user turn on and off debug mode
                sconfigHtmlBuilder.AddServicesHeader("Debug Mode:", "");
                sconfigHtmlBuilder.AddCheckboxField("Debugging Information", _YourExetensionAbbreviation_DEBUGGING_MODE, "true", "false", (string)ModuleData[_YourExetensionAbbreviation_DEBUGGING_MODE] == "true");
                sconfigHtmlBuilder.AddTip(@"This box should be checked if you wish for debugging information to be output to the Logs.");

                // Footer info and set to configstring
                sconfigHtmlBuilder.AddServicesFooter();
                HTML_configString = sconfigHtmlBuilder.html;
            }
            // If we do then move along
            else
            {
                SaveModuleData();
                HTML_configString = null;
            }
            return 0;
        }

        #endregion


        #region |--Here is where you will place the method you would like to override on the storefront--|

        #endregion


        #region |--This is a template for adding the log messages into a method you are overriding for debugging--|
        /*
        // An example of how to check if in ebug mode and log messages to the store and to the .txt file as needed
        if ((string)ModuleData[ _YourExetensionAbbreviation_DEBUGGING_MODE] == "true")
        {
            // Log messages to the storefront "Logs" page
            LogMessage($"Our message to the store);                                    // Log Our message to the storefront "Logs" page
            LogMessage($"Our message to the store with variable: " + msg);             // Log Our message with variable to the storefront "Logs" page

            // Log messages to the .txt file
            LogMessageToFile($"Our message to the .txt logs);                          // Log Our message to the storefront "Logs" page
            LogMessageTofile($"Our message to the .txt logs with variable: " + msg);   // Log Our message with variable to the storefront "Logs" page
        }
        */
        #endregion

        //end of the class: Template
    }
    //end of the file
}