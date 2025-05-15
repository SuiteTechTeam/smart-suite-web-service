using EntityFrameworkCore.CreatedUpdatedDate.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace SweetManagerIotWebService.API.IAM.Domain.Model.Aggregates
{
    public partial class Owner : IEntityWithCreatedUpdatedDate
    {
        [Column("created_at")] public DateTimeOffset? CreatedDate { get; set; }

        [Column("updated_at")] public DateTimeOffset? UpdatedDate { get; set; }
    }
}