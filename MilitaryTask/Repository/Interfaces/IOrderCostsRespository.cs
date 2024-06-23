﻿using CSharpFunctionalExtensions;
using MilitaryTask.Model;

namespace MilitaryTask.Repository.Interfaces
{
    public interface IOrderCostsRespository
    {
        Task<Result> SaveOrderCostsAsync(List<Order> orders);
    }
}