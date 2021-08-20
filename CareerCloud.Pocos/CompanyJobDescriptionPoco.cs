namespace CareerCloud.Pocos
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Company_Jobs_Descriptions")]
    public class CompanyJobDescriptionPoco : IPoco
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public Guid Job { get; set; }

        [Column("Job_Name"), MaxLength(100)]
        public string JobName { get; set; }

        [Column("Job_Descriptions"), MaxLength(100)]
        public string JobDescriptions { get; set; }

        [Column("Time_Stamp"), Timestamp]
        public byte[] TimeStamp { get; set; }




        [ForeignKey("Job")]
        public virtual CompanyJobPoco CompanyJob { get; set; }
    }
}
