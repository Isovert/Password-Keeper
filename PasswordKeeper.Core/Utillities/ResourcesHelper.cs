using System.Resources;

namespace PasswordKeeper.Core
{
    static class ResourcesHelper
    {
        public static readonly string MESSAGE_LANGUAGE_CHANGE_AFTER_RESTART = "MESSAGE_LANGUAGE_CHANGE_AFTER_RESTART";

        public static ResourceManager Manager = new ResourceManager("PasswordKeeper.Properties.Resources", typeof(ResourcesHelper).Assembly);
    }
}
