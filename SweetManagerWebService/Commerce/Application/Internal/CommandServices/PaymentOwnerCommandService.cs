using SweetManagerIotWebService.API.Commerce.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Commerce.Domain.Model.Commands;
using SweetManagerIotWebService.API.Commerce.Domain.Repositories;
using SweetManagerIotWebService.API.Commerce.Domain.Services;
using SweetManagerIotWebService.API.Shared.Domain.Repositories;

namespace SweetManagerIotWebService.API.Commerce.Application.Internal.CommandServices;

public class PaymentOwnerCommandService(
    IPaymentOwnerRepository paymentOwnerRepository,
    IUnitOfWork unitOfWork) : IPaymentOwnerCommandService
{
    public async Task<PaymentOwner?> Handle(CreatePaymentOwnerCommand command)
    {
        var paymentOwner = new PaymentOwner(command);
        try
        {
            await paymentOwnerRepository.AddAsync(paymentOwner);
            await unitOfWork.CommitAsync();
            return paymentOwner;
        } catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating the payment owner: {e.Message}");
            return null;
        }
    }

    public async Task<PaymentOwner?> Handle(UpdatePaymentOwnerCommand command)
    {
        var paymentOwner = await paymentOwnerRepository.FindByIdAsync(command.Id);
        if (paymentOwner == null)
        {
            Console.WriteLine($"Payment Owner with ID {command.Id} not found.");
            return null;
        }

        var newPaymentOwner = new PaymentOwner(command);

        try
        {
            paymentOwnerRepository.Remove(paymentOwner);
            await paymentOwnerRepository.AddAsync(newPaymentOwner);
            await unitOfWork.CommitAsync();
            return newPaymentOwner;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while updating the payment customer: {e.Message}");
            return null;
        }
    }
}