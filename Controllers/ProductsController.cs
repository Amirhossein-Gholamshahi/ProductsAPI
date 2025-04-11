using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ProductsAPI.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using MongoDB.Bson;
using System.Reflection;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
	private readonly IMongoCollection<Product> _collection;

	public ProductsController(IMongoDatabase database)
	{
		_collection = database.GetCollection<Product>("Products");
	}

	[HttpGet]
	public IActionResult Get()
	{
		var products = _collection.Find(_ => true).ToList();
		return Ok(products);
	}

	[HttpGet("SearchByName")]
	public IActionResult Get(string name)
	{
		var products = _collection.Find(p => p.Name == name).ToList(); 
		return Ok(products);
	}

	[HttpPost]
	public IActionResult Post(JObject product)
	{
		if (product == null)
		{
			return BadRequest("Product data is null.");
		}
		try
		{
			var newProductObj = JsonConvert.DeserializeObject<Product>(product.ToString());
			_collection.InsertOne(newProductObj);
			return Ok();
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"Error Inserting Document: {ex.Message}");
		}
	}

	[HttpPut]
	public IActionResult Put(string id , JObject Updatedproduct)
	{
		if (Updatedproduct == null)
		{
			return BadRequest("Product data is null.");
		}
		try
		{
			var updatedproductObj = JsonConvert.DeserializeObject<Product>(Updatedproduct.ToString());
			updatedproductObj.ProductId = id;
			_collection.ReplaceOne(p => p.ProductId == id, updatedproductObj);
			return Ok();
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"Error Updating Document: {ex.Message}");
		}
	}

	[HttpPatch]
	public IActionResult Patch(string id, JObject patchData)
	{
		if (!ObjectId.TryParse(id, out _))
			return BadRequest("Invalid ObjectId");

		var filter = Builders<Product>.Filter.Eq(p => p.ProductId, id);
		var existing = _collection.Find(filter).FirstOrDefault();

		if (existing == null)
			return NotFound();

		foreach (var prop in patchData.Properties())
		{
			var propInfo = typeof(Product).GetProperty(prop.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
			if (propInfo != null && propInfo.CanWrite)
			{
				object value = prop.Value.ToObject(propInfo.PropertyType);
				propInfo.SetValue(existing, value);
			}
		}

	   _collection.ReplaceOne(filter, existing);

		return Ok(existing);
	}


	[HttpDelete]
	public IActionResult Delete(string id)
	{
		if (string.IsNullOrEmpty(id))
		{
			return BadRequest("Id Not Specified.");
		}
		try
		{
			var filter = Builders<Product>.Filter.Eq(p => p.ProductId, id);
			_collection.DeleteOne(filter);
			return Ok();
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"Error Deleting Document: {ex.Message}");
		}
	}

}
