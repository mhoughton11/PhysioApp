using DIYHIIT.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DIYHIIT.Data.Database
{
    public class BaseDatabase<T> : IBaseDatabase<T> where T : IBaseModel, new()
    {
        readonly SQLiteAsyncConnection _database;

        public BaseDatabase(string _dbPath, string name)
        {
            Name = name;

            _database = new SQLiteAsyncConnection(_dbPath);
            _database.CreateTableAsync<T>().Wait();
        }

        public string Name { get; set; }

        /// <summary>
        /// Delete all items in the database.
        /// </summary>
        /// <returns>Number of items deleted.</returns>
        public async Task<int> ClearItemsAsync()
        {
            return await _database.DeleteAllAsync<T>();
        }

        /// <summary>
        /// Delete an item in the database.
        /// </summary>
        /// <param name="item">Item to be deleted.</param>
        /// <returns>Number of rows deleted.</returns>
        public Task<int> DeleteItemAsync(T item)
        {
            return _database.DeleteAsync(item);
        }

        /// <summary>
        /// Retrieve an item from the database using the ID field.
        /// </summary>
        /// <param name="id">The ID of the desired item</param>
        /// <returns>The instance of the desired item.</returns>
        public Task<T> GetItemAsync(int id)
        {
            return _database.Table<T>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Retrieve an item from the database using the Name field.
        /// </summary>
        /// <param name="name">The name of the desired item</param>
        /// <returns>The instance of the desired item.</returns>
        public async Task<T> GetItemAsync(string name)
        {
            T item = await _database.Table<T>()
                            .Where(i => i.Name == name)
                            .FirstOrDefaultAsync();

            Debug.WriteLine($"Pulling object from: {Name}");
            item.DebugObject();

            return item;
        }

        /// <summary>
        /// Count the number of items currently in the exercise database.
        /// </summary>
        /// <returns>Number of items in table.</returns>
        public async Task<int> GetItemCount()
        {
            return await _database.Table<T>().CountAsync();
        }

        /// <summary>
        /// Get a list of all the items stored in the Database.
        /// </summary>
        /// <returns>List of items.</returns>
        public Task<List<T>> GetItemsAsync()
        {
            return _database.Table<T>().ToListAsync();
        }

        /// <summary>
        /// Get a list of items with the specified type.
        /// </summary>
        /// <param name="type">The type of the desired items.</param>
        /// <returns>List of items with the desired type.</returns>
        public Task<List<T>> GetItemsAsync(WorkoutType? type)
        {
            if (type == WorkoutType.All || type == null)
            {
                return _database.Table<T>().ToListAsync();
            }
            else
            {
                return _database.Table<T>()
                                .Where(i => i.Type == type)
                                .ToListAsync();
            }
            
        }

        /// <summary>
        /// Save an item to the database.
        /// </summary>
        /// <param name="item">Item to be added.</param>
        /// <returns>The number of rows added to the database.</returns>
        public Task<int> SaveItemAsync(T item)
        {
            try
            {
                if (item.ID != 0)
                {
                    return _database.UpdateAsync(item);
                }
                else
                {
                    return _database.InsertAsync(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Couldn't save item in db: {Name}");
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Save a list of items to the database.
        /// </summary>
        /// <param name="items">List of items to be added.</param>
        /// <returns>The number of rows added to the database table.</returns>
        public async Task<int> SaveItemsAsync(IList<T> items)
        {          
            int rows = 0;
            foreach (var item in items)
            {
                if (item != null)
                    rows += await SaveItemAsync(item);
            }

            return rows;
        }

        /// <summary>
        /// Save a collection of items to the database.
        /// </summary>
        /// <param name="exercises">Collection of items to be added.</param>
        /// <returns>The number of rows added to the database table.</returns>
        public async Task<int> SaveItemsAsync(ICollection<T> items)
        {
            int rows = 0;
            foreach (var item in items)
            {
                if (item != null)
                    rows += await SaveItemAsync(item);
            }

            return rows;
        }
    }
}
