﻿namespace WebApi.Repository.Interfaces.Actions
{
    public interface IRemoveRepository<T>
    {
        void Remove(T id);
    }
}
