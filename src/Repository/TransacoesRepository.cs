using OFX.Context;
using OFX.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OFX.Repository
{
    public class TransacoesRepository : ITransacoesRepository
    {
        private readonly AppDbContext _dbContext;
        
        public TransacoesRepository(AppDbContext context)
        {
            _dbContext = context;
        }
        public void Insert(Transacoes transacao)
        {
            _dbContext.Transacoes.Add(transacao);
            _dbContext.SaveChanges();
        }
        public void Insert(List<Transacoes> listTransacao)
        {
            _dbContext.Transacoes.AddRange(listTransacao);
            _dbContext.SaveChanges();
        }
        public void Update(int id, string note)
        {
            Transacoes context = _dbContext.Transacoes.First(p => p.Id == id);
            context.Note = note;
            _dbContext.SaveChanges();
        }
        public List<Transacoes> GetAll()
        {
            return _dbContext.Transacoes.ToList();
        }
        public List<Transacoes> GetByData(DateTime dataIni, DateTime dataFim)
        {
            return _dbContext.Transacoes.Where(p => p.Data >= dataIni && p.Data <= dataFim).ToList();
        }

    }
}
