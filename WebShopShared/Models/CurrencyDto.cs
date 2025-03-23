using System.Text.Json.Serialization;

namespace WebShopShared.Models
{
	public class CurrencyDto
	{
		[JsonPropertyName("conversion_result")]
		public double ConversionResult { get; set; }
		public required string TargetCurrency { get; set; }
	}
}
