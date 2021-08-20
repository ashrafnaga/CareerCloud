namespace CareerCloud.Pocos
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Company_Job_Skills")]
    public class CompanyJobSkillPoco : IPoco
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public Guid Job { get; set; }

        [Required(AllowEmptyStrings = true), MaxLength(100)]
        public string Skill { get; set; }

        [Column("Skill_Level", TypeName = "varchar(10)"), Required(AllowEmptyStrings = true)]
        public string SkillLevel { get; set; }

        public int Importance { get; set; }

        [Column("Time_Stamp"), Timestamp]
        public byte[] TimeStamp { get; set; }




        [ForeignKey("Job")]
        public virtual CompanyJobPoco CompanyJob { get; set; }
    }
}
