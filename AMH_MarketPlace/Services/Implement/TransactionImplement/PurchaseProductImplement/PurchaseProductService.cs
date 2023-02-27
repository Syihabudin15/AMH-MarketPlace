using AMH_MarketPlace.DTOs.TransactionDto;
using AMH_MarketPlace.DTOs.TransactionDto.Purchase;
using AMH_MarketPlace.DTOs.TransactionDto.Purchase.Utils;
using AMH_MarketPlace.DTOs.UserDto;
using AMH_MarketPlace.Entities.Transaction;
using AMH_MarketPlace.Entities.Transaction.TransacPurchase;
using AMH_MarketPlace.Repositories;
using AMH_MarketPlace.Services.Interface.StoreInterface.ProductInterface;
using AMH_MarketPlace.Services.Interface.TransactionInterface;
using AMH_MarketPlace.Services.Interface.TransactionInterface.PurchaseProduct;
using AMH_MarketPlace.Services.Interface.UserInterface;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace AMH_MarketPlace.Services.Implement.TransactionImplement.PurchaseProductImplement
{
    public class PurchaseProductService : IPurchaseProductServie
    {
        private readonly IRepository<PurchaseProduct> _repository;
        private readonly IDbPersistence _dbPersistence;
        private readonly ITransactionService _transactionService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly string _charge_bank = "https://api.sandbox.midtrans.com/v2/charge";

        public PurchaseProductService(
            IRepository<PurchaseProduct> repository,
            IDbPersistence dbPersistence,
            IUserService userService,
            IProductService productService,
            ITransactionService transactionService)
        {
            _repository = repository;
            _dbPersistence = dbPersistence;
            _userService = userService;
            _productService = productService;
            _transactionService = transactionService;
        }

        public async Task<object> PurchaseViaBank(PurchaseViaBankTransfer purchase, string email)
        {
            try
            {
                var user = await _userService.GetMyUser(email);
                var data = await CreateRequestTransaction("bank_transfer", purchase.Product, user, purchase.BankName);

                using var client = new HttpClient();
                var reqMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(_charge_bank),
                    Headers =
                    {
                        { "accept", "application/json" },
                        { "authorization", "Basic U0ItTWlkLXNlcnZlci1Mei1hTGlXNjVONXFlNlRZM0dpM3p6U006" }
                    },
                    Content = new StringContent(data, Encoding.UTF8)
                    {
                        Headers =
                        {
                            ContentType = new MediaTypeHeaderValue("application/json")
                        }
                    }
                };

                var result = await client.SendAsync(reqMessage);
                var response = result.Content.ReadAsStringAsync();
                var resultResponse = JsonConvert.DeserializeObject<TransactionResponse>(response.Result);
                resultResponse.userId = user.Id;
                await SaveTransaction(resultResponse, purchase.Product);
                return response.Result;
            }
            catch (Exception)
            {
                throw new Exception("Error while Transaction via Bank Transfer");
            }
        }

        public async Task<object> PurchaseViaGopay(PurchaseViaEWallet purchase, string email)
        {
            try
            {
                var user = await _userService.GetMyUser(email);
                var data = await CreateRequestTransaction("gopay", purchase.Products, user, null);

                using var client = new HttpClient();
                var reqMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(_charge_bank),
                    Headers =
                    {
                        { "accept", "application/json" },
                        { "authorization", "Basic U0ItTWlkLXNlcnZlci1Mei1hTGlXNjVONXFlNlRZM0dpM3p6U006" }
                    },
                    Content = new StringContent(data, Encoding.UTF8)
                    {
                        Headers =
                        {
                            ContentType = new MediaTypeHeaderValue("application/json")
                        }
                    }
                };

                var result = await client.SendAsync(reqMessage);
                var response = result.Content.ReadAsStringAsync();
                var resultResponse = JsonConvert.DeserializeObject<TransactionResponse>(response.Result);
                resultResponse.userId = user.Id;
                await SaveTransaction(resultResponse, purchase.Products);
                return response.Result;
            }
            catch (Exception)
            {
                throw new Exception("Error While Transaction via Gopay");
            }
        }

        /*
         * Helper Method
         */
        private async Task<string> CreateRequestTransaction(string payType, List<UtilProduct> products, UserResponse customer, string? bankName)
        {
            long grossAmnt = 0;
            List<object> listProduct = new List<object>();
            
            foreach(var p in products)
            {
                var product = await _productService.GetForTransaction(p.Id);
                grossAmnt += product.Price * p.Quantity;
                object tmp = new
                {
                    id = product.Id.ToString(),
                    price = product.Price,
                    quantity = p.Quantity,
                    name = product.Name
                };
                listProduct.Add(tmp);
            }
            var name = customer.Name.Split(" ");

            switch(payType)
            {
                case "bank_transfer":
                    object tmpReqBank = new
                    {
                        payment_type = payType,
                        transaction_details = new
                        {
                            order_id = UtilTools.UtilTools.GenerateOrderId(),
                            gross_amount = grossAmnt
                        },
                        customer_details = new
                        {
                            email = customer.Email,
                            first_name = name[0],
                            last_name = name[1],
                            phone = customer.Name
                        },
                        item_details = listProduct,
                        bank_transfer = new
                        {
                            bank = bankName
                        }
                    };
                    var dataBank = JsonConvert.SerializeObject(tmpReqBank);

                    return dataBank;
                case "gopay":
                    object tmpReqGopay = new
                    {
                        payment_type = payType,
                        transaction_details = new
                        {
                            order_id = UtilTools.UtilTools.GenerateOrderId(),
                            gross_amount = grossAmnt
                        },
                        customer_details = new
                        {
                            email = customer.Email,
                            first_name = name[0],
                            last_name = name[1],
                            phone = customer.Name
                        },
                        item_details = listProduct
                    };
                    var dataGopay = JsonConvert.SerializeObject(tmpReqGopay);
                    return dataGopay;
                default:
                    throw new Exception("Error While Create Request Transaction");
            }
        }

        private async Task SaveTransaction(TransactionResponse response, List<UtilProduct> products)
        {
            var transacPersistence =
                await _dbPersistence.ExecuteTransactionAsync(async () =>
                {
                    List<PurchaseProduct> purchaseProducts = new List<PurchaseProduct>();
                    var saveTransac = await _transactionService.CreateTransaction(new Transaction
                    {
                        Id = Guid.Parse(response.transaction_id),
                        TransactionDate = DateTime.Now,
                        UserId = Guid.Parse(response.userId),
                        PaymentMethod = response.payment_type,
                        Description = response.status_message,
                        ReferencePg = response.merchant_id,
                        Status = response.transaction_status,
                        OrderId = response.order_id
                    });
                    foreach(var p in products)
                    {
                        var tmpProduct = new PurchaseProduct
                        {
                            TransactionId = saveTransac.Id,
                            ProductId = Guid.Parse(p.Id),
                            Quantity = p.Quantity
                        };
                        purchaseProducts.Add(tmpProduct);
                    };

                    var savePurchaseProduct = await _repository.SaveAll(purchaseProducts);
                    await _dbPersistence.SaveChangesAsync();
                    return savePurchaseProduct;
                });
        }
    }
}
