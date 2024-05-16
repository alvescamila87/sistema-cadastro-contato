using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Key]
        public int id { get; set; }
        public string enderecoEmail { get; set; }

        public Email()
        {
            
        }
    }


}
