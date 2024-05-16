using ContatoDomain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContatoDomain.Models
{
    public class ContatoDTORequest
    {
        public string Nome { get; set; }
        public string Empresa { get; set; }
        public ICollection<Email> ListaEmails { get; set; }
        public string TelefonePessoal { get; set; }
        public string TelefoneComercial { get; set; }

    }
}
