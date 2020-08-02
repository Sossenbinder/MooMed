using MooMed.Common.Definitions;

namespace MooMed.Frontend.Models
{
    public class ControllerMetaData
    {
        public string Title { get; set; }

        public Language UiLanguage { get; set; }

        public object DataModel { get; set; }

        public ControllerMetaData()
        {

        }

        public ControllerMetaData(string title, Language uiLanguage)
        {
	        Title = title;
	        UiLanguage = uiLanguage;
        }

        public ControllerMetaData(string title, Language uiLanguage, object dataModel)
        {
            Title = title;
            UiLanguage = uiLanguage;
            DataModel = dataModel;
        }
    }
}