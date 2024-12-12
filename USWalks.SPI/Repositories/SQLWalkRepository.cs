using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using USWalks.SPI.Data;
using USWalks.SPI.Models.Domain;
using USWalks.SPI.Models.DTO;

namespace USWalks.SPI.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly USWalksDbContext _dbContext;
        public SQLWalkRepository(USWalksDbContext dbContext)
        {
            _dbContext = dbContext;            
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await _dbContext.Walks.AddAsync(walk);
            await _dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            var walksDomainModel = await _dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
            return walksDomainModel;
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
           var existingModel = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingModel == null)
            {
                return null;
            }
            existingModel.Difficulty = walk.Difficulty;
            existingModel.Name = walk.Name;
            existingModel.WalkImageUrl = walk.WalkImageUrl;
            existingModel.Description = walk.Description;
            existingModel.Region = walk.Region;
            existingModel.LengthInKm = walk.LengthInKm;
            existingModel.RegionId = walk.RegionId;
            existingModel.DifficultyId = walk.DifficultyId;

            await _dbContext.SaveChangesAsync();
            return existingModel;

        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingModel = await _dbContext.Walks.FirstOrDefaultAsync(x=>x.Id == id);
            if (existingModel == null)
            { return null; }
            _dbContext.Walks.Remove(existingModel);
            await _dbContext.SaveChangesAsync();
            return existingModel;
        }
    }
}
