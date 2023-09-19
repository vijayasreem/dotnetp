using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DataAccess;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public class EndorsementService : IEndorsementRepository
    {
        private readonly ILogger _logger;
        private readonly IEndorsementDataAccess _dataAccess;

        public EndorsementService(ILogger logger, IEndorsementDataAccess dataAccess)
        {
            _logger = logger;
            _dataAccess = dataAccess;
        }

        public async Task<int> CreateAsync(EndorsementModel endorsement)
        {
            try
            {
                // Perform create operation
                int createdId = await _dataAccess.CreateAsync(endorsement);

                // Log the create action
                await LogActionAsync("Create", endorsement.Id, endorsement.UserId, DateTime.Now);

                return createdId;
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while creating endorsement");

                throw;
            }
        }

        public async Task<EndorsementModel> GetByIdAsync(int id)
        {
            try
            {
                // Perform get by id operation
                EndorsementModel endorsement = await _dataAccess.GetByIdAsync(id);

                // Log the get by id action
                await LogActionAsync("GetById", endorsement?.Id, endorsement?.UserId, DateTime.Now);

                return endorsement;
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, $"Error occurred while getting endorsement with ID {id}");

                throw;
            }
        }

        public async Task<IEnumerable<EndorsementModel>> GetAllAsync()
        {
            try
            {
                // Perform get all operation
                IEnumerable<EndorsementModel> endorsements = await _dataAccess.GetAllAsync();

                // Log the get all action
                await LogActionAsync("GetAll", null, null, DateTime.Now);

                return endorsements;
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while getting all endorsements");

                throw;
            }
        }

        public async Task UpdateAsync(EndorsementModel endorsement)
        {
            try
            {
                // Perform update operation
                await _dataAccess.UpdateAsync(endorsement);

                // Log the update action
                await LogActionAsync("Update", endorsement.Id, endorsement.UserId, DateTime.Now);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, $"Error occurred while updating endorsement with ID {endorsement.Id}");

                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                // Perform delete operation
                await _dataAccess.DeleteAsync(id);

                // Log the delete action
                await LogActionAsync("Delete", id, null, DateTime.Now);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, $"Error occurred while deleting endorsement with ID {id}");

                throw;
            }
        }

        private async Task LogActionAsync(string action, int? endorsementId, string userId, DateTime timestamp)
        {
            try
            {
                // Create a log entry
                LogModel log = new LogModel
                {
                    Action = action,
                    EndorsementId = endorsementId,
                    UserId = userId,
                    Timestamp = timestamp
                };

                // Store the log entry in the designated database table using the data access layer
                await _dataAccess.LogActionAsync(log);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while logging action");

                throw;
            }
        }
    }
}