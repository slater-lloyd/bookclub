from math import sqrt


def calcDotProduct(x, y):
    result = 0

    for i in range(len(x)):
        result += x[i] * y[i]

    return result


def calcMagnitude(x):
    result = 0

    for i in x:
        result += i**2

    return sqrt(result)


def calcCosSim(x, y):
    dot = calcDotProduct(x, y)
    magx = calcMagnitude(x)
    magy = calcMagnitude(y)
    cos = dot / (magx*magy)
    print(f"Dot: {dot}\nMag (x,y): {magx},{magy}\nCos: {cos}")
    return cos


def main():
    x = [0, 0, 5, 5, 5, 5, 5]
    y = [5, 5, 0, 5, 0, 0, 0]

    calcCosSim(x, y)


if __name__ == "__main__":
    main()
