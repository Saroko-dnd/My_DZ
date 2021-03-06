﻿<%@ Application Language="C#" %>
<%@ Import Namespace="Resources" %>
<%@ Import Namespace="OnlineStoreObjects" %>
<%@ Import Namespace="System.IO" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Код, выполняемый при запуске приложения
        // Mapping for jquery (for validators)
        ScriptManager.ScriptResourceMapping.AddDefinition("jquery",
        new ScriptResourceDefinition
        {
            Path = "https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"
        });
        
        //Creating of directory for images
        string FullPathToDirectoryForImages = HttpRuntime.AppDomainAppPath;
        Directory.CreateDirectory(FullPathToDirectoryForImages + "ProductImages");
        
        //Загружаем список категорий
        List<ProductCategory> ListOfProductCategories = new List<ProductCategory>();
        ListOfProductCategories.Add(new ProductCategory(Texts.ProductCategoryName_1, null));
        ListOfProductCategories.Add(new ProductCategory(Texts.ProductCategoryName_2, null));
        ListOfProductCategories.Add(new ProductCategory(Texts.ProductCategoryName_3, null));
        ListOfProductCategories.Add(new ProductCategory(Texts.ProductCategoryName_4, null));
        ListOfProductCategories.Add(new ProductCategory(Texts.ProductCategoryName_5, null));
        Application[Texts.KeyForListOfProductCategories] = ListOfProductCategories;
        //Загружаем название онлайн магазина
        Application[Texts.KeyForNameOfTheOnlineStore] = Texts.NameOfTheOnlineStore;
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
