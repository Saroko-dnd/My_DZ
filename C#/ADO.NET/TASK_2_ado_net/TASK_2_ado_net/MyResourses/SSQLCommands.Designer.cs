﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TASK_2_ado_net.MyResourses {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class SSQLCommands {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SSQLCommands() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TASK_2_ado_net.MyResourses.SSQLCommands", typeof(SSQLCommands).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT CompanyName,City,Country,SUM(OrderPrice) FROM Orders INNER JOIN 
        ///(SELECT (UnitPrice * Quantity) AS OrderPrice,OrderID FROM [Order Details]) 
        ///AS OrderPriceTable
        /// ON (Orders.OrderID = OrderPriceTable.OrderID 
        /// AND DATEPART(day,Orders.OrderDate) &gt;=1_
        /// AND DATEPART(month,Orders.OrderDate) &gt;=2_ AND
        ///  DATEPART(year,Orders.OrderDate) &gt;= 3_ 
        /// AND DATEPART(day,Orders.OrderDate) &lt;=4_
        /// AND DATEPART(month,Orders.OrderDate) &lt;=5_
        /// AND DATEPART(year,Orders.OrderDate) &lt;= 6_) 
        /// INNER JOIN Customers 
        /// ON Cu [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string FifthQuery {
            get {
                return ResourceManager.GetString("FifthQuery", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT   CompanyName,ContactName,Address,City,Country,Phone,OrderDate,ShippedDate,Freight,ShipCity,ShipRegion,ShipCountry,Quantity,UnitPrice,Discount  FROM Customers INNER JOIN [Orders] ON Customers.CustomerID=[Orders].CustomerID INNER JOIN [Order Details] ON [Orders].[OrderID]=[Order Details].[OrderID].
        /// </summary>
        internal static string FirstQuery {
            get {
                return ResourceManager.GetString("FirstQuery", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT COUNT(*)
        ///  FROM Customers WHERE City =.
        /// </summary>
        internal static string FourthQuery {
            get {
                return ResourceManager.GetString("FourthQuery", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT  LastName,FirstName,BirthDate,Address,HomePhone,Photo FROM Employees.
        /// </summary>
        internal static string SecondQuery {
            get {
                return ResourceManager.GetString("SecondQuery", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT 
        ///      ProductName
        ///      ,UnitPrice
        ///	  ,Discontinued
        ///      ,QuantityPerUnit
        ///	  ,CategoryName
        ///  FROM Products INNER JOIN Categories 
        ///  ON (Products.CategoryID = Categories.CategoryID AND Products.UnitPrice &gt;= 10 AND 
        ///  Products.UnitPrice &lt;= 60).
        /// </summary>
        internal static string ThirdQuery {
            get {
                return ResourceManager.GetString("ThirdQuery", resourceCulture);
            }
        }
    }
}
