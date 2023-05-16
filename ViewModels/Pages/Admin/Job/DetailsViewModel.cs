using System.ComponentModel.DataAnnotations;

namespace ViewModels.Pages.Admin.Job
{
	public class DetailsViewModel : UpdateViewModel
	{
		// **********
		[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Score))]
		public decimal Score { get; set; }
		// **********

		// **********
		[Display
			(Name = nameof(Resources.DataDictionary.UpdateDateTime),
			ResourceType = typeof(Resources.DataDictionary))]
		public DateTime? UpdateDateTime { get; init; }
		// **********

		// **********
		[Display
			(Name = nameof(Resources.DataDictionary.InsertDateTime),
			ResourceType = typeof(Resources.DataDictionary))]
		public DateTime? InsertDateTime { get; init; }
		// **********
	}
}
