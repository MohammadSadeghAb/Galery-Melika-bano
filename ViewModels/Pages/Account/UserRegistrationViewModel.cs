using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Pages.Account
{
	public class UserRegistrationViewModel
	{
        public UserRegistrationViewModel() : base()
        {

        }

        // **********
        [System.ComponentModel.DataAnnotations.Display
            (Name = nameof(Resources.DataDictionary.FullName),
            ResourceType = typeof(Resources.DataDictionary))]

        [System.ComponentModel.DataAnnotations.Required
            (AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

        [System.ComponentModel.DataAnnotations.MaxLength
            (length: Domain.SeedWork.Constants.MaxLength.FullName,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

        public string FullName { get; set; }
        // **********

        // **********
        //[System.ComponentModel.DataAnnotations.Display
        //    (Name = nameof(Resources.DataDictionary.EmailAddress),
        //    ResourceType = typeof(Resources.DataDictionary))]

        //[System.ComponentModel.DataAnnotations.MaxLength
        //    (length: Domain.SeedWork.Constants.MaxLength.EmailAddress,
        //    ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        //    ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

        //[System.ComponentModel.DataAnnotations.RegularExpression
        //    (pattern: Domain.SeedWork.Constants.RegularExpression.EmailAddress,
        //    ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        //    ErrorMessageResourceName = nameof(Resources.Messages.Validations.EmailAddress))]

        //public string? EmailAddress { get; set; }
        // **********

        // **********
        [System.ComponentModel.DataAnnotations.Display
            (Name = nameof(Resources.DataDictionary.NationalCode),
            ResourceType = typeof(Resources.DataDictionary))]

        [System.ComponentModel.DataAnnotations.MaxLength
            (length: Domain.SeedWork.Constants.FixedLength.NationalCode,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

        [System.ComponentModel.DataAnnotations.RegularExpression
            (pattern: Domain.SeedWork.Constants.RegularExpression.NationalCode,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.NationalCode))]

        public string? NationalCode { get; set; }
        // **********

        // **********
        [System.ComponentModel.DataAnnotations.Display
            (Name = nameof(Resources.DataDictionary.Postalcode),
            ResourceType = typeof(Resources.DataDictionary))]

        [System.ComponentModel.DataAnnotations.MaxLength
            (length: Domain.SeedWork.Constants.FixedLength.PostalCode,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

        [System.ComponentModel.DataAnnotations.RegularExpression
            (pattern: Domain.SeedWork.Constants.RegularExpression.NationalCode,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.PostalCode))]

        public string? PostalCode { get; set; }
        // **********

        // **********
        [System.ComponentModel.DataAnnotations.Display
            (Name = nameof(Resources.DataDictionary.Gender),
            ResourceType = typeof(Resources.DataDictionary))]

        [System.ComponentModel.DataAnnotations.MaxLength
            (length: Domain.SeedWork.Constants.MaxLength.Gender,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

        public string? Gender { get; set; }
        // **********

        // **********
        [System.ComponentModel.DataAnnotations.Display
            (Name = nameof(Resources.DataDictionary.Address),
            ResourceType = typeof(Resources.DataDictionary))]

        [System.ComponentModel.DataAnnotations.MaxLength
            (length: Domain.SeedWork.Constants.MaxLength.Address,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

        [System.ComponentModel.DataAnnotations.Required
            (AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

        public string Address { get; set; }
        // **********

        // **********
        [System.ComponentModel.DataAnnotations.Display
            (Name = nameof(Resources.DataDictionary.Province),
            ResourceType = typeof(Resources.DataDictionary))]

        [System.ComponentModel.DataAnnotations.MaxLength
            (length: Domain.SeedWork.Constants.MaxLength.Province,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

        [System.ComponentModel.DataAnnotations.Required
            (AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

        public string Province { get; set; }
        // **********

        // **********
        [System.ComponentModel.DataAnnotations.Display
            (Name = nameof(Resources.DataDictionary.City),
            ResourceType = typeof(Resources.DataDictionary))]

        [System.ComponentModel.DataAnnotations.MaxLength
            (length: Domain.SeedWork.Constants.MaxLength.City,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

        [System.ComponentModel.DataAnnotations.Required
            (AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

        public string City { get; set; }
        // **********

        // **********
        [System.ComponentModel.DataAnnotations.Display
            (Name = nameof(Resources.DataDictionary.CellPhoneNumber),
            ResourceType = typeof(Resources.DataDictionary))]

        [System.ComponentModel.DataAnnotations.Required
            (AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

        [System.ComponentModel.DataAnnotations.MaxLength
            (length: Domain.SeedWork.Constants.FixedLength.CellPhoneNumber,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

        [System.ComponentModel.DataAnnotations.RegularExpression
            (pattern: Domain.SeedWork.Constants.RegularExpression.CellPhoneNumber,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.CellPhoneNumber))]
        public string CellPhoneNumber { get; set; }
        // **********

        //// **********
        [System.ComponentModel.DataAnnotations.Display
            (Name = nameof(Resources.DataDictionary.Username),
            ResourceType = typeof(Resources.DataDictionary))]

        [System.ComponentModel.DataAnnotations.Required
            (AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

        [System.ComponentModel.DataAnnotations.MaxLength
            (length: Domain.SeedWork.Constants.MaxLength.Username,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

        [System.ComponentModel.DataAnnotations.RegularExpression
            (pattern: Domain.SeedWork.Constants.RegularExpression.Username,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.Username))]
        public string Username { get; set; }
        //// **********

        // **********
        [System.ComponentModel.DataAnnotations.Display
            (Name = nameof(Resources.DataDictionary.Password),
            ResourceType = typeof(Resources.DataDictionary))]

        [System.ComponentModel.DataAnnotations.Required
            (AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

        [System.ComponentModel.DataAnnotations.MaxLength
            (length: Domain.SeedWork.Constants.MaxLength.Password,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

        [System.ComponentModel.DataAnnotations.RegularExpression
            (pattern: Domain.SeedWork.Constants.RegularExpression.Password,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.Password))]

        [System.ComponentModel.DataAnnotations.DataType
            (dataType: System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password { get; set; }
        // **********

        // **********
        [System.ComponentModel.DataAnnotations.Display
            (Name = nameof(Resources.DataDictionary.ConfirmPassword),
            ResourceType = typeof(Resources.DataDictionary))]

        [System.ComponentModel.DataAnnotations.Required
            (AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

        //[System.ComponentModel.DataAnnotations.Compare
        //	(otherProperty: "Password",
        //	ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        //	ErrorMessageResourceName = nameof(Resources.Messages.Validations.Compare))]

        [System.ComponentModel.DataAnnotations.Compare
            (otherProperty: nameof(Password),
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.Compare))]

        [System.ComponentModel.DataAnnotations.DataType
            (dataType: System.ComponentModel.DataAnnotations.DataType.Password)]
        public string ConfirmPassword { get; set; }
        // **********
    }
}
