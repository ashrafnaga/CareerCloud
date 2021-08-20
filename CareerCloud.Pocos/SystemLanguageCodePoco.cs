namespace CareerCloud.Pocos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("System_Language_Codes")]
    public class SystemLanguageCodePoco
    {
        [Key, Column(TypeName = "char(10)")]
        public string LanguageID { get; set; }

        [Required(AllowEmptyStrings = true), MaxLength(50)]
        public string Name { get; set; }

        [Column("Native_Name"), Required(AllowEmptyStrings = true), MaxLength(50)]
        public string NativeName { get; set; }



        public virtual ICollection<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
    }
}
