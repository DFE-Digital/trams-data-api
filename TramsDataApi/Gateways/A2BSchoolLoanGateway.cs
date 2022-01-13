using System.Linq;
using Microsoft.EntityFrameworkCore;
using TramsDataApi.DatabaseModels;

namespace TramsDataApi.Gateways
{
    public class A2BSchoolLoanGateway : IA2BSchoolLoanGateway
    {
        private readonly TramsDbContext _tramsDbContext;

        public A2BSchoolLoanGateway(TramsDbContext tramsDbContext)
        {
            _tramsDbContext = tramsDbContext;
        }
        public A2BSchoolLoan CreateA2BSchoolLoan(A2BSchoolLoan schoolLoan)
        {
            _tramsDbContext.A2BSchoolLoans.Add(schoolLoan);
            _tramsDbContext.SaveChanges();

            return schoolLoan;
        }

        public A2BSchoolLoan GetByLoanId(string loanId)
        {
            return _tramsDbContext.A2BSchoolLoans
                .AsNoTracking()
                .FirstOrDefault(k => k.SchoolLoanId == loanId);
        }
    }
}