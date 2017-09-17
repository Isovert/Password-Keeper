using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace PasswordKeeper.Utillities
{
    static class ResourcesHelper
    {
        public static readonly string MESSAGE_LANGUAGE_CHANGE_AFTER_RESTART = "MESSAGE_LANGUAGE_CHANGE_AFTER_RESTART";

        public static ResourceManager Manager = new ResourceManager("PasswordKeeper.Properties.Resources", typeof(ResourcesHelper).Assembly);



    }
}
