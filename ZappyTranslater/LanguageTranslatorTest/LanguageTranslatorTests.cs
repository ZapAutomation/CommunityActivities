using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZappyLanguageTranslator;

namespace LanguageTranslatorTest
{
    [TestClass]
    public class LanguageTranslatorTests
    {
        [TestMethod]
        public void Invoke()
        {
            //Arrange
         //   string FilePath = @"C:\...\Documents\ZappySamples\ExtractRequiredValuesFromPDF\PDFExtractionAutomation.docx";
            string FilePath = "TestFiles/PDFExtractionAutomation.docx";
            string FromLanguage = "en";
            string ToLanguage = "ja";

            //Replace your Azure's ResourceKey
            string TextTranslatorResorceKey = "Replace ResourceKey";
            LanguageTranslator action = new LanguageTranslator();

            //Act
            action.FilePath = FilePath;
            action.FromLanguage = FromLanguage;
            action.ToLanguage = ToLanguage;
            action.TextTranslatorResorceKey = TextTranslatorResorceKey;
            action.Invoke(null, null);

        }
    }
}
