using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContatoDomain.Entity;
using ContatoWebApplication.Data;

namespace ContatoWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController(AppDbContext _context) : ControllerBase
    {                       
        // GET: api/Contato
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contato>>> GetContato()
        {
            return await _context.Contato
                .OrderBy(contato => contato.nome) // ordernar os contatos por nome
                .Include(contato => contato.listaEmails) // listar os e-mails do contato na consulta
                .ToListAsync();
        }

        // GET: api/Contato/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contato>> GetContato(int id)
        {
            var contato = await _context.Contato
                .Include(contato => contato.listaEmails) // listar os e-mails do contato na consulta
                .FirstOrDefaultAsync(contato => contato.id == id);

            if (contato == null)
            {
                return NotFound("Contato não encontrado pelo ID: " + id + ". Tente novamente.");
            }

            return contato;
        }

        // PUT: api/Contato/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContato(int id, Contato contato)
        {
            if (id != contato.id)
            {
                return BadRequest();
            }

            // Carrega o contato e a lista de e-mails
            var contatoParaAtualizar = await _context.Contato
                .Include(contato => contato.listaEmails)
                .FirstOrDefaultAsync(contato => contato.id == id);

            if(contatoParaAtualizar == null)
            {
                return NotFound();
            }

            // Atualiza os campos do cadastro de contato apenas
            contatoParaAtualizar.nome = contato.nome;
            contatoParaAtualizar.empresa = contato.empresa;
            contatoParaAtualizar.telefonePessoal = contato.telefonePessoal;
            contatoParaAtualizar.telefoneComercial = contato.telefoneComercial;

            // Atualiza os e-mails existentes e adiciona novos
            foreach(var email in contato.listaEmails)
            {
                var emailExistente = contatoParaAtualizar.listaEmails.FirstOrDefault(e => e.id == email.id);
                if(emailExistente != null)
                {
                    // Se o e-mail já existir, atualize
                    emailExistente.enderecoEmail = email.enderecoEmail;
                } else
                {
                    // Se o e-mail NÃO já existir, adicione
                    contatoParaAtualizar.listaEmails.Add(email);
                }
            }
                      

            // Atualiza o contato no banco de dados
            _context.Entry(contato).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContatoExists(id))
                {
                    return NotFound("Contato não encontrado pelo ID: " + id + ". Tente novamente.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Contato
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Contato>> PostContato(Contato contato)
        {
            _context.Contato.Add(contato);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContato", new { id = contato.id }, contato);
        }

        // DELETE: api/Contato/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContato(int id)
        {
            var contato = await _context.Contato.FindAsync(id);
            if (contato == null)
            {
                return NotFound("Contato não encontrado pelo ID: " + id + ". Tente novamente."); 
            }

            _context.Contato.Remove(contato);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContatoExists(int id)
        {
            return _context.Contato.Any(e => e.id == id);
        }
    }
}
