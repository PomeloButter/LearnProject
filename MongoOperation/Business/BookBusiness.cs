using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoOperation.Models;

namespace MongoOperation.Business
{
    /// <summary>
    /// books操作层
    /// </summary>
    public class BookBusiness
    {
        private readonly IMongoCollection<Book> _books;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="settings"></param>
        public BookBusiness(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }
        /// <summary>
        /// 查询book列表
        /// </summary>
        /// <returns></returns>
        public List<Book> Get() =>
            _books.Find(book => true).ToList();

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Book Get(string id) =>
            _books.Find<Book>(book => book.Id == id).FirstOrDefault();

        /// <summary>
        /// 创建book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        /// <summary>
        /// 更新book
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bookIn"></param>
        public void Update(string id, Book bookIn) =>
            _books.ReplaceOne(book => book.Id == id, bookIn);

        /// <summary>
        /// 删除book
        /// </summary>
        /// <param name="bookIn"></param>
        public void Delete(Book bookIn) =>
            _books.DeleteOne(book => book.Id == bookIn.Id);

        /// <summary>
        /// 删除book
        /// </summary>
        /// <param name="id"></param>
        public void Delete(string id) =>
            _books.DeleteOne(book => book.Id == id);
    }
}
