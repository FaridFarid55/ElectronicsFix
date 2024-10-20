namespace ElectronicsFix.Models
{
	public class StoreItemsViewModel
	{
		public IEnumerable<Domains.Item> Items { get; set; }
		public IEnumerable<Domains.ItemDetail> ItemDetails { get; set; }
		public IEnumerable<Domains.ItemDiscount> ItemDiscounts { get; set; }
		public IEnumerable<Domains.Category> Categories { get; set; }
	}
}
