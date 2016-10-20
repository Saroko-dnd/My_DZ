<%@ Application Language="C#" %>
<%@ Import  Namespace="System.IO" %>
<%@ Import  Namespace="Newtonsoft.Json" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        string JsonFileName = "JsonDatabase.json";
        string FullPathToJsonDirectory = HttpContext.Current.Server.MapPath("/JsonDatabase");
        string FullPathToFile = FullPathToJsonDirectory + "/" + JsonFileName;
        Directory.CreateDirectory(FullPathToJsonDirectory);
        if (!File.Exists(FullPathToFile))
        {
            File.Create(FullPathToFile);
        }
        else
        {
            JsonBasedMembershipProvider.DataStorageForJson.CurrentCollectionOfUsers = JsonConvert.DeserializeObject<List<JsonBasedMembershipProvider.CustomUser>>(File.ReadAllText(FullPathToFile));
        }
        if (JsonBasedMembershipProvider.DataStorageForJson.CurrentCollectionOfUsers == null)
        {
            JsonBasedMembershipProvider.DataStorageForJson.CurrentCollectionOfUsers = new List<JsonBasedMembershipProvider.CustomUser>();
        }
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown
        string UsersDataJson = JsonConvert.SerializeObject(JsonBasedMembershipProvider.DataStorageForJson.CurrentCollectionOfUsers);
    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

</script>
