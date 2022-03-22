using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TarefasBackEnd.Models;

namespace TarefasBackEnd.Repositories
{
    public interface ITarefaRepository
    {
        List<Tarefa> Read(Guid usuarioId);
        void Create(Tarefa tarefa);
        void Delete(Guid id);
        void Update(Guid id, Tarefa tarefa);
    }
    public class TarefaRepository : ITarefaRepository
    {
        private readonly DataContext _context;

        public TarefaRepository(DataContext context)
        {
            _context = context;
        }

        public void Create(Tarefa tarefa)
        {
            tarefa.Id = Guid.NewGuid();
            _context.Add(tarefa);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var tarefa = _context.Tarefas.Find(id);
            _context.Entry(tarefa).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public List<Tarefa> Read(Guid usuarioId)
        {
            return _context.Tarefas.Where(u => u.UsuarioId == usuarioId).ToList();
        }

        public void Update(Guid id, Tarefa tarefa)
        {
            var entity = _context.Tarefas.Find(id);

            entity.Nome = tarefa.Nome;
            entity.Concluida = tarefa.Concluida;

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
