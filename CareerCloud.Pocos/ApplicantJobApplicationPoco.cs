namespace CareerCloud.Pocos
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Applicant_Job_Applications")]
    public class ApplicantJobApplicationPoco : IPoco
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public Guid Applicant { get; set; }

        public Guid Job { get; set; }

        [Column("Application_Date",TypeName = "datetime2(7)"), DefaultValue("getdate()")]
        public DateTime ApplicationDate { get; set; }

        [Column("Time_Stamp"), Timestamp]
        public byte[] TimeStamp { get; set; }

        [ForeignKey("Applicant")]
        public virtual ApplicantProfilePoco ApplicantProfile { get; set; }

        [ForeignKey("Job")]
        public virtual CompanyJobPoco CompanyJob { get; set; }

    }
}
