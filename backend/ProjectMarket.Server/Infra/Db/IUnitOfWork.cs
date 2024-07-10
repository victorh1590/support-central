﻿using System.Data;

namespace ProjectMarket.Server;

public interface IUnitOfWork
{
    public IDbConnection Connection { get; }
    public void Begin();
    public void Commit();
    public void Rollback();
    public void Dispose();

}