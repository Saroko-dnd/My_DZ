using NewsDataAccess;
using NewsInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsWebsite.Models
{
    public class TestClassWithWebsiteData
    {
        private IEnumerable<User> testListOfUsers;

        public IEnumerable<User> TestListOfUsers
        {
            get { return testListOfUsers; }
            set { testListOfUsers = value; }
        }

        private IEnumerable<News> testListOfNews;

        public IEnumerable<News> TestListOfNews
        {
            get { return testListOfNews; }
            set { testListOfNews = value; }
        }
    }
}