﻿namespace Cadastro.Models
{
    public class UsuarioModel //Modelo das contas, por não ter um banco de dados, acabei não colocando o ID e optando apenas por deixar
                              //o email como chave primaria
    {
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;
    }
}
