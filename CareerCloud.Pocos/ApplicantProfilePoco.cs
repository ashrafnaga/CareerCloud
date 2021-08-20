namespace CareerCloud.Pocos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Applicant_Profiles")]
    public class ApplicantProfilePoco : IPoco
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
		public Guid Id { get; set; }
		public Guid Login { get; set; }

		[Column("Current_Salary", TypeName = "decimal(18,0)")]
		public decimal? CurrentSalary { get; set; }

		[Column("Current_Rate", TypeName = "decimal(18,0)")]
		public decimal? CurrentRate { get; set; }

		[Column(TypeName = "char(10)")]
		public string Currency { get; set; }

		[Column("Country_Code", TypeName = "char(10)")]
		public string Country { get; set; }

		[Column("State_Province_Code", TypeName = "char(10)")]
		public string Province { get; set; }

		[Column("Street_Address"), MaxLength(100)]
		public string Street { get; set; }

		[Column("City_Town"), MaxLength(100)]
		public string City { get; set; }

		[Column("Zip_Postal_Code", TypeName = "char(20)")]
		public string PostalCode { get; set; }

		[Column("Time_Stamp"), Timestamp]
		public byte[] TimeStamp { get; set; }




		[ForeignKey("Login")]
		public virtual SecurityLoginPoco SecurityLogin { get; set; }
		[ForeignKey("Country")]
		public virtual SystemCountryCodePoco SystemCountryCode { get; set; }
		public virtual ICollection<ApplicantEducationPoco> ApplicantEducations { get; set; }
		public virtual ICollection<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
		public virtual ICollection<ApplicantResumePoco> ApplicantResumes { get; set; }
		public virtual ICollection<ApplicantSkillPoco> ApplicantSkills { get; set; }
		public virtual ICollection<ApplicantWorkHistoryPoco> ApplicantWorkHistorys { get; set; }

	}
}
