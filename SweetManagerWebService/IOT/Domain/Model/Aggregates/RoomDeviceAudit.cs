using EntityFrameworkCore.CreatedUpdatedDate.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace SweetManagerWebService.IOT.Domain.Model.Aggregates
{
    public partial class RoomDevice : IEntityWithCreatedUpdatedDate
    {
        [Column("CreatedAt")] public DateTimeOffset? CreatedDate { get; set; }

        [Column("UpdatedAt")] public DateTimeOffset? UpdatedDate { get; set; }
    }
}