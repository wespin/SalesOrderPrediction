﻿namespace WebApi.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkAdapter:IDisposable
    {
        IUnitOfWorkRepository Repositories { get; }
        void SaveChanges();
    }
}
