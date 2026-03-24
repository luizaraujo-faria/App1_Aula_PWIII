using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using PrimeiroApp.Models;
using PrimeiroApp.Repositories.Contracts;
using System.Data;

namespace PrimeiroApp.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _conexaoMySql;
        public UsuarioRepository(IConfiguration conf)
        {
            _conexaoMySql = conf.GetConnectionString("ConexaoMySql");
        }

        public void Atualizar(Usuario usuario)
        {
            using (var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("Update usuario set nomeUsu=@nomeUsu, Cargo=@Cargo," +
                                                    "DataNasc=@DataNasc Where IdUsu=@IdUsu", conexao);

                cmd.Parameters.Add("@nomeUsu", MySqlDbType.VarChar).Value = usuario.nomeUsu;
                cmd.Parameters.Add("@Cargo", MySqlDbType.VarChar).Value = usuario.Cargo;
                cmd.Parameters.Add("@DataNasc", MySqlDbType.Date).Value = usuario.DataNasc;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Cadastrar(Usuario usuario)
        {
            using( var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("insert into tbUsuario(nomeUsu, Cargo, DataNasc)" +
                                                " values (@nomeUsu, @Cargo, @DataNasc )", conexao);

                cmd.Parameters.Add("@nomeUsu", MySqlDbType.VarChar).Value = usuario.nomeUsu;
                cmd.Parameters.Add("@Cargo", MySqlDbType.VarChar).Value = usuario.Cargo;
                cmd.Parameters.Add("@DataNasc", MySqlDbType.Date).Value = usuario.DataNasc;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public IEnumerable<Usuario> ObterUsuarios()
        {
            List<Usuario> UsuarioList = new List<Usuario>();
            using(var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("select * from tbUsuario", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                conexao.Clone();

                foreach (DataRow dr in dt.Rows)
                {
                    UsuarioList.Add(
                        new Usuario
                        {
                            idUsu = Convert.ToInt32(dr["IdUsu"]),
                            nomeUsu = (string)dr["nomeUsu"],
                            Cargo = (string)dr["Cargo"],
                            DataNasc = DateOnly.FromDateTime(Convert.ToDateTime(dr["DataNasc"]))
                        }
                    );
                }

                return UsuarioList;
            }
        }

        public void Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public Usuario ObterUsuario(int id)
        {
            using( var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from tbUsuario where idUsu=@idUsu", conexao);

                cmd.Parameters.AddWithValue("@idUsu", id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Usuario usuario = new Usuario();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    usuario.idUsu = Convert.ToInt32(dr["idUsu"]);
                    usuario.nomeUsu = (string)(dr["nomeUsu"]);
                    usuario.Cargo = (string)(dr["Cargo"]);
                    usuario.DataNasc = DateOnly.FromDateTime(Convert.ToDateTime(dr["DataNasc"]));
                }

                return usuario;
            }
        }
    }
}
