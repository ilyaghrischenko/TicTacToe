using System;
using System.Linq;
using TicTacToe.DTO.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TicTacToe.Contracts.Interfaces.Controllers;

public interface IBaseControllerService
{
    public void GetErrors(ModelStateDictionary modelState)
    {
        if (modelState.IsValid is false)
        {
            var errors = modelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new ValidationError
                {
                    Field = x.Key,
                    Errors = x.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                })
                .ToList();
            throw new ArgumentException(errors.ToString());
        }
    }
}