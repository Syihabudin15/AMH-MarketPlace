using AMH_MarketPlace.DTOs.TransactionDto;
using AMH_MarketPlace.DTOs.TransactionDto.Purchase;
using AMH_MarketPlace.Services.Interface.TransactionInterface.PurchaseProduct;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace AMH_MarketPlace.Controllers.TransactionController
{
    [Route("api/purchase")]
    public class TransactionController : BaseController
    {
        private readonly IPurchaseProductServie _purchaseProductServie;

        public TransactionController(IPurchaseProductServie purchaseProductServie)
        {
            _purchaseProductServie = purchaseProductServie;
        }

        [HttpPost("bank"), Authorize(Roles = "User")]
        public async Task<IActionResult> CreatePurchaseProduct([FromBody] PurchaseViaBankTransfer purchase)
        {
            var user = User.FindFirst(ClaimTypes.Email).Value;
            var result = await _purchaseProductServie.PurchaseViaBank(purchase, user);
            return Created("bank", result);
        }

        [HttpPost("gopay"), Authorize(Roles = "User")]
        public async Task<IActionResult> PurchaseViaGopay([FromBody] PurchaseViaEWallet purchase)
        {
            var user = User.FindFirst(ClaimTypes.Email).Value;
            var result = await _purchaseProductServie.PurchaseViaGopay(purchase, user);
            return Created("gopay", result);
        }
    }
}
