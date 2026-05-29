namespace Argos.Api.Models;

public class ZonaRisco
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Cidade { get; set; } = string.Empty;

    public string NivelRisco { get; set; } = string.Empty;
}