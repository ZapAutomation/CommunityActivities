﻿// ----------------------------------------------------------------------
// <summary>DocumentTranslation.cs</summary>
// ----------------------------------------------------------------------

namespace TranslationAssistant.DocumentTranslationInterface.ViewModel
{
    #region Usings

    using Microsoft.Win32;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Windows.Input;
    using TranslationAssistant.Business;
    using TranslationAssistant.Business.Model;
    using TranslationAssistant.TranslationServices.Core;

    #endregion

    public class DocumentTranslation
    {
        #region Fields

        private bool _isNavigateToTargetFolderEnabled;

        /// <summary>
        ///     The file browse click command
        /// </summary>
        private ICommand inputFilePathBrowseClickCommand;

        /// <summary>
        ///     The input file path.
        /// </summary>
        private List<string> inputFilePaths = new List<string>();

        /// <summary>
        ///     The is go button enabled.
        /// </summary>
        private bool isGoButtonEnabled;

        /// <summary>
        ///     The is started.
        /// </summary>
        private bool isStarted;

        /// <summary>
        ///     The selected source language.
        /// </summary>
        private string selectedSourceLanguage;

        /// <summary>
        ///     The selected target language.
        /// </summary>
        private string selectedTargetLanguage;

        /// <summary>
        ///     The selected translate mode.
        /// </summary>
        private string selectedTranslateMode;



        /// <summary>
        ///     The show progress bar.
        /// </summary>
        private bool showProgressBar;

        /// <summary>
        ///     The source language list.
        /// </summary>
        private List<string> sourceLanguageList = new List<string>();

        /// <summary>
        ///     The status text.
        /// </summary>
        private string statusText;

        private string targetFolder;

        private ICommand targetFolderNavigateClickCommand;

        /// <summary>
        /// Shows at top of page to indicate we are not ready to translate
        /// </summary>
        private string _ReadyToTranslateMessage;

        /// <summary>
        /// The target language list.
        /// </summary>
        private List<string> targetLanguageList = new List<string>();

        /// <summary>
        /// The TranslateMode list.
        /// </summary>
        private List<string> translateModeList = new List<string>();

        /// <summary>
        /// Whether to ignore hidden content in translation, or not
        /// </summary>
        private bool ignoreHiddenContent = false;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DocumentTranslation" /> class.
        /// </summary>
        public DocumentTranslation()
        {
            TranslationServiceFacade.Initialize();
            this.PopulateAvailableLanguages();
            this.PopulateTranslateMode();
            this.ShowProgressBar = false;
            this.IsGoButtonEnabled = false;
            this.TargetFolder = string.Empty;
            this.SelectedTargetLanguage = string.Empty;
            //this.SelectedSourceLanguage = Properties.DocumentTranslator.Default.DefaultSourceLanguage;
            //this.SelectedTargetLanguage = Properties.DocumentTranslator.Default.DefaultTargetLanguage;
            //this.SelectedTranslateMode =  TranslateModeList[Properties.DocumentTranslator.Default.DefaultTranslateMode];  //0=plain text, 1=HTML
            //this.IgnoreHiddenContent = Properties.DocumentTranslator.Default.IgnoreHiddenContent;
            this.StatusText = string.Empty;
            if (TranslationServiceFacade.IsTranslationServiceReady())
            {
                this.StatusText = Properties.Resources.Common_SelectDocuments;
                this.PopulateReadyToTranslateMessage(true);
            }
            

            
        }

        /// <summary>
        /// Save the selected source and target languages for the next session;
        /// </summary>
        //public void SaveSettings()
        //{
        //    Properties.DocumentTranslator.Default.DefaultSourceLanguage = this.SelectedSourceLanguage;
        //    Properties.DocumentTranslator.Default.DefaultTargetLanguage = this.SelectedTargetLanguage;
        //    Properties.DocumentTranslator.Default.IgnoreHiddenContent = this.IgnoreHiddenContent;
        //    Properties.DocumentTranslator.Default.DefaultTranslateMode = TranslateModeList.IndexOf(SelectedTranslateMode);  //0=plain text, 1=HTML
        //    Properties.DocumentTranslator.Default.Save();
        //}

        #endregion

        #region Public Properties



        /// <summary>
        ///     Gets the input file path browse click command.
        /// </summary>
        //public ICommand InputFilePathBrowseClickCommand
        //{
        //    get
        //    {
        //        return this.inputFilePathBrowseClickCommand
        //               ?? (this.inputFilePathBrowseClickCommand = new DelegateCommand(this.FilePathBrowseClick));
        //    }
        //}

