namespace Application.Common.Config;

public record SftpSettings
{
    public required string UserName { get; set; }
    public required string Ip { get; set; }
    public required string Password { get; set; }
    public required string BasePath { get; set; }
    public required long MaxFileSize { get; set; }
    public required string SurveyPath { get; set; }
}