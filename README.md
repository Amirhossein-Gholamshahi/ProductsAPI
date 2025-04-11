# ProductsAPI

A simple ASP.NET Core Web API that provides full CRUD operations on a MongoDB collection of `Product` entities. This project demonstrates how to integrate MongoDB with an ASP.NET Core API using `MongoDB.Driver` and work with dynamic JSON data via `Newtonsoft.Json`.

---

## 🛠 Tech Stack

- ASP.NET Core
- MongoDB
- MongoDB.Driver
- Newtonsoft.Json

---

## 📦 Features

- `GET /api/products`  
  Returns all products.

- `GET /api/products/SearchByName?name=value`  
  Returns products filtered by name.

- `POST /api/products`  
  Accepts a JSON body and inserts a new product.

- `PUT /api/products?id=value`  
  Replaces an existing product by ID with the one provided in the body.

- `PATCH /api/products?id=value`  
  Updates only specific fields of a product based on the provided JSON.

- `DELETE /api/products?id=value`  
  Deletes a product by ID.

---

## 🧾 Example Product Model

```json
{
  "ProductId": "605c72ef2f4b2b39a4dfb123",
  "Name": "Laptop",
  "Category": "Electronics",
  "Price": 999.99
}
