# RESTful Web API with HATEOAS implementation
This API is a simple project that implements the RESTful architecture with the HATEOAS (Hypermedia as the Engine of Application State) principle.
It's built to list a basic system for registering people, their addresses, phone numbers, and associated products.
> All data used is fictional, generated with the [Bogus.Faker](https://github.com/bchavez/Bogus) library.

Example of a response for the endpoint:

```
GET /api/person: Returns all people
```

Response:
```json
{
    "pagination": {
        "currentPage": 2,
        "pageSize": 5,
        "totalItems": 100,
        "previous": {
            "rel": "previous",
            "href": "https://localhost:5001/api/person?page_number=1&page_size=5"
        },
        "next": {
            "rel": "next",
            "href": "https://localhost:5001/api/person?page_number=3&page_size=5"
        }
    },
    "_links": [
        {
            "rel": "self",
            "href": "https://localhost:5001/api/person"
        }
    ],
    "data": [
        {
            "_links": [
                {
                    "rel": "self",
                    "href": "https://localhost:5001/api/person/569.549.913-50"
                },
                {
                    "rel": "addresses",
                    "href": "https://localhost:5001/api/person/569.549.913-50/addresses"
                },
                {
                    "rel": "phones",
                    "href": "https://localhost:5001/api/person/569.549.913-50/phones"
                },
                {
                    "rel": "products",
                    "href": "https://localhost:5001/api/person/569.549.913-50/products"
                }
            ],
            "document": "569.549.913-50",
            "name": "Margarida Carvalho"
        },
        {
            "_links": [
                {
                    "rel": "self",
                    "href": "https://localhost:5001/api/person/671.061.774-23"
                },
                {
                    "rel": "addresses",
                    "href": "https://localhost:5001/api/person/671.061.774-23/addresses"
                },
                {
                    "rel": "phones",
                    "href": "https://localhost:5001/api/person/671.061.774-23/phones"
                },
                {
                    "rel": "products",
                    "href": "https://localhost:5001/api/person/671.061.774-23/products"
                }
            ],
            "document": "671.061.774-23",
            "name": "Antonella Xavier"
        },
        {
            "_links": [
                {
                    "rel": "self",
                    "href": "https://localhost:5001/api/person/737.298.873-00"
                },
                {
                    "rel": "addresses",
                    "href": "https://localhost:5001/api/person/737.298.873-00/addresses"
                },
                {
                    "rel": "phones",
                    "href": "https://localhost:5001/api/person/737.298.873-00/phones"
                },
                {
                    "rel": "products",
                    "href": "https://localhost:5001/api/person/737.298.873-00/products"
                }
            ],
            "document": "737.298.873-00",
            "name": "Théo Melo"
        },
        {
            "_links": [
                {
                    "rel": "self",
                    "href": "https://localhost:5001/api/person/274.686.075-91"
                },
                {
                    "rel": "addresses",
                    "href": "https://localhost:5001/api/person/274.686.075-91/addresses"
                },
                {
                    "rel": "phones",
                    "href": "https://localhost:5001/api/person/274.686.075-91/phones"
                },
                {
                    "rel": "products",
                    "href": "https://localhost:5001/api/person/274.686.075-91/products"
                }
            ],
            "document": "274.686.075-91",
            "name": "João Miguel Reis"
        },
        {
            "_links": [
                {
                    "rel": "self",
                    "href": "https://localhost:5001/api/person/366.982.384-06"
                },
                {
                    "rel": "addresses",
                    "href": "https://localhost:5001/api/person/366.982.384-06/addresses"
                },
                {
                    "rel": "phones",
                    "href": "https://localhost:5001/api/person/366.982.384-06/phones"
                },
                {
                    "rel": "products",
                    "href": "https://localhost:5001/api/person/366.982.384-06/products"
                }
            ],
            "document": "366.982.384-06",
            "name": "Isabela Reis"
        }
    ]
}
```

## References
- https://restfulapi.net/hateoas