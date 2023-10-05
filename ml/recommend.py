import sqlite3
from Review import Review


class Recommender:
    def __init__(self, revDB="reviews.db", recDB="recommendations.db"):
        self.revDB = revDB
        self.recDB = recDB
        self.reviews: [Review] = []
        self.books = set()
        self.users = set()
        self.importReviews()
        for review in self.reviews:
            print(review)

    def importReviews(self):
        with sqlite3.connect(self.revDB) as connection:
            cursor = connection.cursor()
            rows = cursor.execute("SELECT * FROM reviews").fetchall()
            for row in rows:
                self.reviews.append(Review(row[1], row[2], row[3]))
                self.books.add(row[1])
                self.users.add(row[2])

    def getBFFs(self, user, friendCount=2):
        pass

    def getUnreadBooks(self, user):
        unreadBooks = list(self.books)
        for review in self.reviews:
            if review.reviewer == user:
                unreadBooks.remove(review.bookTitle)
        return unreadBooks

    def getRecommendations(self, user):
        pass

    def getReviewCount(self, bookTitle):
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
    print(recommender.getUnreadBooks("Michelle"))


if __name__ == "__main__":
    main()
