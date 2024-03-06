document.getElementById("FormLogin").onsubmit = ValidarFormLogin;
//Form para fazer a validação no client side
function ValidarForm() {
    var nome = document.getElementById("Nome").value;
    var email = document.getElementById("Email").value;
    var senha = document.getElementById("Senha").value;

    if (nome == "" || email == "" || senha == "") {
        alert("Por favor, insira todas as informações!");
        return false;
    }
    if (!ChecarNomeCompleto(nome)) {
        alert("Por favor, insira o seu nome completo!");
        return false;
    }
    if (!ChecarEmail(email)) {
        alert("Por favor, insira um email válido!");
        return false;
    }
    if (senha.trim().length < 6) {
        alert("Por favor, insira uma senha com mais de 6 caractéres!");
        return false;
    }
    return true;
}

function ValidarFormLogin() {
    var email = document.getElementById("Email").value;
    var senha = document.getElementById("Senha").value;

    if (email == "" || senha == "") {
        alert("Por favor, insira todas as informações!");
        return false;
    }
    if (!ChecarEmail(email)) {
        alert("Por favor, insira um email válido!");
        return false;
    }
    if (senha.trim().length < 6) {
        alert("Por favor, insira uma senha com mais de 6 caractéres!");
        return false;
    }
    return true;
}

function ChecarNomeCompleto(nome) {
    var nomeCompleto = nome.trim().split(/\s+/);

    return nomeCompleto.length >= 2;
}

function ChecarEmail(email) {
    var emailPadrao = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    return emailPadrao.test(email);
}
