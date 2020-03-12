namespace MultiBuffer.WebApi.DataModel
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MultiBufferContext : DbContext
    {

        public MultiBufferContext()
            : base("MultiBufferContext")
        {
        }

        public DbSet<BufferItem> BufferItems { get; set; }
    }

}