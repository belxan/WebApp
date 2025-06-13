namespace Application.Common.Config;

public record ElasticSearchSettings : Controllable
{
    public required string Connection { get; set; }
    public required string DefaultIndex { get; set; }
}