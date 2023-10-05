using Microsoft.Data.Sqlite;

namespace bookclub.Controllers;

public class ReviewDBAdapter
{
  private string reviewDB = "reviews.db";
  private string recommendationDB = "recommendations.db";

  public Review getReview(int id)
  {
    using (var connection = new SqliteConnection($"Data Source={reviewDB}"))
    {
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText =
      @"
        SELECT * FROM reviews
        WHERE id = $id;
      ";
      command.Parameters.AddWithValue("$id", id);



      using (var reader = command.ExecuteReader())
      {
        while (reader.Read())
        {
          var title = reader.GetString(1);
          var reviewer = reader.GetString(2);
          var rating = reader.GetInt32(3);
          return new Review { BookTitle = title, Reviewer = reviewer, Rating = rating };
        }
      }
    }
    Console.WriteLine("Error: Failed to find review with id: ", id);
    return new Review();
  }


  public List<Review> getReviewsByReviewer(string reviewerName)
  {
    List<Review> Reviews = new List<Review>
    { };

    using (var connection = new SqliteConnection($"Data Source={reviewDB}"))
    {
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText =
      @"
        SELECT * FROM reviews
        WHERE reviewer = $reviewer;
      ";
      command.Parameters.AddWithValue("$reviewer", reviewerName);



      using (var reader = command.ExecuteReader())
      {
        while (reader.Read())
        {
          var title = reader.GetString(1);
          var reviewer = reader.GetString(2);
          var rating = reader.GetInt32(3);

          Reviews.Add(new Review { BookTitle = title, Reviewer = reviewer, Rating = rating });
        }
      }
    }

    return Reviews;
  }

  public List<Review> getReviewsByBook(string BookTitle)
  {
    List<Review> Reviews = new List<Review>
    { };

    using (var connection = new SqliteConnection($"Data Source={reviewDB}"))
    {
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText =
      @"
        SELECT * FROM reviews
        WHERE title = $title;
      ";
      command.Parameters.AddWithValue("$title", BookTitle);



      using (var reader = command.ExecuteReader())
      {
        while (reader.Read())
        {
          var title = reader.GetString(1);
          var reviewer = reader.GetString(2);
          var rating = reader.GetInt32(3);

          Reviews.Add(new Review { BookTitle = title, Reviewer = reviewer, Rating = rating });
        }
      }
    }

    return Reviews;
  }


  public int createNewReview(Review newReview)
  {
    if (isDuplicateReview(newReview))
    {
      Console.WriteLine("Duplicate review detected. No review created.");
      return 0;
    }
    using (var connection = new SqliteConnection($"Data Source={reviewDB}"))
    {
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText =
      @"
        INSERT INTO reviews (title, reviewer, rating)
        VALUES ($title, $reviewer, $rating);
    ";
      command.Parameters.AddWithValue("$title", newReview.BookTitle);
      command.Parameters.AddWithValue("$reviewer", newReview.Reviewer);
      command.Parameters.AddWithValue("$rating", newReview.Rating);

      command.ExecuteNonQuery();
    }

    return 1;
  }

  public List<String> getReviewers()
  {
    List<String> Reviewers = new List<String>();
    using (var connection = new SqliteConnection("Data Source=reviews.db"))
    {
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText =
      @"
        SELECT DISTINCT reviewer FROM reviews;
      ";

      using (var reader = command.ExecuteReader())
      {
        while (reader.Read())
        {
          var reviewer = reader.GetString(0);
          Reviewers.Add(reviewer);
        }
      }
    }
    return Reviewers;
  }

  public bool isDuplicateReview(Review newReview)
  {
    using (var connection = new SqliteConnection("Data Source=reviews.db"))
    {
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText =
      @"
        SELECT COUNT(*) FROM reviews
        WHERE reviewer = $reviewer AND title = $title;
      ";
      command.Parameters.AddWithValue("$title", newReview.BookTitle);
      command.Parameters.AddWithValue("$reviewer", newReview.Reviewer);

      using (var reader = command.ExecuteReader())
      {
        while (reader.Read())
        {
          if (reader.GetInt32(0) == 0)
          {
            return false;
          }
        }
      }
      return true;
    }
  }

  public List<Recommendation> getRecommendationsByUser(string userName)
  {
    List<Recommendation> Recommendations = new List<Recommendation> { };

    using (var connection = new SqliteConnection($"Data Source={recommendationDB}"))
    {
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText =
      @"
        SELECT * FROM recommendations
        WHERE user = $user;
      ";
      command.Parameters.AddWithValue("$user", userName);



      using (var reader = command.ExecuteReader())
      {
        while (reader.Read())
        {
          var user = reader.GetString(1);
          var bookTitle = reader.GetString(2);
          var priority = reader.GetInt32(3);

          Recommendations.Add(new Recommendation { User = user, BookTitle = bookTitle, Priority = priority });
        }
      }
    }
    return Recommendations;
  }
}
