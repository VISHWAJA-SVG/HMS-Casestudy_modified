
// using AutoMapper;
// using HotelManagement_backend.DomainModels;
// using HotelManagement_backend.Repositories;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;

// namespace HotelManagement_backend.Controllers
// {
//     [ApiController]
//    // [Authorize(Roles ="Manager")]
//     public class InventoriesController : Controller
//     {
//         private readonly IInventoriesRepository inventoriesRepository;
//         private readonly IMapper mapper;
//         public InventoriesController(IInventoriesRepository inventoriesRepository,IMapper mapper)
//         {
//             this.inventoriesRepository = inventoriesRepository;
//             this.mapper = mapper;
//         }

//         [HttpGet]
//         [Route("[controller]")]
//         public async Task<IActionResult> GetAllInventoriesAsync()
//         {
//             var inventories = await inventoriesRepository.GetInventoriesAsync(); 
//             return Ok(mapper.Map<List<Inventory>>(inventories));
//         }

//         [HttpGet]
//         [Route("[controller]/{inventoryId:int}"),ActionName("GetInventoryAsync")]
//         public async Task<IActionResult> GetInventoryAsync([FromRoute] int inventoryId)
//         {
//             var inventory = await inventoriesRepository.GetInventoryAsync(inventoryId);
//             if(inventory == null) 
//             {
//                 return NotFound();
//             }
//             return Ok(mapper.Map<Inventory>(inventory));
//         }

//         [HttpPut]
//         [Route("[controller]/{inventoryId:int}")]
//         public async Task<IActionResult> UpdateInventoryAsync([FromRoute] int inventoryId, [FromBody] UpdateInventoryRequest request)
//         {

//             if (await inventoriesRepository.Exists(inventoryId))
//             {
//                 var updatedguest = await inventoriesRepository.UpdateInventoryAsync(inventoryId, mapper.Map<DataModels.Inventory>(request));
//                 if (updatedguest != null)
//                 {
//                     return Ok(mapper.Map<Inventory>(updatedguest));
//                 }
//             }
//             return NotFound();
//         }

//         [HttpDelete]
//         [Route("[controller]/{inventoryId:int}")]
//         public async Task<IActionResult> DeleteInventoryAsync([FromRoute]int inventoryId)
//         {
//             if (await inventoriesRepository.Exists(inventoryId))
//             {
//                 var inventory = await inventoriesRepository.DeleteInventoryAsync(inventoryId);
//                 return Ok(mapper.Map<Inventory>(inventory));
//             }
//             return NotFound();
//         }

//         [HttpPost]
//         [Route("[controller]/Add")]
//         public async Task<IActionResult> AddInventoryAsync([FromBody] AddInventoryRequest request)
//         {
//             var inventory = await inventoriesRepository.AddInventoryAsync(mapper.Map<DataModels.Inventory>(request));
//             return CreatedAtAction(nameof(GetInventoryAsync), new { inventoryId = inventory.InventoryId },
//                 mapper.Map<Inventory>(inventory));
//         }
//     }
// }
