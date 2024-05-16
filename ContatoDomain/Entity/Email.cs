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
        public int Id { get; set; }
        public string EnderecoEmail { get; set; }

        #region [Associações do Contato aos seus e-mails]
        public int ContatoId { get; set; }
        #endregion

        public Email()
        {
            
        }
    }


}
