using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationAssistant.Business;
using TranslationAssistant.DocumentTranslationInterface.ViewModel;
using TranslationAssistant.TranslationServices.Core;
using Zappy.SharedInterface;
using Zappy.SharedInterface.Helper;

namespace ZappyLanguageTranslator
{
    [Description("Zap Language Translator")]
    public class LanguageTranslator : TemplateAction
    {
        [Category("Input")]
        [Description("Enter the File path")]
        public DynamicProperty<string> FilePath { get; set; }

        [Category("Input")]
        [Description("Enter the Document Language like 'en'")]
        public DynamicProperty<string> FromLanguage { get; set; }

        [Category("Input")]
        [Description("Enter the Language in which you have to convert the document like 'ja'")]
        public DynamicProperty<string> ToLanguage { get; set; }

        [Category("Input")]
        [Description("Enter the Text Translation Resource Key")]
        public DynamicProperty<string> TextTranslatorResorceKey { get; set; }

        public override void Invoke(IZappyExecutionContext context, ZappyTaskActionInvoker actionInvoker)
        {
            TranslationServiceFacade.AzureKey = TextTranslatorResorceKey;
            AccountViewModel.SaveAccountClick();
            var worker = new BackgroundWorker();
            DocumentTranslationManager.DoTranslation(
                           FilePath,
                           false,
                          FromLanguage,
                           ToLanguage,
                           false);
        }
    }
}
