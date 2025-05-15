using Microsoft.EntityFrameworkCore;
using SweetManagerWebService.Communication.Domain.Model.Aggregates;
using SweetManagerWebService.Communication.Domain.Repositories;
using SweetManagerWebService.IAM.Domain.Model.Aggregates;
using SweetManagerWebService.IAM.Domain.Model.Entities.Assignments;
using SweetManagerWebService.Profiles.Domain.Model.Entities;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerWebService.Communication.Infrastructure.Persistence.EFC.Repositories
{
    public class NotificationRepository(SweetManagerContext context) : BaseRepository<Notification>(context), INotificationRepository
    {
        public async Task<IEnumerable<Notification>> FindByTypeNotificationIdAsync(int typeNotificationId)
            => await (from ntf in Context.Set<Notification>()
                join own in Context.Set<Owner>() on ntf.OwnersId equals own.Id
                join htl in Context.Set<Hotel>() on own.Id equals htl.OwnersId
                where ntf.TypesNotificationsId == typeNotificationId 
                select ntf)
                .ToListAsync();

        public async Task<IEnumerable<Notification>> FindAllByHotelIdAsync(int hotelId)
            => await (from ntf in Context.Set<Notification>()
                join ow in Context.Set<Owner>() on ntf.OwnersId equals ow.Id
                join ho in Context.Set<Hotel>() on ow.Id equals ho.OwnersId
                where ho.Id == hotelId
                select ntf)
                .ToListAsync();

        public async Task<IEnumerable<Notification>> FindAllByWorkerIdAsync(int workerId)
            => await (from wo in Context.Set<Worker>()
                join ass in Context.Set<AssignmentWorker>() on wo.Id equals ass.WorkersId
                join ad in Context.Set<Admin>() on ass.AdminsId equals ad.Id
                join noti in Context.Set<Notification>() on ad.Id equals noti.AdminsId
                where wo.Id == workerId && noti.OwnersId == null
                select noti)
                .ToListAsync();

        public async Task<IEnumerable<Notification>> FindAllByHotelIdAndExitsOwnersIdAsync(int hotelId)
        {
            var notifications = await (from no in Context.Set<Notification>()
                join ow in Context.Set<Owner>() on no.OwnersId equals ow.Id
                join ho in Context.Set<Hotel>() on ow.Id equals ho.OwnersId
                where ho.Id == hotelId
                select no)
                .ToListAsync();
            
            return notifications.Where(n => n is { OwnersId: not null, AdminsId: null, WorkersId: null });
        }
    }
}
