using System.Globalization;
using System.Resources;

namespace MainDen.Crossinform.TestTask2021
{
    internal static class Localization
    {
        private static ResourceManager s_resourceManager;

        private static CultureInfo s_resourceCulture;

        internal static ResourceManager ResourceManager
        {
            get
            {
                return s_resourceManager ??= new ResourceManager(typeof(Localization).FullName, typeof(Localization).Assembly);
            }
        }

        internal static CultureInfo Culture
        {
            get
            {
                return s_resourceCulture;
            }
            set
            {
                s_resourceCulture = value;
            }
        }

        internal static string Info => ResourceManager.GetString("Info", s_resourceCulture);
    }
}
