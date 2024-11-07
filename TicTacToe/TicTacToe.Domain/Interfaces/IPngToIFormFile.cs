using Microsoft.AspNetCore.Http;

namespace TicTacToe.Domain.Interfaces;

public interface IPngToIFormFile
{
    public IFormFile ConvertPngToIFormFile(string filePath);
}