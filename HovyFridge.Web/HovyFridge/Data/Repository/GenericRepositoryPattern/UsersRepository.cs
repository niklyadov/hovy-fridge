﻿using HovyFridge.Data.Entity;
using HovyFridge.Data.Repository.GenericRepositoryPattern.Abstract;
using Microsoft.EntityFrameworkCore;

namespace HovyFridge.Data.Repository.GenericRepositoryPattern
{
    public class UsersRepository : BaseRepository<User, ApplicationContext>
    {
        private readonly ApplicationContext _dbContext;
        public UsersRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetByUsername(string username)
        {
            var result = await _dbContext.Users.Where(u => u.Name == username).FirstOrDefaultAsync();

            if (result == null)
                throw new Exception("User with username {username} is not found!");

            return result;
        }
    }
}