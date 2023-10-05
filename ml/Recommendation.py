class Recommendation:
    def __init__(self, user, bookTitle, priority):
        self.user = user
        self.bookTitle = bookTitle
        self.priority = priority

    def __str__(self):
        return f"{self.user}: ({self.priority}) {self.bookTitle}"
