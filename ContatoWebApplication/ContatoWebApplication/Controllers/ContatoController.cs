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

namespace ContatoWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController(AppDbContext _context) : ControllerBase
    {
        /// <summary>
        /// Retorna todos os contatos cadastrados
        /// </summary>
        /// <returns>Uma lista de contatos.</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Contato>>> GetContato()
        {
            return await _context.Contato
                .OrderBy(contato => contato.Nome) // ordernar os contatos por nome
                .Include(contato => contato.ListaEmails) // listar os e-mails do contato na consulta
                .ToListAsync();
        }

        /// <summary>
        /// Retorna um contato com o ID especificado.
        /// </summary>
        /// <param name="id">O ID do contato a ser retornado.</param>
        /// <returns>O contato correspondente ao ID fornecido.</returns>
        /// <response code="200">O contato foi encontrado e retornado com sucesso.</response>
        /// <response code="404">O contato com o ID fornecido não foi encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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

        /// <summary>
        /// Retorna um contato com o nome especificado.
        /// </summary>
        /// <param name="id">O nome do contato a ser retornado.</param>
        /// <returns>O contato correspondente ao nome fornecido.</returns>
        /// <response code="200">O contato foi encontrado e retornado com sucesso.</response>
        [HttpGet("FiltrarPorNome")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Contato>>> FiltrarPorNome(string nome)
        {
            return await _context.Contato
                .Where(contato => contato.Nome.Contains(nome))
                .Include(contato => contato.ListaEmails)
                .ToListAsync();
        }

        /// <summary>
        /// Retorna um contato com a empresa especificada.
        /// </summary>
        /// <param name="id">A empresa do contato a ser retornado.</param>
        /// <returns>O contato correspondente à empresa fornecida.</returns>
        /// <response code="200">O contato foi encontrado e retornado com sucesso.</response>
        [HttpGet("FiltrarPorEmpresa")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Contato>>> FiltarPorEmpresa(string empresa)
        {
            return await _context.Contato
                .Where(contato => contato.Empresa.Contains(empresa))
                .Include(contato => contato.ListaEmails)
                .ToListAsync();
        }

        /// <summary>
        /// Retorna um contato com o telefone pessoal especificado.
        /// </summary>
        /// <param name="id">O telefone pessoal do contato a ser retornado.</param>
        /// <returns>O contato correspondente ao telefone fornecido.</returns>
        /// <response code="200">O contato foi encontrado e retornado com sucesso.</response>
        [HttpGet("FiltrarPorTelefonePessoal")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Contato>>> FiltrarPorTelefonePessoal(string telefonePessoal)
        {
            return await _context.Contato
                .Where(contato => contato.TelefonePessoal.Contains(telefonePessoal))
                .Include(contato => contato.ListaEmails)
                .ToListAsync();
        }

        /// <summary>
        /// Retorna um contato com o telefone comercial especificado.
        /// </summary>
        /// <param name="id">O telefone comercial do contato a ser retornado.</param>
        /// <returns>O contato correspondente ao telefone fornecido.</returns>
        /// <response code="200">O contato foi encontrado e retornado com sucesso.</response>
        [HttpGet("FiltrarPorTelefoneComercial")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Contato>>> FiltrarPorTelefoneComercial(string telefoneComercial)
        {
            return await _context.Contato
                .Where(contato => contato.TelefoneComercial.Contains(telefoneComercial))
                .Include(contato => contato.ListaEmails)
                .ToListAsync();
        }

        /// <summary>
        /// Retorna um contato com o email especificado.
        /// </summary>
        /// <param name="id">O email do contato a ser retornado.</param>
        /// <returns>O contato correspondente ao email fornecido.</returns>
        /// <response code="200">O contato foi encontrado e retornado com sucesso.</response>
        [HttpGet("FiltrarPorEmail")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Contato>>> FiltrarPorEmail(string email)
        {
            return await _context.Contato
                .Where(contato => contato.ListaEmails.Any(e => e.EnderecoEmail == email))
                .Include(contato => contato.ListaEmails)
                .ToListAsync();
        }

        /// <summary>
        /// Adiciona um novo contato.
        /// </summary>
        /// <param name="contatoRequest">O novo contato a ser adicionado.</param>
        /// <returns>O novo contato adicionado.</returns>
        /// <response code="201">O contato foi adicionado com sucesso.</response>
        [HttpPost]
        [ProducesResponseType(201)]
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

        /// <summary>
        /// Atualiza um contato com o ID especificado.
        /// </summary>
        /// <param name="id">O ID do contato a ser atualizado.</param>
        /// <param name="contato">O novo de nome, empresa, lista de emails, telefone pessoal e/ou comercial do contato.</param>
        /// <returns>O contato atualizado.</returns>
        /// <response code="200">O contato foi atualizado com sucesso.</response>
        /// <response code="404">O contato com o ID fornecido não foi encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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
                return NotFound("Contato não encontrado pelo ID: " + id + ". Tente novamente.");
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

        /// <summary>
        /// Deleta um contato com o ID especificado.
        /// </summary>
        /// <param name="id">O ID do contato a ser deletado.</param>
        /// <returns>Nenhum conteúdo.</returns>
        /// <response code="204">O contato foi deletado com sucesso.</response>
        /// <response code="404">O contato com o ID fornecido não foi encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
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
