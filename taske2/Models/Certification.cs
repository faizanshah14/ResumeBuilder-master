
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
    
public partial class Certification
{

    public int IdCertificate { get; set; }

    public string CertificateName { get; set; }

    public string CertificationAuthority { get; set; }

    public string CertificationLevel { get; set; }

    public Nullable<int> IDPerson { get; set; }



    public virtual Person Person { get; set; }

}

}
