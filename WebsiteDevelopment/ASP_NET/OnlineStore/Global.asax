<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Код, выполняемый при запуске приложения
        //Загружаем список категорий
        List<ProductCategory> ListOfProductCategories = new List<ProductCategory>();
        ListOfProductCategories.Add(new ProductCategory("Category_1"));
        ListOfProductCategories.Add(new ProductCategory("Category_2"));
        ListOfProductCategories.Add(new ProductCategory("Category_3"));
        ListOfProductCategories.Add(new ProductCategory("Category_4"));
        ListOfProductCategories.Add(new ProductCategory("Category_5"));
        Application["ListOfProductCategories"] = ListOfProductCategories;
        //Загружаем название онлайн магазина
        Application["OnlineStoreName"] = "Name of the online store";
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
