namespace CareerCloud.Pocos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Security_Logins")]
    public class SecurityLoginPoco : IPoco
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public Guid Id { get; set; }

		[Column(TypeName = "varchar(50)"), Required(AllowEmptyStrings = true)]
		public string Login { get; set; }

		[Column(TypeName = "varchar(100)"), Required(AllowEmptyStrings = true)]
		public string Password { get; set; }

		[Column("Created_Date", TypeName = "datetime2(7)")]
		public DateTime Created { get; set; }

		[Column("Password_Update_Date", TypeName = "datetime2(7)")]
		public DateTime? PasswordUpdate { get; set; }

		[Column("Agreement_Accepted_Date", TypeName = "datetime2(7)")]
		public DateTime? AgreementAccepted { get; set; }

		[Column("Is_Locked"), DefaultValue(0)]
		public bool IsLocked { get; set; }

		[Column("Is_Inactive"), DefaultValue(0)]
		public bool IsInactive { get; set; }

		[Column("Email_Address", TypeName = "varchar(50)"), Required(AllowEmptyStrings = true)]
		public string EmailAddress { get; set; }

		[Column("Phone_Number", TypeName = "varchar(20)")]
		public string PhoneNumber { get; set; }

		[Column("Full_Name", TypeName = "varchar(100)")]
		public string FullName { get; set; }

		[Column("Force_Change_Password"), DefaultValue(0)]
		public bool ForceChangePassword { get; set; }

		[Column("Prefferred_Language", TypeName = "char(10)")]
		public string PrefferredLanguage { get; set; }

		[Column("Time_Stamp"), Timestamp]
		public byte[] TimeStamp { get; set; }




		public virtual ICollection<ApplicantProfilePoco> ApplicantProfiles { get; set; }
		public virtual ICollection<SecurityLoginsLogPoco> SecurityLoginsLogs { get; set; }
		public virtual ICollection<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
	}
}
