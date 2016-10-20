<%@ Application Language="C#" %>
<%@ Import  Namespace="System.IO" %>
<%@ Import  Namespace="Newtonsoft.Json" %>
<%@ Import  Namespace="Resources" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        string FullPathToJsonDirectory = HttpContext.Current.Server.MapPath("/" + Texts.NameOfDirectoryForJsonData);
        string FullPathToFile = FullPathToJsonDirectory + "/" + Texts.NameOfFileForJsonData;
        Directory.CreateDirectory(FullPathToJsonDirectory);
        if (!File.Exists(FullPathToFile))
        {
            File.Create(FullPathToFile);
        }
        else
        {
            JsonBasedMembershipProvider.DataStorageForJson.CurrentCollectionOfUsers = JsonConvert.DeserializeObject<List<JsonBasedMembershipProvider.CustomUser>>(File.ReadAllText(FullPathToFile, Encoding.UTF8));
        }
        if (JsonBasedMembershipProvider.DataStorageForJson.CurrentCollectionOfUsers == null)
        {
            JsonBasedMembershipProvider.DataStorageForJson.CurrentCollectionOfUsers = new List<JsonBasedMembershipProvider.CustomUser>();
        }
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
        Session["Nothing"] = 8;
    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

</script>
