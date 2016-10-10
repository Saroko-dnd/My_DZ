using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using OnlineStoreLogic;
using Resources;

/// <summary>
/// Сводное описание для AccessToSession
/// </summary>
public class AccessorToSessionForListOfProductsPage
{
    private HttpSessionState CurrentSession;

    public void AddManagerOfProductsToSession(ManagerOfProducts CurrentManagerOfProducts)
    {
        CurrentSession[Texts.KeyForManagerOfProducts] = CurrentManagerOfProducts;
    }

    public ManagerOfProducts GetManagerOfProductsForCurrentSession()
    {
        return (CurrentSession[Texts.KeyForManagerOfProducts] as ManagerOfProducts);
    }

    public AccessorToSessionForListOfProductsPage(HttpSessionState NewCurrentSession)
    {
        CurrentSession = NewCurrentSession;
    }
}