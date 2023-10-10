import sqlite3
from Review import Review
from util import calcCosSim


class Recommender:
    def __init__(self, revDB="reviews.db", recDB="recommendations.db"):
        self.revDB = revDB
        self.recDB = recDB
        self.reviews: [Review] = []
        self.books = set()
        self.users = set()
        self.importReviews()
        self.books = list(self.books)
        self.users = list(self.users)

    def importReviews(self):
        with sqlite3.connect(self.revDB) as connection:
            cursor = connection.cursor()
            rows = cursor.execute("SELECT * FROM reviews").fetchall()
            for row in rows:
                self.reviews.append(Review(row[1], row[2], row[3]))
                self.books.add(row[1])
                self.users.add(row[2])

    def getReviewsByUser(self, user):
        reviews = {}
        for book in self.books:
            reviews[book] = 0

        for review in self.reviews:
            if review.reviewer == user:
                reviews[review.bookTitle] = review.rating

        return reviews

    def getSimilarUsers(self, targetUser, friendCount=2):
        similarity = {}
        targetReviews = self.getReviewsByUser(targetUser)
        for user in self.users:
            if user != targetUser:
                x = []
                y = []
                userReviews = self.getReviewsByUser(user)
                for key in targetReviews.keys():
                    x.append(targetReviews[key])
                    y.append(userReviews[key])
                similarity[user] = calcCosSim(x, y)
        print(f"User similarity to {targetUser}")
        for user in similarity.keys():
            print(f"{user}: {similarity[user]}")

    def getUnreadBooks(self, user):
        unreadBooks = list(self.books)
        for review in self.reviews:
            if review.reviewer == user:
                unreadBooks.remove(review.bookTitle)
        return unreadBooks

    def getRecommendations(self, user):
        pass

    def clearDBRows(self):
        with sqlite3.connect(self.recDB) as connection:
            cursor = connection.cursor()
            cursor.execute("DELETE FROM recommendations")

    def storeRecommendations(self):
        self.clearDBRows()
        recommendations = []
        for user in self.users:
            recommendations.extend(self.getRecommendations(user))
        with sqlite3.connect(self.recDB) as connection:
            cursor = connection.cursor()


def main():
    recommender = Recommender()

    recommender.getSimilarUsers("Slater")


if __name__ == "__main__":
    main()
