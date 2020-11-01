using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace WhatSappDatabase.MongoDB.Base
{


    /// <summary>
    /// Responsavel para lidar com as entidades do MongoDB
    /// </summary>
    /// <typeparam name="T">Informe o tipo da classe</typeparam>
    public class ServiceMongo<T> : IDisposable where T : class, new()
    {
        /// <summary>
        /// Propriedade que está a conexão com o banco de dados.
        /// </summary>
        private static string StrConexao
        {
            get
            {
                return "mongodb://localhost:27017/WhatSaap";
            }
        }

        /// <summary>
        /// Propiedade que inicia a conexão com o banco de dados
        /// </summary>
        public IMongoClient server = new MongoClient(StrConexao);

        /// <summary>
        /// Propriedade que guarda o nome da coleçãp.
        /// </summary>
        private string nomeDaColecao;

        /// <summary>
        /// Variavel que representa um banco de dados no MongoDB.
        /// </summary>
        private IMongoDatabase db;

        /// <summary>
        /// Retorna o nome da coleção
        /// </summary>
        public IMongoCollection<T> Colecao
        {
            get
            {
                return db.GetCollection<T>(nomeDaColecao);
            }
            set
            {
                Colecao = value;
            }
        }

        public IClientSessionHandle sessao;

        /// <summary>
        /// Retorna o valor sempre atualizado
        /// </summary>
        public FindOneAndUpdateOptions<T> opcaoPadrao
        {
            get
            {
                return new FindOneAndUpdateOptions<T>()
                {
                    ReturnDocument = ReturnDocument.After
                };
            }
            set
            {
                opcaoPadrao = value;
            }
        }

        /// <summary>
        /// Conecta com o banco de dados.
        /// </summary>
        public ServiceMongo()
        {
            try
            {
                nomeDaColecao = PegarNomeColecao();
                sessao = server.StartSession();
                db = sessao.Client.GetDatabase(MongoUrl.Create(StrConexao).DatabaseName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Conecta com o banco de dados com a sessão informada.
        /// </summary>
        public ServiceMongo(IClientSessionHandle sessao)
        {
            try
            {
                nomeDaColecao = PegarNomeColecao();
                this.sessao = sessao;
                db = this.sessao.Client.GetDatabase(MongoUrl.Create(StrConexao).DatabaseName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Pega o nome da coleção de acordo com o informado na classe
        /// </summary>
        /// <returns>Nome da coleção</returns>
        private static string PegarNomeColecao()
        {
            return (typeof(T)?.GetCustomAttributes(typeof(NomeDaColecaoAttribute), true)?.FirstOrDefault()
                as NomeDaColecaoAttribute).NomeDaColecao;
        }

        /// <summary>
        /// Cria um filtro de acordo com o objeto informado.
        /// </summary>
        /// <param name="registro">Infrome o objeto.</param>
        /// <returns>Retorna um filtro exatamente igual ao objeto.</returns>
        public virtual Expression<Func<T, bool>> CriarFiltro(object registro)
        {
            try
            {
                Type type = registro.GetType();
                PropertyInfo[] props = type.GetProperties();
                var entity = Expression.Parameter(type, type.Name);
                BinaryExpression velho = null;
                BinaryExpression lambda = null;
                for (int i = 0; i < props.Count(); i++)
                {
                    var prop = props[i];
                    Type propType = prop.PropertyType;
                    var nomeProp = prop.Name;
                    var valorProp = prop.GetValue(registro);
                    lambda = Expression.Equal(
                              Expression.Property(entity, nomeProp),
                              Expression.Constant(valorProp, propType));

                    if (velho != null)
                    {
                        velho = Expression.AndAlso(velho, lambda);
                        continue;
                    }
                    velho = lambda;
                }

                return Expression.Lambda<Func<T, bool>>(velho, entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel criar o filtro!", ex);
            }
        }

        /// <summary>
        ///  Cria um filtro do Id do objeto informado.
        /// </summary>
        /// <param name="registro">Infrome o objeto.</param>
        /// <returns>Retorna um filtro do Id ao objeto.</returns>
        public virtual Expression<Func<T, bool>> CriarFiltroId(object registro)
        {
            try
            {
                Type type = registro.GetType();
                PropertyInfo[] props = type.GetProperties();
                var prop = props.Where(x => x.Name == "Id").FirstOrDefault();

                if (prop.GetValue(registro) is null)
                {
                    //"Objeto não possui Id como propriedade!"
                    return null;
                }

                var entity = Expression.Parameter(type, type.Name);
                BinaryExpression lambda = null;

                Type propType = prop.PropertyType;
                var nomeProp = prop.Name;
                var valorProp = prop.GetValue(registro);
                lambda = Expression.Equal(
                          Expression.Property(entity, nomeProp),
                          Expression.Constant(valorProp, propType));

                return Expression.Lambda<Func<T, bool>>(lambda, entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel criar o filtro!", ex);
            }
        }

        public void CriarEdicao(object registroOriginal, object registroNovo)
        {
            try
            {
                Type typeRegistroOriginal = registroOriginal.GetType();
                PropertyInfo[] propsOriginal = typeRegistroOriginal.GetProperties();

                Type typeRegistroNovo = registroNovo.GetType();
                PropertyInfo[] propsNovo = typeRegistroNovo.GetProperties();

                var novoObjeto = propsOriginal.Where(x => propsNovo.Contains(x)).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel criar o filtro!", ex);
            }
        }

        /// <summary>
        /// Recupera a lista de todos os registros da tabela.
        /// </summary>
        /// <returns>Lista do tipo IMongoQueryable com dados da tabelaa.</returns>
        public virtual IMongoQueryable<T> Listar()
        {
            try
            {
                return Colecao.AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Recupera a lista dos registros da tabela de acordo com filtro.
        /// </summary>
        /// <param name="filtro">Expressão LAMBDA.</param>
        /// <returns>Lista do tipo IMongoQueryable com dados da tabela de acordo com filtro informado.</returns>
        public virtual IMongoQueryable<T> Listar(Expression<Func<T, bool>> filtro)
        {
            try
            {
                return Colecao.AsQueryable().Where(filtro);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista assíncrono todos os registros da tabela assincronos.
        /// </summary>
        /// <returns>Lista os dados da coleção.</returns>
        public virtual async Task<List<T>> ListarAsync()
        {
            try
            {
                return await Colecao.AsQueryable().ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista assíncrono todos os registros da tabela assincronos.
        /// </summary>
        /// <param name="filtro">Expressão LAMBDA.</param>
        /// <returns>Lista os dados da coleção de acordo com filtro informado.</returns>
        public virtual async Task<List<T>> ListarAsync(Expression<Func<T, bool>> filtro)
        {
            try
            {
                return await Colecao.Find(sessao, filtro).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Busca o registro identico ao objeto informado.
        /// </summary>
        /// <param name="registro">Informe o objeto.</param>
        /// <returns>Retorna o primeiro registro encontrado.</returns>
        public virtual T Buscar(T registro)
        {
            try
            {
                return Colecao.Find(sessao, CriarFiltro(registro)).FirstOrDefault();
            }
            catch (Exception)
            {
                sessao.AbortTransaction();
                throw;
            }
        }

        /// <summary>
        /// Busca o registro assíncrono identico ao objeto informado.
        /// </summary>
        /// <param name="registro">Informe o objeto.</param>
        /// <returns>Retorna o primeiro registro encontrado.</returns>
        public virtual async Task<T> BuscarAsync(T registro)
        {
            try
            {
                return await Colecao.Find(sessao, CriarFiltro(registro)).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Busca o registro assincrono identico ao objeto informado.
        /// </summary>
        /// <param name="filtro">Expressão LAMBDA.</param>
        /// <returns>Retorna o primeiro registro encontrado.</returns>
        public virtual T Buscar(Expression<Func<T, bool>> filtro)
        {
            try
            {
                return Colecao.Find(sessao, filtro).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Busca assíncrono o registro identico ao objeto informado.
        /// </summary>
        /// <param name="filtro">Expressão LAMBDA.</param>
        /// <returns>Retorna o primeiro registro encontrado.</returns>
        public virtual async Task<T> BuscarAsync(Expression<Func<T, bool>> filtro)
        {
            try
            {
                return await Colecao.Find(sessao, filtro).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Altera o registro na base de dados.
        /// </summary>
        /// <param name="filtro">Expressão LAMBDA.</param>
        /// <param name="registro">Informe o objeto.</param>
        /// <param name="opcao">Opções para um comando findAndModify para atualizar um objeto.</param>
        /// <returns>Retorna o registro atualizado.</returns>
        public virtual T Alterar(Expression<Func<T, bool>> filtro, UpdateDefinition<T> registro, FindOneAndUpdateOptions<T> opcao = null)
        {
            try
            {
                Buscar(filtro);
                return Colecao.FindOneAndUpdate(sessao, filtro, registro, opcao == null ? opcaoPadrao : opcao);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Altera assíncrono o registro na base de dados.
        /// </summary>
        /// <param name="filtro">Expressão LAMBDA.</param>
        /// <param name="registro">Informe o objeto.</param>
        /// <param name="opcao">Opções para um comando findAndModify para atualizar um objeto.</param>
        /// <returns>Retorna o registro atualizado.</returns>
        public virtual async Task<T> AlterarAsync(Expression<Func<T, bool>> filtro, UpdateDefinition<T> registro, FindOneAndUpdateOptions<T> opcao = null)
        {
            try
            {
                Buscar(filtro);
                return await Colecao.FindOneAndUpdateAsync(filtro, registro, opcao);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Altera o registro na base de dados.
        /// </summary>
        /// <param name="filtro">Expressão LAMBDA.</param>
        /// <param name="registro">Informe o objeto.</param>
        /// <returns>Retorna o registro atualizado.</returns>
        public virtual T Alterar(Expression<Func<T, bool>> filtro, UpdateDefinition<T> registro)
        {
            try
            {
                Buscar(filtro);
                return Colecao.FindOneAndUpdate(sessao, filtro, registro, opcaoPadrao);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Altera assíncrono o registro na base de dados.
        /// </summary>
        /// <param name="filtro">Expressão LAMBDA.</param>
        /// <param name="registro">Informe o objeto.</param>
        /// <returns>Retorna o registro atualizado.</returns>
        public virtual async Task<T> AlterarAsync(Expression<Func<T, bool>> filtro, UpdateDefinition<T> registro)
        {
            try
            {
                await BuscarAsync(filtro);
                return await Colecao.FindOneAndUpdateAsync(sessao, filtro, registro, opcaoPadrao);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Altera o registro na base de dados.
        /// </summary>
        /// <param name="filtro">Expressão LAMBDA.</param>
        /// <param name="registro">Informe o objeto.</param>
        /// <param name="opcao">Opções para um comando findAndModify para atualizar um objeto.</param>
        /// <returns>Retorna o registro atualizado.</returns>
        public virtual T Alterar(Expression<Func<T, bool>> filtro, T registro, FindOneAndUpdateOptions<T> opcao)
        {
            try
            {
                Buscar(filtro);
                var novo = registro.ToJson();
                return Colecao.FindOneAndUpdate(sessao, filtro, novo, opcao == null ? opcaoPadrao : opcao);
            }
            catch (Exception)
            {
                sessao.AbortTransaction();
                throw;
            }
        }

        /// <summary>
        /// Altera assíncrono o registro na base de dados.
        /// </summary>
        /// <param name="filtro">Expressão LAMBDA.</param>
        /// <param name="registro">Informe o objeto.</param>
        /// <param name="opcao">Opções para um comando findAndModify para atualizar um objeto.</param>
        /// <returns>Retorna o registro atualizado.</returns>
        public virtual async Task<T> AlterarAsync(Expression<Func<T, bool>> filtro, T registro, FindOneAndUpdateOptions<T> opcao)
        {
            try
            {
                Buscar(filtro);
                var novo = registro.ToJson();
                return await Colecao.FindOneAndUpdateAsync(sessao, filtro, novo, opcao);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Altera o registro na base de dados.
        /// </summary>
        /// <param name="filtro">Expressão LAMBDA.</param>
        /// <param name="registro">Informe o objeto.</param>
        /// <returns>Retorna o registro atualizado.</returns>
        public virtual T Alterar(Expression<Func<T, bool>> filtro, T registro)
        {
            try
            {
                Buscar(filtro);
                var novo = registro.ToJson();
                return Colecao.FindOneAndUpdate(sessao, filtro, novo, opcaoPadrao);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Altera assíncrono o registro na base de dados.
        /// </summary>
        /// <param name="filtro">Expressão LAMBDA.</param>
        /// <param name="registro">Informe o objeto.</param>
        /// <returns>Retorna o registro atualizado.</returns>
        public virtual async Task<T> AlterarAsync(Expression<Func<T, bool>> filtro, T registro)
        {
            try
            {
                await BuscarAsync(filtro);
                var novo = registro.ToJson();

                return await Colecao.FindOneAndUpdateAsync(sessao, filtro, novo, opcaoPadrao);

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Exclui o primeiro registro encontrado igual ao registro informado da base de dados.
        /// </summary>
        /// <param name="registro">Informe o objeto.</param>
        public virtual void Excluir(T registro)
        {
            try
            {
                Buscar(registro);
                Colecao.DeleteOne(sessao, CriarFiltro(registro));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Exclui registro da base de dados.
        /// </summary>
        /// <param name="filtro">Expressão LAMBDA.</param>
        public virtual void Excluir(Expression<Func<T, bool>> filtro)
        {
            try
            {
                Buscar(filtro);
                Colecao.DeleteOne(sessao, filtro);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Exclui registro assíncrono da base de dados.
        /// </summary>
        /// <param name="filtro">Expressão LAMBDA.</param>
        public virtual async void ExcluirAsync(Expression<Func<T, bool>> filtro)
        {
            try
            {
                await BuscarAsync(filtro);
                await Colecao.DeleteOneAsync(sessao, filtro);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Insere uma lista.
        /// </summary>
        /// <param name="lstRegistro">Lista de registros.</param>
        public virtual void Inserir(IEnumerable<T> lstRegistro)
        {
            try
            {
                Colecao.InsertMany(sessao, lstRegistro);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Insere uma lista assíncrona.
        /// </summary>
        /// <param name="lstRegistro">Lista de registros.</param>
        public virtual async void InserirAsync(IEnumerable<T> lstRegistro)
        {
            try
            {
                await Colecao.InsertManyAsync(sessao, lstRegistro);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Insere um registro na base de dados.
        /// </summary>
        /// <param name="registro">Informe o objeto.</param>
        /// <returns>Retorna o registro inserido.</returns>
        public virtual T Inserir(T registro)
        {
            try
            {
                if (Buscar(registro) != null)
                {
                    throw new Exception("O registro já existe na base de dados");
                }

                Colecao.InsertOne(sessao, registro);
                return registro;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Insere um registro assíncrono na base de dados.
        /// </summary>
        /// <param name="registro">Informe o objeto.</param>
        /// <returns>Retorna o registro inserido.</returns>
        public virtual async Task<T> InserirAsync(T registro)
        {
            try
            {
                if (await BuscarAsync(registro) != null)
                {
                    new Exception("O registro já existe na base de dados");
                }

                await Colecao.InsertOneAsync(sessao, registro);
                return registro;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Insere ou atualiza um registro na base de dados.
        /// </summary>
        /// <param name="filtro">Expressão LAMBDA.</param>
        /// <param name="registro">Retorna o registro.</param>
        /// <returns></returns>
        public virtual async Task<T> GravarAsync(T registro)
        {
            try
            {
                var filtro = CriarFiltroId(registro);


                if (filtro == null)
                {
                    return await InserirAsync(registro);
                }
                else
                {
                    return await AlterarAsync(filtro, registro);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Insere ou atualiza um registro na base de dados.
        /// </summary>
        /// <param name="filtro">Expressão LAMBDA.</param>
        /// <param name="registro">Retorna o registro inserido.</param>
        /// <returns></returns>
        public virtual T Gravar(T registro)
        {
            try
            {

                var filtro = CriarFiltroId(registro);
                if (filtro == null)
                {
                    return Inserir(registro);
                }
                else
                {
                    return Alterar(filtro, registro);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
        }
    }
}
