using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContatoDomain.Entity;
using ContatoWebApplication.Data;
using ContatoDomain.Models;
using AutoMapper;

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
                .OrderBy(contato => contato.Nome) // ordernar os contatos por nome
                .Include(contato => contato.ListaEmails) // listar os e-mails do contato na consulta
                .ToListAsync();
        }

        // GET: api/Contato/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contato>> GetContato(int id)
        {
            var contato = await _context.Contato
                .Include(contato => contato.ListaEmails) // listar os e-mails do contato na consulta
                .FirstOrDefaultAsync(contato => contato.Id == id);

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
            if (id != contato.Id)
            {
                return BadRequest();
            }

            // Carrega o contato e a lista de e-mails
            var contatoParaAtualizar = await _context.Contato
                .Include(contato => contato.ListaEmails)
                .FirstOrDefaultAsync(contato => contato.Id == id);

            if(contatoParaAtualizar == null)
            {
                return NotFound();
            }

            // Atualiza os campos do cadastro de contato apenas
            contatoParaAtualizar.Nome = contato.Nome;
            contatoParaAtualizar.Empresa = contato.Empresa;
            contatoParaAtualizar.TelefonePessoal = contato.TelefonePessoal;
            contatoParaAtualizar.TelefoneComercial = contato.TelefoneComercial;

            // Atualiza os e-mails existentes e adiciona novos
            foreach(var email in contato.ListaEmails)
            {
                var emailExistente = contatoParaAtualizar.ListaEmails.FirstOrDefault(e => e.Id == email.Id);
                if(emailExistente != null)
                {
                    // Se o e-mail já existir, atualize
                    emailExistente.EnderecoEmail = email.EnderecoEmail;
                } else
                {
                    // Se o e-mail NÃO já existir, adicione
                    contatoParaAtualizar.ListaEmails.Add(email);
                }
            }
                      

            // Atualiza o contato no banco de dados
            _context.Entry(contatoParaAtualizar).State = EntityState.Modified;

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
        [HttpPost]
        public async Task<ActionResult<Contato>> PostContato(ContatoDTORequest contatoRequest)
        {

            var novoContato = new Contato()
            {
                Nome = contatoRequest.Nome,
                Empresa = contatoRequest.Empresa,
                TelefonePessoal = contatoRequest.TelefonePessoal,
                TelefoneComercial = contatoRequest.TelefoneComercial,
            };

            var listaEmails = new List<Email>();

                foreach (var emailDTO in contatoRequest.ListaEmails)
                {
                    var novoEmail = new Email
                    {
                        EnderecoEmail = emailDTO.EnderecoEmail,
                        ContatoId = novoContato.Id, // Associar o email ao novo contato
                    };

                    novoContato.ListaEmails.Add(novoEmail);
                }
            

            _context.Contato.Add(novoContato);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContato", new { id = novoContato.Id }, novoContato);
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
            return _context.Contato.Any(e => e.Id == id);
        }
    }
}
