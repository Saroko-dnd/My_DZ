//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowsFormsApplication1
{
    using System;
    using System.Collections.Generic;
    
    public partial class TMotor
    {
        public int IDD { get; set; }
        public string Marka { get; set; }
        public Nullable<double> Power { get; set; }
        public string Country { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual TAuto TAuto { get; set; }
    }
}
