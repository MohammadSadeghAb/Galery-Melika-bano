using System.ComponentModel.DataAnnotations;
using Resources.Messages;
using static Domain.SeedWork.Constants;

namespace ViewModels.Pages.Admin.Job
{
	public class UpdateViewModel : CommonViewModel
	{
		// ********
		[Display
		(Name = nameof(Resources.DataDictionary.Id),
		 ResourceType = typeof(Resources.DataDictionary))]
		public Guid? Id { get; set; }
		// ********

		// **********
		[MaxLength
		(length: FixedLength.CellPhoneNumber,
		ErrorMessageResourceType = typeof(Validations),
		ErrorMessageResourceName = nameof(Validations.MaxLength))]

		[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.CellPhoneNumber))]
		public string? PhoneNumber2 { get; set; }
		// **********

		// **********
		[MaxLength
		(length: FixedLength.CellPhoneNumber,
		ErrorMessageResourceType = typeof(Validations),
		ErrorMessageResourceName = nameof(Validations.MaxLength))]

		[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.CellPhoneNumber))]
		public string? CellPhoneNumber1 { get; set; }
		// **********

		// **********
		[MaxLength
		(length: FixedLength.CellPhoneNumber,
		ErrorMessageResourceType = typeof(Validations),
		ErrorMessageResourceName = nameof(Validations.MaxLength))]

		[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.CellPhoneNumber))]
		public string? CellPhoneNumber2 { get; set; }
		// **********
	}
}
