using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using OnlineStoreLogic;

/// <summary>
/// Сводное описание для AccessToSession
/// </summary>
public class AccessorToSessionForListOfProductsPage
{
    private HttpSessionState CurrentSession;

    public void AddManagerOfProductsToSession(ManagerOfProducts CurrentManagerOfProducts)
    {
        CurrentSession["CurrentManagerOfProducts"] = CurrentManagerOfProducts;
    }

    public ManagerOfProducts GetManagerOfProductsForCurrentSession()
    {
        return (CurrentSession["CurrentManagerOfProducts"] as ManagerOfProducts);
    }

    public AccessorToSessionForListOfProductsPage(HttpSessionState NewCurrentSession)
    {
        CurrentSession = NewCurrentSession;
    }
}