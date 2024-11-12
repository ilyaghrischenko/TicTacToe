using Microsoft.AspNetCore.Http;

namespace TicTacToe.Contracts;

public interface IPngToIFormFile
{
    public IFormFile ConvertPngToIFormFile(string filePath);
}