// using HotelManagement_backend.DataModels;
// using Microsoft.EntityFrameworkCore;
// namespace HotelManagement_backend.Repositories
// {
//     public class InventoriesRepository : IInventoryRepository
//     {
//         private readonly HotelDbContext context;
//         public InventoriesRepository(HotelDbContext context)
//         {
//             this.context = context;
//         }

//         public async Task<List<Inventory>> GetInventoriesAsync()
//         {
//             return await context.Inventories.ToListAsync();
//         }

//         public async Task<Inventory> GetInventoryAsync(int inventoryId)
//         {
//             return await context.Inventories.FirstOrDefaultAsync(x => x.InventoryId == inventoryId);
//         }

//         public async Task<bool> Exists(int inventoryId)
//         {
//             return await context.Inventories.AnyAsync(x => x.InventoryId == inventoryId); 
//         }

//         public async Task<Inventory> UpdateInventoryAsync(int inventoryId, Inventory request)
//         {
//             var existingInventory = await GetInventoryAsync(inventoryId);
//             if (existingInventory != null)
//             {
//                 existingInventory.InventoryName = request.InventoryName;
//                 existingInventory.Quantity = request.Quantity;
               

//                 await context.SaveChangesAsync();
//                 return existingInventory;
//             }
//             return null;
//         }

//         public async Task<Inventory> DeleteInventoryAsync(int inventoryId)
//         {
//             var inventory = await GetInventoryAsync(inventoryId);
//             if (inventory != null)
//             {
//                 context.Inventories.Remove(inventory);
//                 await context.SaveChangesAsync();
//                 return inventory;
//             }
//             return null;
//         }

//         public async Task<Inventory> AddInventoryAsync(Inventory request)
//         {
//             var inventory = await context.Inventories.AddAsync(request);
//             await context.SaveChangesAsync();
//             return inventory.Entity;
//         }
//     }
// }
