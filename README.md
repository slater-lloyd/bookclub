Overview:
This is a backend program that implements a REST api for reviewing/rating books. All reviews are logged in an SQL database. In the 'ml' folder, there is a program to use machine learning to determine book recommendations for each user. The algorithm, Cosine Similarity, finds similar users and provides recommendations based on what other similar users enjoyed. 


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
