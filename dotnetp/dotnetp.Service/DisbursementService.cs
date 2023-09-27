using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DataAccess;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public class VerificationService : IVerificationService
    {
        private readonly IDataAccess<VerificationModel> _verificationDataAccess;

        public VerificationService(IDataAccess<VerificationModel> verificationDataAccess)
        {
            _verificationDataAccess = verificationDataAccess;
        }

        public async Task<int> AddAsync(VerificationModel verification)
        {
            // TODO: Implement your add logic here
            throw new NotImplementedException();
        }

        public async Task<VerificationModel> GetByIdAsync(int id)
        {
            // TODO: Implement your get by ID logic here
            throw new NotImplementedException();
        }

        public async Task<List<VerificationModel>> GetAllAsync()
        {
            // TODO: Implement your get all logic here
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(VerificationModel verification)
        {
            // TODO: Implement your update logic here
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // TODO: Implement your delete logic here
            throw new NotImplementedException();
        }
    }

    public class ValidationService : IValidationService
    {
        private readonly IDataAccess<ValidationModel> _validationDataAccess;

        public ValidationService(IDataAccess<ValidationModel> validationDataAccess)
        {
            _validationDataAccess = validationDataAccess;
        }

        public async Task<int> AddAsync(ValidationModel validation)
        {
            // TODO: Implement your add logic here
            throw new NotImplementedException();
        }

        public async Task<ValidationModel> GetByIdAsync(int id)
        {
            // TODO: Implement your get by ID logic here
            throw new NotImplementedException();
        }

        public async Task<List<ValidationModel>> GetAllAsync()
        {
            // TODO: Implement your get all logic here
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(ValidationModel validation)
        {
            // TODO: Implement your update logic here
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // TODO: Implement your delete logic here
            throw new NotImplementedException();
        }
    }

    public class DisbursementService : IDisbursementService
    {
        private readonly IDataAccess<DisbursementModel> _disbursementDataAccess;

        public DisbursementService(IDataAccess<DisbursementModel> disbursementDataAccess)
        {
            _disbursementDataAccess = disbursementDataAccess;
        }

        public async Task<int> AddAsync(DisbursementModel disbursement)
        {
            // TODO: Implement your add logic here
            throw new NotImplementedException();
        }

        public async Task<DisbursementModel> GetByIdAsync(int id)
        {
            // TODO: Implement your get by ID logic here
            throw new NotImplementedException();
        }

        public async Task<List<DisbursementModel>> GetAllAsync()
        {
            // TODO: Implement your get all logic here
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(DisbursementModel disbursement)
        {
            // TODO: Implement your update logic here
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // TODO: Implement your delete logic here
            throw new NotImplementedException();
        }
    }
}