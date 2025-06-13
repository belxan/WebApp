namespace Application.Common.Config;

public record ConnectionStrings
{
    public required string AppDb { get; set; }
}