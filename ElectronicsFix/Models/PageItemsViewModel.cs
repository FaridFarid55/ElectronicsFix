namespace ElectronicsFix.Models
{
	public class PageItemsViewModel
	{
		public StoreItemsViewModel StoreItemsViewModel { get; set; }
		public int PageNumber { get; set; }
		public int TotalPages { get; set; }
		public string Category {  get; set; }
		public string Brand { get; set; }
		public string Sort { get; set; }

	}
}
