
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace taske2.Models
{

using System;
    using System.Collections.Generic;
    
public partial class Language
{

    public int IDLanguage { get; set; }

    public string LanguageName { get; set; }

    public string Proficiency { get; set; }

    public Nullable<int> IDPerson { get; set; }



    public virtual Person Person { get; set; }

}

}
