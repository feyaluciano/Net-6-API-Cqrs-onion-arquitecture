using CQRS.BankAPI.Domain.Common;

namespace CQRS.BankAPI.Domain.Entities
{
    public class Customer : AuditBaseEntity
    {
        private int _age;
        public string? FirtsName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        public int Age
        {
            get
            {
                if(_age <=0)
                {
                    _age = new DateTime(DateTime.Now.Subtract(BirthDate).Ticks).Year - 1;
                }
                return _age;
            }
            set { 
            this._age = value;
            }

        }
    }
}
