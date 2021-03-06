﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RensyuRensyu.Entities;
using System.Data;
using System.Threading.Tasks;

namespace RensyuRensyu.Infrastructure.Database
{
	public class ApplicationDbContext : DbContext
    {
		private IDbContextTransaction _currentTransaction;
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
		}

		public DbSet<User> Users { get; set; }
		public DbSet<TestData> TestDatas { get; set; }
		public DbSet<Crud> Cruds { get; set; }
		public DbSet<WorldEntity> Worlds { get; set; }
		public DbSet<Company> Companies { get; set; }
		public DbSet<UserAuthority> UserAuthorities { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// ユーザ名はユニーク制約を付ける
			modelBuilder.Entity<User>()
				.HasIndex(m => m.Name)
				.IsUnique();

			// ユニーク制約を付ける
			modelBuilder.Entity<WorldEntity>()
				.HasIndex(x => new { x.Name }).IsUnique();
		}

		#region トランザクション処理のラップ
		public async Task BeginTransactionAsync()
		{
			if (_currentTransaction != null)
			{
				return;
			}
			_currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted).ConfigureAwait(false);
		}

		public async Task CommitTransactionAsync()
		{
			try
			{
				await SaveChangesAsync().ConfigureAwait(false);

				_currentTransaction?.Commit();
			}
			catch
			{
				RollbackTransaction();
				throw;
			}
			finally
			{
				if (_currentTransaction != null)
				{
					_currentTransaction.Dispose();
					_currentTransaction = null;
				}
			}
		}

		public void RollbackTransaction()
		{
			try
			{
				_currentTransaction?.Rollback();
			}
			finally
			{
				if (_currentTransaction != null)
				{
					_currentTransaction.Dispose();
					_currentTransaction = null;
				}
			}
		}
		#endregion
	}
}
