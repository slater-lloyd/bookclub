class Review:
    def __init__(self, bookTitle, reviewer, rating):
        self.bookTitle = bookTitle
        self.reviewer = reviewer
        self.rating = rating

    def __str__(self):
        return f"{self.bookTitle}, {self.reviewer}, {self.rating}"