        /// <summary>
        ///     Gets or sets the input file path.
        /// </summary>
        public List<string> InputFilePaths
        {
            get
            {
                return this.inputFilePaths;
            }

            set
            {
                this.inputFilePaths = value;
    
                this.IsGoButtonEnabled = ((this.InputFilePaths.Count > 0) && (selectedTargetLanguage != string.Empty) && TranslationServiceFacade.IsTranslationServiceReady());
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether is go button enabled.
        /// </summary>
        public bool IsGoButtonEnabled
        {
            get
            {
                return this.isGoButtonEnabled;
            }

            set
            {
                this.isGoButtonEnabled = value;
                
            }
        }

        public bool IsNavigateToTargetFolderEnabled
        {
            get
            {
                return this._isNavigateToTargetFolderEnabled;
            }
            set
            {
                this._isNavigateToTargetFolderEnabled = value;
                
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether is started.
        /// </summary>
        public bool IsStarted
        {
            get
            {
                return this.isStarted;
            }

            set
            {
                this.isStarted = value;
                
            }
        }

        /// <summary>
        ///     Gets or sets the selected source language.
        /// </summary>
        public string SelectedSourceLanguage
        {
            get
            {
                return this.selectedSourceLanguage;
            }

            set
            {
                this.selectedSourceLanguage = value;
                
            }
        }

        /// <summary>
        ///     Gets or sets the selected target language.
        /// </summary>
        public string SelectedTargetLanguage
        {
            get
            {
                return this.selectedTargetLanguage;
            }

            set
            {
                this.selectedTargetLanguage = value;
                
                this.IsGoButtonEnabled = ((this.InputFilePaths.Count > 0) && (selectedTargetLanguage != string.Empty) && TranslationServiceFacade.IsTranslationServiceReady());
            }
        }

        /// <summary>
        ///     Gets or sets the selected target language.
        /// </summary>
        public string SelectedTranslateMode
        {
            get
            {
                return this.selectedTranslateMode;
            }

            set
            {
                this.selectedTranslateMode = value;
                
            }
        }



        /// <summary>
        ///     Gets or sets a value indicating whether show progress bar.
        /// </summary>
        public bool ShowProgressBar
        {
            get
            {
                return this.showProgressBar;
            }

            set
            {
                this.showProgressBar = value;
                
            }
        }

        /// <summary>
        ///     Gets or sets the source language list.
        /// </summary>
        public List<string> SourceLanguageList
        {
            get
            {
                return this.sourceLanguageList;
            }

            set
            {
                this.sourceLanguageList = value;
                
            }
        }

        public string ReadyToTranslateMessage
        {
            get { return this._ReadyToTranslateMessage; }
            set
            {
                this._ReadyToTranslateMessage = value;
                
            }
        }

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

        public string TargetFolder
        {
            get
            {
                return this.targetFolder;
            }

            set
            {
                this.targetFolder = value;
                
            }
        }

        //public ICommand TargetFolderNavigateClickCommand
        //{
        //    get
        //    {
        //        return this.targetFolderNavigateClickCommand
        //               ?? (this.targetFolderNavigateClickCommand = new DelegateCommand(this.NavigateToTargetFolderClick));
        //    }
        //}

        /// <summary>
        ///     Gets or sets the target language list.
        /// </summary>
        public List<string> TargetLanguageList
        {
            get
            {
                return this.targetLanguageList;
            }

            set
            {
                this.targetLanguageList = value;
                
            }
        }

        /// <summary>
        ///     Gets or sets the TranslateMode list.
        /// </summary>
        public List<string> TranslateModeList
        {
            get
            {
                return this.translateModeList;
            }

            set
            {
                this.translateModeList = value;
                
            }
        }





        /// <summary>
        /// Gets or sets a value indicating whether hidden content should be ignored.
        /// </summary>
        public bool IgnoreHiddenContent
        {
            get
            {
                return this.ignoreHiddenContent;
            }
            set
            {
                this.ignoreHiddenContent = value;
                
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Assemblies the browse_ click.
        /// </summary>
        //private void FilePathBrowseClick()
        //{
        //    //var openfileDlg = new OpenFileDialog
        //    //                      {
        //    //                          Filter = $"{Properties.Resources.Common_SupportedFiles}|*.doc; *.docx; *.pdf; *.xls; *.xlsx; *.ppt; *.pptx; *.txt; *.text; *.htm; *.html; *.srt",      //Add XLF file types here
        //    //                          Multiselect = true
        //    //                      };
        //    if (openfileDlg.ShowDialog().Value)
        //    {
        //        this.InputFilePaths = openfileDlg.FileNames.ToList();
        //    }
        //    this.StatusText = "";
        //}

        private void NavigateToTargetFolderClick()
        {
            if (!string.IsNullOrEmpty(this.TargetFolder))
            {
                Process.Start(new ProcessStartInfo(this.TargetFolder));
            }
        }

        /// <summary>
        ///     Populate available source and target languages.
        /// </summary>
        private void PopulateAvailableLanguages()
        {
            this.SourceLanguageList.Clear();
            this.TargetLanguageList.Clear();
            this.SourceLanguageList.Add(Properties.Resources.Common_AutoDetect);
            try
            {
                this.TargetLanguageList.AddRange(TranslationServiceFacade.AvailableLanguages.Values);
            }
            catch {
                this.StatusText = Properties.Resources.Error_LanguageList;
                
                return;
            };
            this.TargetLanguageList.Sort();
            this.SourceLanguageList.AddRange(this.TargetLanguageList);
            
        }

        private void PopulateTranslateMode()
        {
            this.TranslateModeList.Clear();
            this.TranslateModeList.Add(Properties.Resources.Common_PlainText);
            this.TranslateModeList.Add(Properties.Resources.Common_HTML);
        }


        /// <summary>
        /// Populate the Ready to Translate message at top of screen.
        /// </summary>
        private void PopulateReadyToTranslateMessage(bool successful)
        {
            //if (TranslationServiceFacade.IsTranslationServiceReady())
            if (successful)
            {
                ReadyToTranslateMessage = "";
                PopulateAvailableLanguages();
            }
            else
            {
                ReadyToTranslateMessage =  Properties.Resources.Translate_invalidcredentials;
            }
        }

        #endregion
    }
}
