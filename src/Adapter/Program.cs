using Adapter.Legacy;
using Adapter.Payment;
using Adapter.Service;

Console.WriteLine("=== Sistema de Checkout ===\n");

// Funciona bem com o processador moderno
var modernProcessor = new ModernPaymentProcessor();
var checkoutWithModern = new CheckoutService(modernProcessor);
checkoutWithModern.CompleteOrder("cliente@email.com", 150.00m, "4111111111111111");

Console.WriteLine("\n" + new string('-', 60) + "\n");

var legacySystem = new LegacyPaymentSystem();
var processor = new PaymentAdapter(legacySystem);
var checkoutWithLegacy = new CheckoutService(modernProcessor);
checkoutWithLegacy.CompleteOrder("cliente2@email.com", 200.00m, "4111111111111111");