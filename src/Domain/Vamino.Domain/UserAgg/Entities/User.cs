using Vamino.Domain._Common;
using Vamino.Domain.LoanContractAgg.Entities;
using Vamino.Domain.LoanGuarantorAgg.Entities;

namespace Vamino.Domain.UserAgg.Entities;

public class User : BaseEntity
{
    public string IdentityId { get; set; } 
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalCode { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public decimal Balance { get; set; }
    public ICollection<LoanContract> BorrowerContracts { get; set; } = new List<LoanContract>();
    public ICollection<LoanGuarantor> GuaranteedContracts { get; set; } = new List<LoanGuarantor>();
}