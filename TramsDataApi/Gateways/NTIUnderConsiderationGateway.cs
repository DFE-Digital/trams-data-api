﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TramsDataApi.DatabaseModels;

namespace TramsDataApi.Gateways
{
	public class NTIUnderConsiderationGateway : INTIUnderConsiderationGateway
    {
        private readonly TramsDbContext _tramsDbContext;
        private readonly ILogger<NTIUnderConsiderationGateway> _logger;

        public NTIUnderConsiderationGateway(TramsDbContext tramsDbContext, ILogger<NTIUnderConsiderationGateway> logger)
		{
            _tramsDbContext = tramsDbContext;
            _logger = logger;
        }

        public async Task<NTIUnderConsideration> CreateNTIUnderConsideration(NTIUnderConsideration request)
        {
            try
            {
                _tramsDbContext.NTIUnderConsiderations.Add(request);
                await _tramsDbContext.SaveChangesAsync();
                return request;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Failed to create NTIUnderConsideration with Id {Id}, {ex}", request.Id, ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("An application exception has occurred whilst creating NTIUnderConsideration with Id {Id}, {ex}", request.Id, ex);
                throw;
            }
        }

        public async Task<ICollection<NTIUnderConsideration>> GetNTIUnderConsiderationByCaseUrn(int caseUrn)
        {
            var considerations = await _tramsDbContext.NTIUnderConsiderations.Where(n => n.CaseUrn == caseUrn).ToListAsync();
            var reasonMappings = await _tramsDbContext.NTIUnderConsiderationReasonMappings.ToListAsync();

            considerations = considerations.Select(consideration =>
            {
                consideration.UnderConsiderationReasonsMapping = reasonMappings.Where(r => r.NTIUnderConsiderationId == consideration.Id).ToList();
                return consideration;
            }).ToList();

            return considerations;
        }

        public async Task<NTIUnderConsideration> GetNTIUnderConsiderationById(long ntiUnderConsiderationId)
        {
            try
            {
                var consideration = await _tramsDbContext.NTIUnderConsiderations.SingleOrDefaultAsync(n => n.Id == ntiUnderConsiderationId);
                var reasonMappings = await  _tramsDbContext.NTIUnderConsiderationReasonMappings.Where(r => r.NTIUnderConsiderationId == ntiUnderConsiderationId).ToListAsync();
                consideration.UnderConsiderationReasonsMapping = reasonMappings;

                return consideration;
            }
            catch (InvalidOperationException iox)
            {
                _logger.LogError(iox, "Multiple NTIUnderConsiderations records found with Id: {Id}", ntiUnderConsiderationId);
                throw;
            }
        }

        public async Task<NTIUnderConsideration> PatchNTIUnderConsideration(NTIUnderConsideration patchNTIUnderConsideration)
        {
            try
            {
                var reasonsToRemove = await _tramsDbContext.NTIUnderConsiderationReasonMappings.Where(r => r.NTIUnderConsiderationId == patchNTIUnderConsideration.Id).ToListAsync();
                _tramsDbContext.NTIUnderConsiderationReasonMappings.RemoveRange(reasonsToRemove);

                var tracked = _tramsDbContext.Update<NTIUnderConsideration>(patchNTIUnderConsideration);
                await _tramsDbContext.SaveChangesAsync();

                return tracked.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while trying to patch the NTI underconsideration with Id:", patchNTIUnderConsideration.Id);
                throw;
            }
        }

        public async Task<List<NTIUnderConsiderationStatus>> GetAllStatuses()
        {
            return await _tramsDbContext.NTIUnderConsiderationStatuses.ToListAsync();
        }

        public async Task<List<NTIUnderConsiderationReason>> GetAllReasons()
        {
            return await _tramsDbContext.NTIUnderConsiderationReasons.ToListAsync();
        }
    }
}

