using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using visual_studio_psicanalista.Models;  
using visual_studio_psicanalista.ORM;
namespace visual_studio_psicanalista.Models
{
    public class ServicoRepositorio
    {
        private readonly AndrezaMeloNeuropsicanalistaContext _context;

        // Construtor, injetando o DbContext
        public ServicoRepositorio(AndrezaMeloNeuropsicanalistaContext context)
        {
            _context = context;
        }

        // Criar um novo serviço
        public bool InserirServico(string tipoServico, decimal valor)
        {
            try
            {
                // Criando uma nova instância de TbServico
                TbServico servico = new TbServico
                {
                    TipoServico = tipoServico,
                    Valor = valor
                };

                // Adicionando o serviço no contexto e salvando as mudanças
                _context.TbServicos.Add(servico);
                _context.SaveChanges();

                return true;  // Retorna true se a inserção foi bem-sucedida
            }
            catch (Exception ex)
            {
                // Log de erro ou tratamento personalizado
                Console.WriteLine($"Erro ao inserir serviço: {ex.Message}");
                return false;  // Retorna false se houver falha
            }
        }

        // Listar todos os serviços
        public List<ServicoVM> ListarServicos()
        {
            try
            {
                // Mapeando cada TbServico para um ServicoVM
                var servicosVM = _context.TbServicos
                    .Select(s => new ServicoVM
                    {
                        
                        Id = s.Id,               
                        Valor= s.Valor,           
                        TipoServico = s.TipoServico 
                                                
                    })
                    .ToList();

                return servicosVM;  // Retorna a lista de ServicoVM
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar serviços: {ex.Message}");
                return new List<ServicoVM>();  // Retorna uma lista vazia de ServicoVM em caso de erro
            }
        }


        // Obter um serviço pelo ID
        public TbServico ObterServicoPorId(int id)
        {
            try
            {
                // Retorna o serviço com o ID especificado
                return _context.TbServicos.FirstOrDefault(s => s.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar o serviço com ID {id}: {ex.Message}");
                return null;  // Retorna null se não encontrar ou ocorrer erro
            }
        }

        // Atualizar um serviço existente
        public bool AtualizarServico(int id, string tipoServico, decimal valor)
        {
            try
            {
                // Buscando o serviço pelo ID
                var servico = _context.TbServicos.FirstOrDefault(s => s.Id == id);
                if (servico != null)
                {
                    // Atualizando os dados do serviço
                    servico.TipoServico = tipoServico;
                    servico.Valor = valor;

                    // Salvando as mudanças no banco de dados
                    _context.SaveChanges();

                    return true;  // Retorna true se a atualização for bem-sucedida
                }
                else
                {
                    return false;  // Retorna false se o serviço não for encontrado
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar o serviço com ID {id}: {ex.Message}");
                return false;  // Retorna false se houver erro durante a atualização
            }
        }

        // Excluir um serviço pelo ID
        public bool ExcluirServico(int id)
        {
            try
            {
                // Buscando o serviço pelo ID
                var servico = _context.TbServicos.FirstOrDefault(s => s.Id == id);
                if (servico != null)
                {
                    // Removendo o serviço do banco de dados
                    _context.TbServicos.Remove(servico);
                    _context.SaveChanges();  // Persistindo as mudanças no banco

                    return true;  // Retorna true se a exclusão for bem-sucedida
                }
                else
                {
                    return false;  // Retorna false se o serviço não for encontrado
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir o serviço com ID {id}: {ex.Message}");
                return false;  // Retorna false se houver erro durante a exclusão
            }
        }

        public List<ServicoVM> ListarNomesServicos()
        {
            // Recupera os serviços com filtragem e projeção para ServicoVM diretamente no banco de dados
            var query = _context.TbServicos.ToList();

            // Projeta diretamente para ServicoVM e retorna como lista
            var listServicos = _context.TbServicos
                .Select(s => new ServicoVM
                {
                    Id = s.Id,
                    TipoServico = s.TipoServico,
                })
                .ToList();

            return listServicos;
        }
    }
}

