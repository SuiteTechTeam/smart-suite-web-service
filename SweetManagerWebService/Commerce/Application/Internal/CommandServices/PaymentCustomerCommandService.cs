using SweetManagerIotWebService.API.Commerce.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Commerce.Domain.Model.Commands;
using SweetManagerIotWebService.API.Commerce.Domain.Repositories;
using SweetManagerIotWebService.API.Commerce.Domain.Services;
using SweetManagerIotWebService.API.Shared.Domain.Repositories;

namespace SweetManagerIotWebService.API.Commerce.Application.Internal.CommandServices;

public class PaymentCustomerCommandService(
    IPaymentCustomerRepository paymentCustomerRepository, 
    IUnitOfWork unitOfWork) : IPaymentCustomerCommandService
{
    public async Task<PaymentCustomer?> Handle(CreatePaymentCustomerCommand command)
    {
        var paymentCustomer = new PaymentCustomer(command);
        try
        {
            await paymentCustomerRepository.AddAsync(paymentCustomer);
            await unitOfWork.CommitAsync();
            return paymentCustomer;
        } catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating the payment customer: {e.Message}");
            return null;
        }
    }

    public async Task<PaymentCustomer?> Handle(UpdatePaymentCustomerCommand command)
    {
        var paymentCustomer = await paymentCustomerRepository.FindByIdAsync(command.Id);
        if (paymentCustomer == null)
        {
            Console.WriteLine($"Payment Customer with ID {command.Id} not found.");
            return null;
        }

        var newPaymentCustomer = new PaymentCustomer(command);

        try
        {
            paymentCustomerRepository.Remove(paymentCustomer);
            await paymentCustomerRepository.AddAsync(newPaymentCustomer);
            await unitOfWork.CommitAsync();
            return newPaymentCustomer;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while updating the payment customer: {e.Message}");
            return null;
        }
    }
}