using System.Security.Cryptography;
using System.Text;
using Application.DTOs.FlightSearch;

namespace Infrastructure.Services;

public class SignatureService
{
    public string GenerateSignature(string token, RoundTripRequest request)
    {
        var values = new List<string>
        {
            token,
            request.marker,
            request.currency,
            request.host,
            request.know_english,
            request.locale,
            request.trip_class,
            request.user_ip
        };
        values = values.OrderBy(m=>m).ToList();

        string raw = string.Join(":", values);
        using var md5 = MD5.Create();
        byte[] inputBytes = Encoding.UTF8.GetBytes(raw);
        byte[] hashBytes = md5.ComputeHash(inputBytes);
        return Convert.ToHexString(hashBytes).ToLower();
    }
}
