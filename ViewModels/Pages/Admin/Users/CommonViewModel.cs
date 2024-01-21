using Resources.Messages;
using static Domain.SeedWork.Constants;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System.Xml.Linq;

namespace ViewModels.Pages.Admin.Users;

public class CommonViewModel : object
{
	public CommonViewModel() : base()
	{
		Ordering = 10_000;
	}


	// **********
	[System.ComponentModel.DataAnnotations.Display
	(Name = nameof(Resources.DataDictionary.Username),
	ResourceType = typeof(Resources.DataDictionary))]

	[System.ComponentModel.DataAnnotations.Required
	(AllowEmptyStrings = false,
	ErrorMessageResourceType = typeof(Resources.Messages.Validations),
	ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public string Username { get; set; }
	// **********

	// **********
	[System.ComponentModel.DataAnnotations.Display
	(Name = nameof(Resources.DataDictionary.FullName),
	ResourceType = typeof(Resources.DataDictionary))]

	[System.ComponentModel.DataAnnotations.Required
	(AllowEmptyStrings = false,
	ErrorMessageResourceType = typeof(Resources.Messages.Validations),
	ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public string? FullName { get; set; }
	// **********

	// **********
	//[System.ComponentModel.DataAnnotations.Display
	//	(Name = nameof(Resources.DataDictionary.EmailAddress),
	//	ResourceType = typeof(Resources.DataDictionary))]

	//[System.ComponentModel.DataAnnotations.Required
	//	(AllowEmptyStrings = false,
	//	ErrorMessageResourceType = typeof(Resources.Messages.Validations),
	//	ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	//[System.ComponentModel.DataAnnotations.MaxLength
	//	(length: Domain.SeedWork.Constants.MaxLength.EmailAddress,
	//	ErrorMessageResourceType = typeof(Resources.Messages.Validations),
	//	ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

	//[System.ComponentModel.DataAnnotations.RegularExpression
	//	(pattern: Domain.SeedWork.Constants.RegularExpression.EmailAddress,
	//	ErrorMessageResourceType = typeof(Resources.Messages.Validations),
	//	ErrorMessageResourceName = nameof(Resources.Messages.Validations.EmailAddress))]
	//public string? EmailAddress { get; set; }
	// **********

	// **********
	[System.ComponentModel.DataAnnotations.Display
		(Name = nameof(Resources.DataDictionary.CellPhoneNumber),
		ResourceType = typeof(Resources.DataDictionary))]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Domain.SeedWork.Constants.FixedLength.CellPhoneNumber,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

	[System.ComponentModel.DataAnnotations.RegularExpression
		(pattern: Domain.SeedWork.Constants.RegularExpression.CellPhoneNumber,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.CellPhoneNumber))]
	public string? CellPhoneNumber { get; set; }
	// **********

	// **********
	[System.ComponentModel.DataAnnotations.Display
		(Name = nameof(Resources.DataDictionary.Role),
		ResourceType = typeof(Resources.DataDictionary))]

	//[System.ComponentModel.DataAnnotations.Required
	//	(ErrorMessageResourceType = typeof(Resources.Messages.Validations),
	//	ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public System.Guid? RoleId { get; set; }
	// **********

	// **********
	[System.ComponentModel.DataAnnotations.Display
		(Name = nameof(Resources.DataDictionary.IsActive),
		ResourceType = typeof(Resources.DataDictionary))]
	public bool IsActive { get; set; }
	// **********

	// **********
	[System.ComponentModel.DataAnnotations.Display
		(Name = nameof(Resources.DataDictionary.IsUndeletable),
		ResourceType = typeof(Resources.DataDictionary))]
	public bool IsUndeletable { get; set; }
	// **********

	// **********
	[System.ComponentModel.DataAnnotations.Display
		(Name = nameof(Resources.DataDictionary.IsProgrammer),
		ResourceType = typeof(Resources.DataDictionary))]
	public bool IsProgrammer { get; set; }
	// **********

	// **********
	[System.ComponentModel.DataAnnotations.Display
		(Name = nameof(Resources.DataDictionary.Ordering),
		ResourceType = typeof(Resources.DataDictionary))]

	[System.ComponentModel.DataAnnotations.Range
		(minimum: Domain.SeedWork.Constants.Minimum.Ordering,
		maximum: Domain.SeedWork.Constants.Maximum.Ordering,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Range))]
	public int Ordering { get; set; }
	// **********

	// **********
	[System.ComponentModel.DataAnnotations.Display
		(Name = nameof(Resources.DataDictionary.AdminDescription),
		ResourceType = typeof(Resources.DataDictionary))]
	public string? AdminDescription { get; set; }
    // **********

    // **********
    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Postalcode))]

    [MaxLength
        (length: FixedLength.PostalCode,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

    [System.ComponentModel.DataAnnotations.RegularExpression
        (pattern: Domain.SeedWork.Constants.RegularExpression.NationalCode,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.PostalCode))]

    public string? PostalCode { get; set; }
    // **********

    // **********
    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Province))]

    [MaxLength
        (length: MaxLength.Province,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

    public string? ProvinceAddress { get; set; }
    // **********

    // **********
    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.City))]

    [MaxLength
        (length: MaxLength.City,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

	public string? CityAddress { get; set; }
	// **********

	//// **********
 //   [Display
 //       (ResourceType = typeof(Resources.DataDictionary),
 //       Name = nameof(Resources.DataDictionary.Fathersname))]

 //   [Required
 //       (ErrorMessageResourceType = typeof(Validations),
 //       ErrorMessageResourceName = nameof(Validations.Required))]

 //   [MaxLength
 //       (length: MaxLength.Name,
 //       ErrorMessageResourceType = typeof(Validations),
 //       ErrorMessageResourceName = nameof(Validations.MaxLength))]
	//public string FatherName { get; set; }
	//// **********

	// **********
    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.NationalCode))]

    [MaxLength
        (length: FixedLength.NationalCode,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

    [System.ComponentModel.DataAnnotations.RegularExpression
        (pattern: Domain.SeedWork.Constants.RegularExpression.NationalCode,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.NationalCode))]

    public string? NationalCode { get; set; }
	// **********

	// **********
    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Address))]

    [MaxLength
        (length: MaxLength.Address,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

    public string? Address { get; set; }
    // **********

    // **********
    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Gender))]

    [MaxLength
        (length: MaxLength.Gender,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

    [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]

    public string? Gender { get; set; }
    // **********
}
