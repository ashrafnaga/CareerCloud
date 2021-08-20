namespace CareerCloud.Pocos
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Applicant_Resumes")]
    public class ApplicantResumePoco : IPoco
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public Guid Applicant { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Resume { get; set; }

        [Column("Last_Updated", TypeName = "datetime2(7)"), DefaultValue("getdate()")]
        public DateTime? LastUpdated { get; set; }



        [ForeignKey("Applicant")]
        public virtual ApplicantProfilePoco ApplicantProfile { get; set; }
    }
}
