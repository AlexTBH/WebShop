using WebShopShared.Models;

namespace WebShopShared.Interfaces
{
	public interface ICurrencyExchange
	{
		public Task<CurrencyDto> ConvertCurrency(CurrencyDto request);
	}
}
