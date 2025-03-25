using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using DebtShare.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DebtShare.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<ConfirmationStatus> ConfirmationStatuses { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<GroupUser> GroupUser { get; set; }

        public DbSet<GroupUserExpenseConfirmation> GroupUserExpenseConfirmations { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<ItemExpense> ItemExpenses { get; set; }

        public DbSet<ItemExpenseGrpoupUser> ItemExpenseGrpoupUsers { get; set; }

        public DbSet<Merchant> Merchants { get; set; }

        public DbSet<Payment> Payment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Place any Fluent configuration here

            // Payment (две FK към ApplicationUser)
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Payer)
                .WithMany()
                .HasForeignKey(p => p.PayerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Receiver)
                .WithMany()
                .HasForeignKey(p => p.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            // Expense (две FK към ApplicationUser)
            modelBuilder.Entity<Expense>()
                .HasOne(e => e.Payer)
                .WithMany()
                .HasForeignKey(e => e.PayerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Expense>()
                .HasOne(e => e.Creator)
                .WithMany()
                .HasForeignKey(e => e.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Group (една FK към ApplicationUser)
            modelBuilder.Entity<Group>()
                .HasOne(g => g.Creator)
                .WithMany()
                .HasForeignKey(g => g.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            // GroupUser (една FK към ApplicationUser)
            modelBuilder.Entity<GroupUser>()
                .HasOne(gu => gu.User)
                .WithMany()
                .HasForeignKey(gu => gu.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GroupUserExpenseConfirmation>()
                .HasOne(guec => guec.GroupUser)
                .WithMany()
                .HasForeignKey(guec => guec.GroupUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GroupUserExpenseConfirmation>()
                .HasOne(guec => guec.Expense)
                .WithMany()
                .HasForeignKey(guec => guec.ExpenseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GroupUserExpenseConfirmation>()
                .HasOne(guec => guec.ConfirmationStatus)
                .WithMany()
                .HasForeignKey(guec => guec.ConfirmationStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemExpenseGrpoupUser>()
                .HasOne(x => x.ItemExpense)
                .WithMany()
                .HasForeignKey(x => x.ItemExpenseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemExpenseGrpoupUser>()
                .HasOne(x => x.GroupUser)
                .WithMany()
                .HasForeignKey(x => x.GroupUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
