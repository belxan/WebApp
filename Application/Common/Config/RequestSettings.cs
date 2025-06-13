namespace Application.Common.Config;

public record RequestSettings
{
    public required string PageIndex { get; set; }
    public required string PageSize { get; set; }
}