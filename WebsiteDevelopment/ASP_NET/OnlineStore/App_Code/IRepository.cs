using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IRepository
/// </summary>
interface IRepository<DataType>
{
    IEnumerable<DataType> GetAllData();
}