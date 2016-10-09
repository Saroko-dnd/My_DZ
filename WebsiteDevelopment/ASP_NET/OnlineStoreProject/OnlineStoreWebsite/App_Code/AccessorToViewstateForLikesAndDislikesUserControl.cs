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

    public AccessorToViewstateForLikesAndDislikesUserControl(StateBag NewCurrentStateBag)
    {
        CurrentStateBag = NewCurrentStateBag;
    }
}