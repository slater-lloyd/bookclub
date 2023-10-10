Instructions to run:
Use "dotnet run" to start server.
Use "python3 ml/recommend.py" to run job to calculate and store book recommendations.

Dotnet methods:
Reviews GET method: input a user's name to get all reviews that a user has made.
Reviews POST method: use the following request body to insert a new reviews
{
  "bookTitle": "Dune",
  "reviewer": "Dan",
  "rating": 5
}
Recommendations GET method: input a user's name to get book recommendations for that user.
