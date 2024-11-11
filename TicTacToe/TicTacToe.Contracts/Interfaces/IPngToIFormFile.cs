using Microsoft.AspNetCore.Http;

namespace TicTacToe.Contracts.Interfaces;

public interface IPngToIFormFile
{
    public IFormFile ConvertPngToIFormFile(string filePath);
}