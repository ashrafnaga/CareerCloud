namespace CareerCloud.BusinessLogicLayer
{
    using CareerCloud.DataAccessLayer;
    using CareerCloud.Pocos;
    using System;
    using System.Collections.Generic;

    public class SecurityRoleLogic : BaseLogic<SecurityRolePoco>
    {
        public SecurityRoleLogic(IDataRepository<SecurityRolePoco> repository) : base(repository) { }
        protected override void Verify(SecurityRolePoco[] securityRoles)
        {
            List<ValidationException> exceptionsList = new List<ValidationException>();
            foreach (SecurityRolePoco item in securityRoles)
            {
                if (string.IsNullOrEmpty(item.Role))
                {
                    exceptionsList.Add(new ValidationException(800, "Role Cannot be empty, kindly solve the issue."));
                }
            }

            if (exceptionsList.Count > 0)
            {
                throw new AggregateException(exceptionsList);
            }
        }

        public override void Add(SecurityRolePoco[] securityRoles)
        {
            this.Verify(securityRoles);
            base.Add(securityRoles);
        }

        public override void Update(SecurityRolePoco[] securityRoles)
        {
            this.Verify(securityRoles);
            base.Update(securityRoles);
        }
    }
}
