//------------------------------------------------------------------------------
// <auto-generated>
//     Codice generato da un modello.
//
//     Le modifiche manuali a questo file potrebbero causare un comportamento imprevisto dell'applicazione.
//     Se il codice viene rigenerato, le modifiche manuali al file verranno sovrascritte.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CompanyProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reseller
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reseller()
        {
            this.OrderHeaders = new HashSet<OrderHeader>();
        }
    
        public int ResellerID { get; set; }
        public string ResellerIdAPI { get; set; }
        public string BusinessName { get; set; }
        public string VAT { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Mail { get; set; }
        public string TelephoneNumber { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderHeader> OrderHeaders { get; set; }
    }
}
