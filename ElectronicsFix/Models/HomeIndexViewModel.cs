namespace ElectronicsFix.Models
{
	public class HomeIndexViewModel
	{
		public IEnumerable<Domains.Item> Items { get; set; }
		public IEnumerable<Domains.ItemDetail> ItemDetails { get; set; }
		public IEnumerable<Domains.ItemDiscount> ItemDiscounts { get; set; }
	}
}
