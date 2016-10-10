using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Сводное описание для AccessorToViewstateForLikesAndDislikesUserControl
/// </summary>
public class AccessorToViewstateForLikesAndDislikesUserControl
{
    private StateBag CurrentStateBag;

    public void AddProductIDToViewstate(int CurrentProductID)
    {
        CurrentStateBag[Texts.KeyForProductID] = CurrentProductID;
    }

    public int GetProductIDForCurrentViewstate()
    {
        return (int)CurrentStateBag[Texts.KeyForProductID];
    }

    public AccessorToViewstateForLikesAndDislikesUserControl(StateBag NewCurrentStateBag)
    {
        CurrentStateBag = NewCurrentStateBag;
    }
}