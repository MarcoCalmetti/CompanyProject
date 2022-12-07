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
    using System.ComponentModel.DataAnnotations;

    public partial class Reseller
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reseller()
        {
            this.OrderHeaders = new HashSet<OrderHeader>();
        }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public int ResellerID { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string BusinessName { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string VAT { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string City { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Mail { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string TelephoneNumber { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderHeader> OrderHeaders { get; set; }
    }
}
