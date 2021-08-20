namespace CareerCloud.Pocos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Security_Roles")]
    public class SecurityRolePoco : IPoco
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Guid Id { get; set; }

        [Column(TypeName = "varchar(50)"), Required(AllowEmptyStrings = true)]
        public string Role { get; set; }

        [Column("Is_Inactive"), DefaultValue(0)]
        public bool IsInactive { get; set; }




        public virtual ICollection<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
    }
}
