using PrimeiroApp.Models;

namespace PrimeiroApp.Repositories.Contracts
{
    public interface IUsuarioRepository
    {
        IEnumerable<Usuario> Cadastro();
        void Cadastrar(Usuario usuario);
        void Atualizar(Usuario usuario);
        Usuario ObterUsuario(int id);
        void Excluir(int id);
    }
}
