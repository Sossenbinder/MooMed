using MooMed.Common.Definitions;
using MooMed.Core.Translations;

namespace MooMed.Web.Models
{
    public class ControllerMetaData
    {
        public string Title { get; set; }

        public Language UiLanguage { get; set; }

        public string ReactRoute { get; set; }

        public object DataModel { get; set; }

        public ControllerMetaData()
        {

        }

        public ControllerMetaData(string title, Language uiLanguage, string reactRoute, object dataModel)
        {
            Title = title;
            UiLanguage = uiLanguage;
            ReactRoute = reactRoute;
            DataModel = dataModel;
        }
    }
}