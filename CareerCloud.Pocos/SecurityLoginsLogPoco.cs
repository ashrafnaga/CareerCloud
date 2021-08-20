﻿namespace CareerCloud.Pocos
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Security_Logins_Log")]
    public class SecurityLoginsLogPoco : IPoco
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public Guid Login { get; set; }

        [Column("Source_IP", TypeName = "char(15)"), Required(AllowEmptyStrings = true)]
        public string SourceIP { get; set; }

        [Column("Logon_Date")]
        public DateTime LogonDate { get; set; }

        [Column("Is_Succesful"), DefaultValue(0)]
        public bool IsSuccesful { get; set; }




        [ForeignKey("Login")]
        public virtual SecurityLoginPoco SecurityLogin { get; set; }
    }
}
