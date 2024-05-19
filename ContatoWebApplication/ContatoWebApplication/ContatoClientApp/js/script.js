const urlAPI = '/api/contato';

/**
 * Função que permite motrar e ocultar seção especificada do HTML.
 * @param {string} sectionId - O ID da seção a ser exibida.
 */
function showSection(sectionId) {
    document.querySelectorAll('.section').forEach(section => {
        section.style.display = 'none';
    });
    document.getElementById(sectionId).style.display = 'block';
}

/**
 * Função para criar o cadastro de contato 
 */
function adicionarContato() {
    const nome = document.getElementById('nome').value;
    const empresa = document.getElementById('empresa').value;
    const telefonePessoal = document.getElementById('telefonePessoal').value;
    const telefoneComercial = document.getElementById('telefoneComercial').value;
    const email = document.getElementById('email').value;

    const novoContato = {
        nome: nome,
        empresa: empresa,
        telefonePessoal: telefonePessoal,
        telefoneComercial: telefoneComercial,
        listaEmails: [{ enderecoEmail: email }]
    };

    fetch(`${urlAPI}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(novoContato)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Erro ao adicionar contato');
            }
            return response.json();
        })
        .then(data => {
            alert("Contato adicionado com sucesso!");
            console.log(data); // opcional: exibe o novo contato adicionado
            limparFormulario();
            listarContatos(); // Atualiza a lista de contatos após adicionar
        })
        .catch(error => {
            console.error('Erro:', error);
            alert('Erro ao adicionar contato');
        });
}

/**
 * Função para limpar o formulário de criação de contato.
 */
function limparFormulario() {
    document.getElementById('nome').value = '';
    document.getElementById('empresa').value = '';
    document.getElementById('telefonePessoal').value = '';
    document.getElementById('telefoneComercial').value = '';
    document.getElementById('email').value = '';
}

/**
 * Função para excluir o contato de acordo com o ID fornecido.
 */
function excluirContato() {
    const id = document.getElementById('idExcluir').value;

    if (!id) {
        alert("Por favor, insira o ID do contato a ser excluído.");
        return;
    }

    if (confirm("Você tem certeza que deseja excluir este contato?")) {
        fetch(`${urlAPI}/${id}`, {
            method: 'DELETE'
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Erro ao excluir contato');
                }
                return response.json();
            })
            .then(data => {
                alert("Contato excluído com sucesso!");
                listarContatos(); // Atualiza a lista de contatos após a exclusão
            })
            .catch(error => {
                console.error('Erro:', error);
                alert('Erro ao excluir contato');
            });
    }
}

/**
* Função para listar todos os contatos cadastrados.
*/
function listarContatos() {
    fetch(`${urlAPI}`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Erro ao listar contatos');
            }
            return response.json();
        })
        .then(data => {
            const listaContatos = document.getElementById('listaContatos');
            listaContatos.innerHTML = '';

            data.forEach(contato => {
                const row = document.createElement('tr');
                row.innerHTML = `
                <td>${contato.id}</td>
                <td>${contato.nome}</td>
                <td>${contato.empresa}</td>
                <td>${contato.listaEmails[0].enderecoEmail}</td>
                <td>${contato.telefonePessoal}</td>
                <td>${contato.telefoneComercial}</td>
            `;
                listaContatos.appendChild(row);
            });
        })
        .catch(error => {
            console.error('Erro:', error);
            alert('Erro ao listar contatos');
        });
}

/**
 * Carregar a lista de contatos ao carregar a página
 */
document.addEventListener('DOMContentLoaded', listarContatos);

/**
 * Função para pesquisar um contato de acordo com o ID fornecido.
 */
function pesquisarContatoPorId() {
    const id = document.getElementById('idPesquisar').value;

    if (!id) {
        alert("Por favor, insira o ID do contato.");
        return;
    }

    fetch(`${urlAPI}/${id}`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Contato não encontrado');
            }
            return response.json();
        })
        .then(data => {
            const resultadoPesquisaId = document.getElementById('resultadoPesquisaId');
            resultadoPesquisaId.innerHTML = `
                <tr>
                    <td>${data.id}</td>
                    <td>${data.nome}</td>
                    <td>${data.empresa}</td>
                    <td>${data.email}</td>
                    <td>${data.telefonePessoal}</td>
                    <td>${data.telefoneComercial}</td>
                </tr>
            `;
        })
        .catch(error => {
            console.error('Erro:', error);
            alert('Erro ao pesquisar contato');
            const resultadoPesquisaId = document.getElementById('resultadoPesquisaId');
            resultadoPesquisaId.innerHTML = ''; // Limpa o resultado anterior, se houver
        });
}