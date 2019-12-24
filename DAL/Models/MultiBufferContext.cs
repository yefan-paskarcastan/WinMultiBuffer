namespace DAL.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MultiBufferContext : DbContext
    {
        public MultiBufferContext()
            : base("name=MultiBufferContext")
        {
        }

        public virtual DbSet<tblClipboards> tblClipboards { get; set; }
        public virtual DbSet<tblUsers> tblUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblUsers>()
                .HasMany(e => e.tblClipboards)
                .WithRequired(e => e.tblUsers)
                .HasForeignKey(e => e.idUser)
                .WillCascadeOnDelete(false);
        }
    }
}
