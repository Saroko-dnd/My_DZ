<%@ Application Language="C#" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="Resources" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Код, выполняемый при запуске приложения
        Directory.CreateDirectory(Texts.FullPathOfDirectoryForImages);

        ImagesInMemoryForRepeater.AllImagesOnServer = new List<ImageInStorage>();
        foreach (string CurrentFileName in Directory.GetFiles(Texts.FullPathOfDirectoryForImages))
        {
            ImagesInMemoryForRepeater.AllImagesOnServer.Add(new ImageInStorage(Path.GetFileNameWithoutExtension(CurrentFileName), Texts.DirectoryForImagesName + "/" + Path.GetFileName(CurrentFileName)));
        }
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Код, выполняемый при завершении работы приложения

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Код, выполняемый при возникновении необрабатываемой ошибки

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Код, выполняемый при запуске нового сеанса

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Код, выполняемый при запуске приложения. 
        // Примечание: Событие Session_End вызывается только в том случае, если для режима sessionstate
        // задано значение InProc в файле Web.config. Если для режима сеанса задано значение StateServer 
        // или SQLServer, событие не порождается.

    }
       
</script>
