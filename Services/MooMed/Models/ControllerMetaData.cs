using MooMed.Common.Definitions;
using MooMed.Core.Translations;

namespace MooMed.Web.Models
{
    public class ControllerMetaData
    {
        public string Title { get; set; }

        public Language UiLanguage { get; set; }

        public object DataModel { get; set; }

        public ControllerMetaData()
        {

        }

        public ControllerMetaData(string title, Language uiLanguage, object dataModel)
        {
            Title = title;
            UiLanguage = uiLanguage;
            DataModel = dataModel;
        }
    }
}