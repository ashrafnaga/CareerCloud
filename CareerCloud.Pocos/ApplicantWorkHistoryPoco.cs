namespace CareerCloud.Pocos
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Applicant_Work_History")]
    public class ApplicantWorkHistoryPoco : IPoco
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Guid Id { get; set; }

		public Guid Applicant { get; set; }

		[Column("Company_Name"),Required(AllowEmptyStrings = true), MaxLength(150)]
		public string CompanyName { get; set; }

		[Column("Country_Code", TypeName = "char(10)"), Required(AllowEmptyStrings = true)]
		public string CountryCode { get; set; }

		[Required(AllowEmptyStrings = true), MaxLength(50)]
		public string Location { get; set; }

		[Column("Job_Title"), Required(AllowEmptyStrings = true), MaxLength(50)]
		public string JobTitle { get; set; }

		[Column("Job_Description"), Required(AllowEmptyStrings = true), MaxLength(500)]
		public string JobDescription { get; set; }

		[Column("Start_Month")]
		public Int16 StartMonth { get; set; }

		[Column("Start_Year")]
		public int StartYear { get; set; }

		[Column("End_Month")]
		public Int16 EndMonth { get; set; }

		[Column("End_Year")]
		public int EndYear { get; set; }

		[Column("Time_Stamp"), Timestamp]
		public byte[] TimeStamp { get; set; }




		[ForeignKey("Applicant")]
		public virtual ApplicantProfilePoco ApplicantProfile { get; set; }

		[ForeignKey("CountryCode")]
		public virtual SystemCountryCodePoco SystemCountryCode { get; set; }

	}
}
