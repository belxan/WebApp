namespace Application.Common.Config;

public record ConfigSettings
{
    public AuthSettings AuthSettings { get; set; } = default!;
    public GeneralSettings GeneralSettings { get; set; } = default!;
    public DatabaseOptionSettings DatabaseOptionSettings { get; set; } = default!;
    public RequestSettings RequestSettings { get; set; } = default!;
    public SwaggerSettings SwaggerSettings { get; set; } = default!;
    public ElasticSearchSettings ElasticSearchSettings { get; set; } = default!;
    public CryptographySettings CryptographySettings { get; set; } = default!;
    public MailSettings MailSettings { get; set; } = default!;
    public SftpSettings SftpSettings { get; set; } = default!;
}
