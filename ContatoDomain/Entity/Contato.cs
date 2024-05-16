using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContatoDomain.Entity
{
    /// <summary>
    /// Representa um Contato com id, nome, empresa, email e telefones: pessoal e comercial
    /// </summary>
    public class Contato
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Empresa { get; set; }
        public ICollection<Email> ListaEmails { get; set; }
        public string TelefonePessoal { get; set; }
        public string TelefoneComercial { get; set; }

        public Contato()
        {
            ListaEmails = new List<Email>();    
        }

        public void AdicionarEmail(Email email)
        {
            email.ContatoId = this.Id; // Atribui automaticamente o ID do contato ao email
            ListaEmails.Add(email);
        }

    }
}
