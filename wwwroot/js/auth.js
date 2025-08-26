const cadastroContainer = document.getElementById('cadastro-container');
const loginContainer = document.getElementById('login-container');

function showLogin() {
    cadastroContainer.classList.add('hidden');
    loginContainer.classList.remove('hidden');
}

function showCadastro() {
    loginContainer.classList.add('hidden');
    cadastroContainer.classList.remove('hidden');
}