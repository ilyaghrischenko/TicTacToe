using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TicTacToe.DTO.Responses;

namespace TicTacToe.Contracts.Controllers;

public interface IBaseControllerService
{
    public void GetErrors(ModelStateDictionary modelState)
    {
        if (modelState.IsValid is false)
        {
            var errors = modelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new ValidationErrorResponse
                {
                    Field = x.Key,
                    Errors = x.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                })
                .ToList();
            
            throw new ArgumentException(errors.ToString());
        }
    }
}