using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace AppGestionStockMVC.Models
{
    public class Commande
    {
        public int Id { get; set; }

        public string ClientId { get; set; } // FK vers IdentityUser
        public virtual Client ?Client { get; set; }

        public DateTime DateCommande { get; set; }
        public string Statut { get; set; } // EnCours, Validée, Annulée...

        public virtual ICollection<LigneCommande> LignesCommande { get; set; }
    }
}
