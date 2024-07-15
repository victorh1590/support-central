﻿using Dapper;
using ProjectMarket.Server.Data.Model.VO;

namespace ProjectMarket.Server.Infra.Repository;

public class PaymentFrequencyRepository(IUnitOfWork uow)
{
    private readonly IUnitOfWork _uow = uow;

    public IEnumerable<PaymentFrequencyVO> GetAll()
    {
        // TODO Use pagination instead.
        string query = "SELECT PaymentFrequencyName, Suffix FROM PaymentFrequency";
        return _uow.Connection.Query<PaymentFrequencyVO>(query);
    }

    public PaymentFrequencyVO? GetByPaymentFrequencyName(string name)
    {
        string query = "SELECT PaymentFrequencyName, Suffix FROM PaymentFrequency WHERE PaymentFrequencyName = @PaymentFrequencyName";
        return _uow.Connection.QueryFirstOrDefault<PaymentFrequencyVO>(query, new { PaymentFrequencyName = name });
    }

    public void Insert(PaymentFrequencyVO PaymentFrequency)
    {
        string query = "INSERT INTO PaymentFrequency (Description, Suffix) VALUES (@Description, @Suffix)";
        _uow.Connection.Execute(query, PaymentFrequency);
    }

    public void Update(PaymentFrequencyVO PaymentFrequency)
    {
        string query = 
            "UPDATE PaymentFrequency " +
            "SET Description = @Description, Suffix = @Suffix " +
            "WHERE PaymentFrequencyName = @PaymentFrequencyName";
        _uow.Connection.Execute(query, PaymentFrequency);
    }

    public void Delete(string name)
    {
        string query = "DELETE CASCADE FROM PaymentFrequency WHERE PaymentFrequencyName = @PaymentFrequencyName";
        _uow.Connection.Execute(query, new { PaymentFrequencyNamePaymentFrequencyName = name });
    }
}