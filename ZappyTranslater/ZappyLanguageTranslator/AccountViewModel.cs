// // ----------------------------------------------------------------------
// // <copyright file="AccountViewModel.cs" company="Microsoft Corporation">
// // Copyright (c) Microsoft Corporation.
// // All rights reserved.
// // THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
// // KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// // IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// // PARTICULAR PURPOSE.
// // </copyright>
// // ----------------------------------------------------------------------
// // <summary>AccountViewModel.cs</summary>
// // ----------------------------------------------------------------------

namespace TranslationAssistant.DocumentTranslationInterface.ViewModel
{
    using System.Windows.Input;

    /// <summary>
    ///     The account view model.
    /// </summary>
    public class AccountViewModel
    {
        #region Fields

        /// <summary>
        ///     The application id.
        /// </summary>
        private string _AzureKey;

        
        /// <summary>
        ///     The category identifier.
        /// </summary>
        private string categoryID;

        /// <summary>
        /// Use the Government instance of Azure (true) or not (false) 
        /// </summary>
        private bool useAzureGovernment;

        /// <summary>
        ///     The save account settings click command.
        /// </summary>
        private ICommand saveAccountSettingsClickCommand;

        /// <summary>
        ///     The status text.
        /// </summary>
        private string statusText;

        #endregion

        #region Constructors and Destructors


        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the application id.
        /// </summary>
        public string AzureKey
        {
            get
            {
                return this._AzureKey;
            }

            set
            {
                this._AzureKey= value;
            }
        }

        
        public string CategoryID
        {
            get
            {
                return this.categoryID;
            }

            set
            {
                this.categoryID = value;
                
            }
        }

        public bool UseAzureGovernment
        {
            get
            {
                return this.useAzureGovernment;
            }

            set
            {
                this.useAzureGovernment = value;
                
            }
        }

        /// <summary>
        ///     Gets the save account settings click command.
        /// </summary>
        //public ICommand SaveAccountSettingsClickCommand
        //{
        //    get
        //    {
        //        return this.saveAccountSettingsClickCommand
        //               ?? (this.saveAccountSettingsClickCommand = new DelegateCommand(this.SaveAccountClick));
        //    }
        //}

        /// <summary>
        ///     Gets or sets the status text.
        /// </summary>
        public string StatusText
        {
            get
            {
                return this.statusText;
            }

            set
            {
                this.statusText = value;
                
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Load the account settings from the DocumentTranslator.settings file, which is actually in the user appsettings folder and named user.config.
        /// </summary>
        public AccountViewModel()
        {
            //Initialize in order to load the credentials.
            TranslationServices.Core.TranslationServiceFacade.Initialize();
            this.AzureKey = TranslationServices.Core.TranslationServiceFacade.AzureKey;
            this.categoryID = TranslationServices.Core.TranslationServiceFacade.CategoryID;
            this.useAzureGovernment = TranslationServices.Core.TranslationServiceFacade.UseAzureGovernment;
        }

        /// <summary>
        ///     Saves the account settings to the settings file for next use.
        /// </summary>
        public static void SaveAccountClick()
        {
            //Set the Account values and save.
            TranslationServices.Core.TranslationServiceFacade.AzureKey = TranslationServices.Core.TranslationServiceFacade.AzureKey.Trim();
            TranslationServices.Core.TranslationServiceFacade.CategoryID = "";
            TranslationServices.Core.TranslationServiceFacade.UseAzureGovernment = false;
            TranslationServices.Core.TranslationServiceFacade.SaveCredentials();
            TranslationServices.Core.TranslationServiceFacade.Initialize(true);

            bool isready = false;

            try { isready = TranslationServices.Core.TranslationServiceFacade.IsTranslationServiceReady(); }
            catch { isready = false; }

            if (isready) {
                
            }
            else
            {
                
            }
            //if (!TranslationServices.Core.TranslationServiceFacade.IsCategoryValid(this.categoryID))
            //{
               
            //}
            return;
          
        }

        #endregion
    }
}