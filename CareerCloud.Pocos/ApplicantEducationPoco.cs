namespace CareerCloud.Pocos
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Applicant_Educations")]
    public class ApplicantEducationPoco: IPoco
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public Guid Applicant { get; set; }

        [Required(AllowEmptyStrings = true),MaxLength(100)]
        public string Major { get; set; }

        [Column("Certificate_Diploma"), MaxLength(100)]
        public string CertificateDiploma { get; set; }

        [Column("Start_Date")]
        public DateTime? StartDate { get; set; }

        [Column("Completion_Date")]
        public DateTime? CompletionDate { get; set; }

        [Column("Completion_Percent")]
        public byte? CompletionPercent { get; set; }

        [Column("Time_Stamp"),Timestamp]
        public byte[] TimeStamp { get; set; }


        [ForeignKey("Applicant")]
        public virtual ApplicantProfilePoco ApplicantProfile { get; set; }
    }
}
