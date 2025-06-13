using EntityFrameworkCore.CreatedUpdatedDate.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace SweetManagerWebService.IOT.Domain.Model.Entities
{
    public partial class NotificationHistory : IEntityWithCreatedUpdatedDate
    {
        [Column("CreatedAt")] public DateTimeOffset? CreatedDate { get; set; }

        [Column("UpdatedAt")] public DateTimeOffset? UpdatedDate { get; set; }
    }
}