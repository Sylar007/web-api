using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApi.Entities;

namespace WebApi.Helpers
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<User> Users { get; set; }
        public virtual DbSet<current_nav_equipment> current_nav_equipment { get; set; }
        public virtual DbSet<equipment> equipments { get; set; }
        public virtual DbSet<equipment_field> equipment_field { get; set; }
        public virtual DbSet<equipment_file> equipment_file { get; set; }
        public virtual DbSet<equipment_link> equipment_link { get; set; }
        public virtual DbSet<equipment_model> equipment_model { get; set; }
        public virtual DbSet<equipment_model_field> equipment_model_field { get; set; }
        public virtual DbSet<equipment_model_file> equipment_model_file { get; set; }
        public virtual DbSet<equipment_model_link> equipment_model_link { get; set; }
        public virtual DbSet<equipment_model_part> equipment_model_part { get; set; }
        public virtual DbSet<equipment_model_part_model> equipment_model_part_model { get; set; }
        public virtual DbSet<equipment_part> equipment_part { get; set; }
        public virtual DbSet<equipment_status> equipment_status { get; set; }
        public virtual DbSet<equipment_type> equipment_type { get; set; }
        public virtual DbSet<estimated_nav> estimated_nav { get; set; }
        public virtual DbSet<media> media { get; set; }
        public virtual DbSet<part> parts { get; set; }
        public virtual DbSet<part_field> part_field { get; set; }
        public virtual DbSet<part_file> part_file { get; set; }
        public virtual DbSet<part_link> part_link { get; set; }
        public virtual DbSet<part_model> part_model { get; set; }
        public virtual DbSet<policy> policies { get; set; }
        public virtual DbSet<qrcode_link> qrcode_link { get; set; }
        public virtual DbSet<wo_type> wo_type { get; set; }
        public virtual DbSet<work_order> work_order { get; set; }
        public virtual DbSet<wo_link> wo_link { get; set; }        
        public virtual DbSet<wo_file> wo_file { get; set; }        
        public virtual DbSet<wo_action> wo_action { get; set; }
        public virtual DbSet<wo_comment> wo_comment { get; set; }
        public virtual DbSet<wo_part> wo_part { get; set; }
        public virtual DbSet<wo_priority> wo_priority { get; set; }
        public virtual DbSet<wo_status> wo_status { get; set; }
        public virtual DbSet<wo_task_check> wo_task_check { get; set; }
        public virtual DbSet<wo_task_sub> wo_task_sub { get; set; }
        public virtual DbSet<wo_task_sub_file> wo_task_sub_file { get; set; }
        

    }
}