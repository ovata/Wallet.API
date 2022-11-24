using System.Net;
using System.Text.Json.Serialization;

namespace Wallet.Service.Common;

public class BasicActionResult
{
    [JsonIgnore]
    public string ErrorMessage { get; set; }

    public BasicActionResult(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }
}