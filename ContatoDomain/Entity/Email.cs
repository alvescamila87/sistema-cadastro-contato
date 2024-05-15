using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContatoDomain.Entity
{
    /// <summary>
    /// Representa o e-mail do contato
    /// </summary>
    public class Email
    {
        public int Id { get; set; }
        public Contato contato { get; set; }
        public string enderecoEmail { get; set; }

        public Email()
        {
            
        }
    }


}
