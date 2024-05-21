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
 * Função para limpar o formulário de criação de contato
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
function excluirContato(idContato) {
    if (confirm("Você tem certeza que deseja excluir este contato?")) {
        fetch(`${urlAPI}/${idContato}`, {
            method: 'DELETE'
        })
        .then(response => {
            if (!response.ok) {
                if (response.status === 404) {
                    throw new Error('Contato não encontrado');
                } else {
                    throw new Error('Erro ao excluir o contato');
                }
            }
            return response.text();
        })
        .then(() => {
            alert("Contato excluído com sucesso");
            document.getElementById("modal-listagem-contatos").style.display = 'none';
            listarContatos(); // Recarregar a lista de contatos após a exclusão
        })
        .catch(error => {
            console.error('Erro:', error);
            alert(error.message);
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
                <td>
                    <button onclick="verMais(${contato.id})">Ver mais</button>
                </td>
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
                    <td>${data.listaEmails[0].enderecoEmail}</td>
                    <td>${data.telefonePessoal}</td>
                    <td>${data.telefoneComercial}</td>
                    <td>
                        <button onclick="verMais(${data.id})">Ver mais</button>
                    </td>
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

/**
* Função para visualizar mais detalhes do contato
*/
function verMais(id) {
    document.getElementById("modal-listagem-contatos").style.display = 'block';

    fetch(`${urlAPI}/${id}`, {
        method: 'GET'
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Contato não encontrado');
            }
            return response.json();
        })
        .then(data => {
            console.log(data);

            const detalhesContato = document.querySelector('.modal-listagem-contatos-content');
            detalhesContato.innerHTML = '';

            /// Cria o botão de fechar a modal
            const closeButton = document.createElement('span');
            closeButton.className = 'close-button';
            closeButton.innerHTML = '&times;';
            closeButton.onclick = () => {
                document.getElementById("modal-listagem-contatos").style.display = 'none';
            };
            detalhesContato.appendChild(closeButton);

            // Cria um container para as informações básicas do contato
            const tituloInfo = document.createElement('h2');
            tituloInfo.textContent = 'Informações do contato:';
            detalhesContato.appendChild(tituloInfo);

            const infoBasica = document.createElement('div');
            infoBasica.className = 'info-basica';
            infoBasica.innerHTML = `
                <p>ID: ${data.id}</p>
                <p>Nome: ${data.nome}</p>
                <p>Empresa: ${data.empresa}</p>
                <p>Telefone Pessoal: ${data.telefonePessoal}</p>
                <p>Telefone Comercial: ${data.telefoneComercial}</p>
            `;

            detalhesContato.appendChild(infoBasica);

            // Cria uma lista para os e-mails
            const tituloEmails = document.createElement('h2');
            tituloEmails.textContent = 'E-mails do contato:';
            detalhesContato.appendChild(tituloEmails);
            
            const listaEmails = document.createElement('ul');
            listaEmails.className = 'lista-emails';
            
            data.listaEmails.forEach(email => {
                const emailItem = document.createElement('li');
                emailItem.textContent = email.enderecoEmail;
                listaEmails.appendChild(emailItem);
            });

            detalhesContato.appendChild(listaEmails);

            // Adiciona botões de ação na lista de contato
            const botoesAcao = document.createElement('div');
            botoesAcao.innerHTML = `
                <button onclick="editarContato(${data.id})">Editar</button>
                <button onclick="excluirContato(${data.id})">Excluir</button>
            `;

            // Adiciona os botões de ação ao modal
            detalhesContato.appendChild(botoesAcao);

        })
        .catch(error => {
            console.error('Erro:', error);
        });
}

/**
 * Função para filtrar contatos com base nos campos de entrada.
 */
function filtrarContatos() {
    const nome = document.getElementById('filtroNome').value;
    const empresa = document.getElementById('filtroEmpresa').value;
    const telefonePessoal = document.getElementById('filtroTelefonePessoal').value;
    const telefoneComercial = document.getElementById('filtroTelefoneComercial').value;
    const email = document.getElementById('filtroEmail').value;

    const params = new URLSearchParams({
        nome: nome || '',
        empresa: empresa || '',
        telefonePessoal: telefonePessoal || '',
        telefoneComercial: telefoneComercial || '',
        email: email || ''
    });

    fetch(`${urlAPI}/FiltrarContato?${params.toString()}`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Erro ao filtrar contatos');
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
                    <td>
                        <button onclick="verMais(${contato.id})">Ver mais</button>
                    </td>
                `;
                listaContatos.appendChild(row);
            });
        })
        .catch(error => {
            console.error('Erro:', error);
            alert('Erro ao filtrar contatos');
        });
}

/**
 * Função para limpar os campos de filtro de listagem de contato
 */
function limparFiltros() {
    document.getElementById('filtroNome').value = '';
    document.getElementById('filtroEmpresa').value = '';
    document.getElementById('filtroTelefonePessoal').value = '';
    document.getElementById('filtroTelefoneComercial').value = '';
    document.getElementById('filtroEmail').value = '';
    listarContatos(); // Chama a função para listar todos os contatos
}

/**
 * Função para abrir a modal de edição com os dados do contato
 */
function editarContato(id) {
    // Abre a modal de edição
    document.getElementById("modal-editar-contato").style.display = 'block';

    // Preenche os campos da modal de edição com os dados do contato
    fetch(`${urlAPI}/${id}`, {
        method: 'GET'
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Contato não encontrado');
        }
        return response.json();
    })
    .then(data => {
        document.getElementById('editarNome').value = data.nome;
        document.getElementById('editarEmpresa').value = data.empresa;
        document.getElementById('editarEmail').value = data.listaEmails[0].enderecoEmail; // Assumindo que o primeiro email é o principal
        document.getElementById('editarTelefonePessoal').value = data.telefonePessoal;
        document.getElementById('editarTelefoneComercial').value = data.telefoneComercial;

        // Armazena o ID do contato na modal para uso posterior
        document.getElementById('modal-editar-contato').dataset.id = id;
    })
    .catch(error => {
        console.error('Erro:', error);
    });
}

/**
 * Função para fechar a modal de edição
 */
function fecharModalEditar() {
    document.getElementById("modal-editar-contato").style.display = 'none';
}

/**
 * Função para salvar as alterações feitas no contato
 */
function salvarEdicaoContato() {
    const id = document.getElementById('modal-editar-contato').dataset.id;
    const nome = document.getElementById('editarNome').value;
    const empresa = document.getElementById('editarEmpresa').value;
    const email = document.getElementById('editarEmail').value;
    const telefonePessoal = document.getElementById('editarTelefonePessoal').value;
    const telefoneComercial = document.getElementById('editarTelefoneComercial').value;

    const contatoAtualizado = {
        id: id,
        nome: nome,
        empresa: empresa,
        telefonePessoal: telefonePessoal,
        telefoneComercial: telefoneComercial,
        listaEmails: [{ enderecoEmail: email }]
    };

    fetch(`${urlAPI}/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(contatoAtualizado)
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Erro ao atualizar o contato');
        }
        return response.json();
    })
    .then(() => {
        alert('Contato atualizado com sucesso!');
        fecharModalEditar();
        // Atualize a lista de contatos ou recarregue a página, se necessário
    })
    .catch(error => {
        console.error('Erro:', error);
        alert('Erro ao atualizar o contato.');
    });
}