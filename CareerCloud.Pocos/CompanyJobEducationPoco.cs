namespace CareerCloud.Pocos
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Company_Job_Educations")]
    public class CompanyJobEducationPoco : IPoco
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public Guid Job { get; set; }

        [Required(AllowEmptyStrings = true), MaxLength(100)]
        public string Major { get; set; }

        [DefaultValue(0)]
        public Int16 Importance { get; set; }

        [Column("Time_Stamp"), Timestamp]
        public byte[] TimeStamp { get; set; }




        [ForeignKey("Job")]
        public virtual CompanyJobPoco CompanyJob { get; set; }
    }
}
