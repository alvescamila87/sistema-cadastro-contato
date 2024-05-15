using System;
using System.Collections.Generic;
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
        public int id { get; set; }
        public string nome { get; set; }
        public string empresa { get; set; }
        public List<Email> listaEmails { get; set; }
        public string telefonePessoal { get; set; }
        public string telefoneComercial { get; set; }

        public Contato()
        {

        }

    }
}
