namespace CareerCloud.Pocos
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Company_Descriptions")]
    public class CompanyDescriptionPoco : IPoco
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Computed), DefaultValue("newid()")]
        public Guid Id { get; set; }

        public Guid Company { get; set; }

        [Required(AllowEmptyStrings = true), Column(TypeName = "char(10)")]
        public string LanguageId { get; set; }

        [Column("Company_Name"), Required(AllowEmptyStrings = true), MaxLength(50)]
        public string CompanyName { get; set; }

        [Column("Company_Description"), Required(AllowEmptyStrings = true), MaxLength(1000)]
        public string CompanyDescription { get; set; }

        [Column("Time_Stamp"), Timestamp]
        public byte[] TimeStamp { get; set; }




        [ForeignKey("Company")]
        public virtual CompanyProfilePoco CompanyProfile { get; set; }

        [ForeignKey("LanguageId")]
        public virtual SystemLanguageCodePoco SystemLanguageCode { get; set; }
    }
}
