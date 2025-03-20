namespace WebShopBackend.Interfaces
{
	public interface ICurrencyExchangeApi
	{
		public Task<decimal> GetUSD(decimal sek);
	}
}
