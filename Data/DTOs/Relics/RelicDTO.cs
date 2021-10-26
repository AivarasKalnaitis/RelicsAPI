using System.Collections.Generic;

namespace RelicsAPI.Data.DTOs.Relics
{
    public record RelicDTO(int Id, string Name, byte[] Image, List<string> Materials, string Creator, decimal Price, string Period, string YearMade, int CategoryId);
}
