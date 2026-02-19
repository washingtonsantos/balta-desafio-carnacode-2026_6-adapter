using Adapter.Legacy;

namespace Adapter.Payment
{
    public class PaymentAdapter : IPaymentProcessor
    {
        private readonly LegacyPaymentSystem _legacyPaymentSystem;

        public PaymentAdapter(LegacyPaymentSystem legacyPaymentSystem)
        {
            _legacyPaymentSystem = legacyPaymentSystem;
        }

        public PaymentStatus CheckStatus(string transactionId)
        {
            var status = _legacyPaymentSystem.QueryTransactionStatus(transactionId);

            return Enum.Parse<PaymentStatus>(status);
        }

        public PaymentResult ProcessPayment(PaymentRequest request)
        {
            var response = _legacyPaymentSystem.AuthorizeTransaction(
                cardNum: request.CreditCardNumber,
                cvvCode: int.Parse(request.Cvv), 
                expMonth: request.ExpirationDate.Month, 
                expYear: request.ExpirationDate.Year,
                amountInCents: (double)Math.Round(request.Amount * 100M, 0),
                customerInfo: request.CustomerEmail);

            return new PaymentResult
            {
                Success = response.ResponseCode == "00",
                Message = response.ResponseMessage,
                TransactionId = response.TransactionRef
            };
        }

        public bool RefundPayment(string transactionId, decimal amount)
        {
            return _legacyPaymentSystem.ReverseTransaction(transactionId, (double)amount);
        }
    }
}
