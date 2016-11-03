using NewsWebsite.ClassesForNewsWebsite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsWebsite.Areas.Admin.Models
{
    public class ModelForNewsCreationMenu
    {
        private NewsForEditInEditorTemplate currentNews;

        public NewsForEditInEditorTemplate CurrentNews
        {
            get
            {
                return currentNews;
            }

            set
            {
                currentNews = value;
            }
        }

        public ModelForNewsCreationMenu()
        {

        }
    }
}