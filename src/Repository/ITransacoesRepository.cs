using System;
using System.Collections.Generic;
using OFX.Entities;

namespace OFX.Repository
{
    public interface ITransacoesRepository
    {
        void Insert(Transacoes transacao);
        void Insert(List<Transacoes> transacao);
        void Update(int id, string note);
        List<Transacoes> GetAll();
        List<Transacoes> GetByData(DateTime dataIni, DateTime dataFim);
    }
}
