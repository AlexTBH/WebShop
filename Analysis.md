Problem

1. Min PopulateDatabase lade aldrig till objekt i databasen. Jag använde breakpoints för att kolla igenom alla steg för att se vad som händer.
Jag noterade att DbSet<Product> Products aldrig kallades. Problemet var i koden som såg ut enligt nedan.

			public static void PopulateDatabase(WebShopDbContext db)
			{
				var products = ProductData.GetSampleProducts();

				foreach(var product in products)
				{
					db.Add(product); *** Här hade jag bara db.Add, jag upptäckte att den inte lade till objekten i tabellen Products
				}
				db.SaveChangesAsync();
			}

Lösning: 
Jag ändrade ovan kod till db.Products.Add istället.

2. Produkter fetchades två gånger från mina blazor komponenter Jag använde breakpoints för att se anledningen till att det skedde.
Det verkade ske eftersom min LoadProducts kallades av föräldrarkomponenten vilket triggade igång fetchen via OnInitializedAsync.
Andra gången som produkterna fetchades verkade bero på att en omrendering av föräldern (StateHasChanged) ledde till att LoadProducts körde igen

	<div class="product-container">
	    <h1>Products</h1>
	    <LoadProducts OnProductsLoaded="HandleProductsLoaded" />
	
	    <div class="products-list">
	        <GenericList Items="_products">
	            <ItemTemplate>
	                <div class="product-item">
	                    <img src="@context.Url" />
	                    <p>@context.Name</p>
	                    <p>@context.Price kr</p>
	                </div>
	            </ItemTemplate>
	        </GenericList>
	    </div>
	
	</div>
	
	@code {
	    private List<ProductDto> _products = new List<ProductDto>();
	
	    private async Task HandleProductsLoaded(List<ProductDto> fetchedProducts)
	    {
	        _products = fetchedProducts;
	        await InvokeAsync(StateHasChanged);
	    }
	}
	
	
	@using WebShopShared.Interfaces;
	@using WebShopShared.Models;
	
	@code {
	    [Inject]
	    private IProductService? _productService { get; set; }
	
	    [Parameter]
	    public EventCallback<List<ProductDto>> OnProductsLoaded { get; set; }
	
	    private List<ProductDto> _products = new List<ProductDto>();
	
	    protected override async Task OnInitializedAsync()
	    {
	        if (_productService == null)
	        {
	            throw new InvalidOperationException("ProductService is not available.");
	        }
	
	        _products = await _productService.GetProducts(); 
	        await OnProductsLoaded.InvokeAsync(_products);
	    }
	}
	
Lösning:
Tog bort min LoadProducts komponent och tog in fetch logiken direkt till min page komponent istället

    private List<ProductDto> _products = new List<ProductDto>();
    [Inject]
    private IProductService? _productService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (_productService == null)
        {
            throw new InvalidOperationException("ProductService is not available.");
        }

        _products = await _productService.GetProducts();
    }


3. Mina produkter fetchades sällan vid första körningen. Efter att ha satt en breakpoint vid min Populatedatabase så noterade jag att ajg körde SaveChangesAsync, däremot var inte metod async, samt att jag inte awaitar det.

Lösning: 
Ändra metoden till async och awaita det

	Innan ändring
	static public class DatabaseHelper
	{
		public static void PopulateDatabase(WebShopDbContext db)
		{
			var products = ProductData.GetSampleProducts();

			foreach(var product in products)
			{
				db.Products.Add(product);
			}
			db.SaveChangesAsync();
		}
	}
	Efter ändring
		static public class DatabaseHelper
	{
		public static async Task PopulateDatabase(WebShopDbContext db)
		{
			var products = ProductData.GetSampleProducts();

			foreach(var product in products)
			{
				db.Products.Add(product);
			}
			await db.SaveChangesAsync();
		}
	}

	4. Jag fick statuskod 400 när jag registrerade mina users, för att upptäcka vad felet berodde på så använde jag breakpoints och upptäckte att min serialisering av objekt returnerade i en tom objekt "{}". Efter lite felsökande kom jag på att min objekt inte hade getter & setters.

	Lösning: Lägga till getters & setters till mina properties.